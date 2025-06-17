using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JournalDropdown : MonoBehaviour
{
    public RectTransform dropdownRectTransform => GetComponent<RectTransform>();
    private Toggle toggle => GetComponentInChildren<Toggle>();
    private CanvasGroup ButtonsContainer => GetComponentInChildren<CanvasGroup>();

    private float minHeight = 0;

    public static event Action OnDropdownClicked;

    public int SceneIndex;
    public GameObject DropdownButtonPrefab;
    public Text DropdownHeaderText;
    public Text LessonsFoundText;
    public List<LessonItem> LessonsInScene;


    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleDropdownClick);
        minHeight = dropdownRectTransform.sizeDelta.y;

        SetHeaderText();
        CreateDropdownButtons();
    }

    private void HandleDropdownClick(bool _open)
    {
        //set buttonscontainer state
        ButtonsContainer.interactable = _open;
        ButtonsContainer.alpha = _open?1:0;
        ButtonsContainer.blocksRaycasts = _open;

        //change rect transform sizes
        dropdownRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height(_open));
        OnDropdownClicked?.Invoke();

        // turn arrow when clicked

    }

    private void CreateDropdownButtons()
    {
        for(int i = 0; i < LessonsInScene.Count; i++)
        {
            DropdownButtonPrefab.GetComponentInChildren<JournalDropdownButton>()
                .SetValues(LessonsInScene[i].LessonId, LessonsInScene[i].HeaderText, LessonsInScene[i].ContentText);

            Instantiate(DropdownButtonPrefab, ButtonsContainer.transform);
        }
        SetLessonsFoundText(0);
    }

    private void SetHeaderText()
    {
        DropdownHeaderText.text = SceneManager.GetSceneByBuildIndex(SceneIndex).name;
        //SceneManager.GetSceneAt(SceneIndex).name;
    }

    public void SetLessonsFoundText(int _value)
    {
        LessonsFoundText.text = _value + "/" + LessonsInScene.Count;
    }

    private float Height(bool _open)
    {
        float _height = 0;
        _height = ButtonsContainer.gameObject.GetComponent<VerticalLayoutGroup>().preferredHeight
            * (_open ? 1f:0f) + minHeight;
        return _height * 1.1f;
    }
}

[Serializable]
public struct LessonItem
{
    public int LessonId;
    public string HeaderText;
    [TextArea(15, 20)] public string ContentText;
}