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
        JournalDropdown.OnDropdownClicked += HandleDropdownClicked;
        PrevLessonButton.onClick.AddListener(HandlePrevLessonButtonClick);
        NextLessonButton.onClick.AddListener(HandleNextLessonButtonClick);
    }

    private void HandleNextLessonButtonClick()
    {
        int _next = 0;
        for (int i = CurrentLesson() + 1; i < dropdownButtons.Count; i++)
        {
            if (dropdownButtons[i].IsInteractable())
            {
                _next = i;
                break;
            }
        }
        dropdownButtons[_next].HandleJournalButtonClick();
    }

    private void HandlePrevLessonButtonClick()
    {
        int _prev = 0;
        for (int i = CurrentLesson() - 1; i >= -1; i--)
        {
            if (i == -1)
            {
                i = dropdownButtons.Count - 1;
            }
            if (dropdownButtons[i].IsInteractable())
            {
                _prev = i;
                break;
            }
        }
        dropdownButtons[_prev].HandleJournalButtonClick();
    }

    private int CurrentLesson()
    {
        int _lesson = 0;
        for (int i = 0; i < dropdownButtons.Count; i++)
        {
            if (dropdownButtons[i].isOpen)
            {
                _lesson = i;
                break;
            }
        }
        return _lesson;
    }

    private void HandleDropdownClicked(int _sceneId)
    {
        //change rect transform size (to dropdown sizes)
        dropdownsContainerRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DropdownHeight());

        //change image
        JournalImage.sprite = lessonTracker.SceneItemById(_sceneId).SceneImage;
    }

    private float DropdownHeight()
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
    public void CreateDropdowns(List<SceneItem> _sceneItems)
    {
        for (int i = 0; i < _sceneItems.Count; i++)
        {
            JournalDropdownPrefab.GetComponent<JournalDropdown>().SetInitValues(_sceneItems[i]);
            Instantiate(JournalDropdownPrefab, DropdownsContainer.transform);
        }

        JournalDropdowns = GetComponentsInChildren<JournalDropdown>().ToList();
    }

    public void CreateDropdownButtons(List<SceneItem> _sceneItems)
    {
        for (int i = 0; i < JournalDropdowns.Count; i++)
        {
            JournalDropdowns[i].CreateDropdownButtons(_sceneItems[i].lessons);
        }

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

    public bool CheckIsLessonActivated(int _lessonId)
    {
        bool _returnValue = false;

        if (!lessonTracker.LessonItemById(_lessonId).IsNew)
        {
            _returnValue = true;
        }

        return _returnValue;
    }

    public void SetLessonTexts(int _lessonId)
    {
        HeaderText.text = lessonTracker.LessonItemById(_lessonId).HeaderText;
        ContentText.text = lessonTracker.LessonItemById(_lessonId).ContentText;
        JournalImage.sprite = lessonTracker.LessonItemById(_lessonId).LessonImage;

        ResetScrollbar();
    }

    public void ResetScrollbar()
    {
        StartCoroutine(ResetScrollbar(ContentScrollbar));
    }
    #endregion
}