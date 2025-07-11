using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JournalWindow : Window
{
    public List<JournalDropdown> JournalDropdowns { get; set; }
    private List<JournalDropdownButton> dropdownButtons;
    private RectTransform dropdownsContainerRectTransform => DropdownsContainer.GetComponent<RectTransform>();

    [Header("Lesson")]
    public Text HeaderText;
    public Text ContentText;
    public Button PrevLessonButton;
    public Button NextLessonButton;

    [Header("ScrollBox")]
    public GameObject JournalDropdownPrefab;
    public GameObject DropdownsContainer;
    public List<JDropdown> Dropdowns;

    #region private methods
    private void Awake()
    {
        JournalDropdown.OnDropdownClicked += HandleDropdownClicked;
        PrevLessonButton.onClick.AddListener(HandlePrevLessonButtonClick);
        NextLessonButton.onClick.AddListener(HandleNextLessonButtonClick);
        
        CreateDropdowns();
        JournalDropdowns = GetComponentsInChildren<JournalDropdown>().ToList();

        CreateDropdownButtons();
        dropdownButtons = GetComponentsInChildren<JournalDropdownButton>().ToList();
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

        foreach (JournalDropdown _d in JournalDropdowns)
        {
            _height += _d.dropdownRectTransform.sizeDelta.y;
        }

        return _height;
    }

    private void CreateDropdowns()
    {
        for (int i = 0; i < Dropdowns.Count; i++)
        {
            JournalDropdownPrefab.GetComponent<JournalDropdown>().SetInitValues(Dropdowns[i].SceneIndex,
                Dropdowns[i].SceneHeader, Dropdowns[i].lessons);
            Instantiate(JournalDropdownPrefab, DropdownsContainer.transform);
        }
    }

    private void CreateDropdownButtons()
    {
        for (int i = 0; i < JournalDropdowns.Count; i++)
        {
            JournalDropdowns[i].CreateDropdownButtons(Dropdowns[i].lessons);
            Debug.Log("BUTTONS");
        }
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

[Serializable]
public struct JDropdown
{
    public string SceneHeader;
    public int SceneIndex;
    public List<LessonItem> lessons;
}

[Serializable]
public struct LessonItem
{
    public string HeaderText;
    public int LessonId;
    [TextArea(15, 20)] public string ContentText;
}