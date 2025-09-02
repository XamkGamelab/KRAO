using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JournalWindow : Window
{
    private LessonTracker lessonTracker => FindFirstObjectByType<LessonTracker>();
    public List<JournalDropdown> JournalDropdowns { get; set; }
    public List<JournalDropdownButton> dropdownButtons { get; set; }
    private RectTransform dropdownsContainerRectTransform => DropdownsContainer.GetComponent<RectTransform>();

    [Header("Lesson")]
    public RectTransform ContentRect;
    public Scrollbar ContentScrollbar;
    public Text HeaderText;
    public Text ContentText;
    public Button PrevLessonButton;
    public Button NextLessonButton;
    public Image JournalImage;

    [Header("ScrollBox")]
    public GameObject JournalDropdownPrefab;
    public GameObject DropdownsContainer;

    #region private methods
    private void Awake()
    {
        // Listen to dropdown clicks
        JournalDropdown.OnDropdownClicked += HandleDropdownClicked;
        // Start listening to next and previous button clicks
        PrevLessonButton.onClick.AddListener(HandlePrevLessonButtonClick);
        NextLessonButton.onClick.AddListener(HandleNextLessonButtonClick);
    }

    // Open next found lesson if there is one
    private void HandleNextLessonButtonClick()
    {
        if (CurrentLesson() != -1)
        {
            // Default is current open lesson if no other found lesson are found
            int _next = CurrentLesson();
            if (dropdownButtons[CurrentLesson()].isOpen)
            {
                for (int i = CurrentLesson() + 1; i < dropdownButtons.Count + 1; i++)
                {
                    // When at end of list, go to first button in list
                    if (i == dropdownButtons.Count)
                    {
                        i = 0;
                    }
                    // If an interactable (found) lesson/button is found
                    if (dropdownButtons[i].IsInteractable())
                    {
                        //Set its list index to _next
                        _next = i;
                        break;
                    }
                }
            }
            // Open the button with list index of _next
            dropdownButtons[_next].HandleJournalButtonClick();
        }
    }

    // Open previous found lesson if there is one
    private void HandlePrevLessonButtonClick()
    {
        if (CurrentLesson() != -1)
        {
            // Default is current open lesson if no other found lesson are found
            int _prev = CurrentLesson();
            for (int i = CurrentLesson() - 1; i >= -1; i--)
            {
                // Set i as the last button in the list when at the beginning of the list (to loop)
                if (i == -1)
                {
                    i = dropdownButtons.Count - 1;
                }
                // If an interactable (found) lesson/button is found
                if (dropdownButtons[i].IsInteractable())
                {
                    //Set its list index to _prev
                    _prev = i;
                    break;
                }
            }
            // Open the button with list index of _prev
            dropdownButtons[_prev].HandleJournalButtonClick();
        }
    }

    private int CurrentLesson()
    {
        // Default return value is -1 because using 0 would open the first lesson even if it isn't found yet
        // (when pressing next/previous buttons)
        int _lesson = -1;
        // Go through dropdown buttons
        for (int i = 0; i < dropdownButtons.Count; i++)
        {
            // If button's lesson is open in Journal
            if (dropdownButtons[i].isOpen)
            {
                //return its index in the list
                _lesson = i;
                break;
            }
        }
        // If no lesson was open
        if (_lesson == -1)
        {
            for (int i = 1;i < dropdownButtons.Count+1; i++)
            {
                // Try to find the first lesson that is found
                if (!lessonTracker.LessonItemById(i).IsNew)
                {
                    // return i-1, because list ids start from 0 and lesson ids from 1
                    _lesson = i - 1;
                    break;
                }
            }
        }
        return _lesson;
    }

    // Resize dropdown container (to accommodate the buttons), change image
    private void HandleDropdownClicked(int _sceneId)
    {
        //change rect transform size (to dropdown sizes)
        dropdownsContainerRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DropdownContainerHeight());

        //change image
        JournalImage.sprite = lessonTracker.SceneItemById(_sceneId).SceneImage;
    }

    // Get combined height of dropdowns
    private float DropdownContainerHeight()
    {
        float _height = 0;

        foreach (JournalDropdown _d in JournalDropdowns)
        {
            _height += _d.dropdownRectTransform.sizeDelta.y;
        }

        return _height;
    }
    #endregion

    #region public methods
    // Create dropdowns for each scene to the dropdowns container
    public void CreateDropdowns(List<SceneItem> _sceneItems)
    {
        for (int i = 0; i < _sceneItems.Count; i++)
        {
            // Get values from lesson tracker
            JournalDropdownPrefab.GetComponent<JournalDropdown>().SetInitValues(_sceneItems[i]);
            Instantiate(JournalDropdownPrefab, DropdownsContainer.transform);
        }
        // Add created dropdowns to list
        JournalDropdowns = GetComponentsInChildren<JournalDropdown>().ToList();
    }

    // Create lesson buttons under the dropdowns
    public void CreateDropdownButtons(List<SceneItem> _sceneItems)
    {
        for (int i = 0; i < JournalDropdowns.Count; i++)
        {
            // Get values from lesson tracker
            JournalDropdowns[i].CreateDropdownButtons(_sceneItems[i].lessons);
        }
        // Add the created buttons to a list
        dropdownButtons = GetComponentsInChildren<JournalDropdownButton>().ToList();
    }

    public void ActivateLesson(int _lessonId)
    {
        // make button with correct id interactable + show checkmark
        for (int i = 0; i < dropdownButtons.Count; i++)
        {
            if (dropdownButtons[i].LessonId == _lessonId)
            {
                dropdownButtons[i].ActivateButton();
            }
        }
    }

    public void SetLessonTexts(int _lessonId)
    {
        HeaderText.text = lessonTracker.LessonItemById(_lessonId).HeaderText;
        ContentText.text = lessonTracker.LessonItemById(_lessonId).ContentText;
        JournalImage.sprite = lessonTracker.LessonItemById(_lessonId).LessonImage;

        ResetScrollbar();
    }

    // Set scrollbar to top/beginning of content
    public void ResetScrollbar()
    {
        StartCoroutine(ResetScrollbar(ContentScrollbar));
    }
    #endregion
}