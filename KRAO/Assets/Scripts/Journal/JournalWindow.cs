using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JournalWindow : Window
{
    private List<JournalDropdown> journalDropdowns => GetComponentsInChildren<JournalDropdown>().ToList();
    private List<JournalDropdownButton> dropdownButtons => GetComponentsInChildren<JournalDropdownButton>().ToList();
    private RectTransform dropdownsContainerRectTransform => DropdownsContainer.GetComponent<RectTransform>();

    [Header("Lesson")]
    public Text HeaderText;
    public Text ContentText;
    public Button PrevLessonButton;
    public Button NextLessonButton;

    [Header("ScrollBox")]
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
        // go to next active/found lesson

        // if current lesson last in list disable button
    }

    private void HandlePrevLessonButtonClick()
    {
        // go to previous active/found lesson

        // if current lesson first in list disable button

    }

    private void HandleDropdownClicked()
    {
        //change rect transform size (to dropdown sizes)
        dropdownsContainerRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DropdownHeight());
    }

    private float DropdownHeight()
    {
        float _height = 0;

        foreach (JournalDropdown _d in journalDropdowns)
        {
            _height += _d.dropdownRectTransform.sizeDelta.y;
        }

        return _height;
    }

    #endregion

    #region public methods
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

    public void SetLessonTexts(string _header, string _content)
    {
        HeaderText.text = _header;
        ContentText.text = _content;
    }
    #endregion
}