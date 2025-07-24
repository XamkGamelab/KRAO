using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalDropdown : MonoBehaviour
{
    public RectTransform dropdownRectTransform => GetComponent<RectTransform>();
    private Toggle toggle => GetComponentInChildren<Toggle>();
    public int SceneIndex { get; private set; }
    public static event Action OnDropdownClicked;

    private float minHeight = 0;
    public GameObject Arrow;
    public CanvasGroup ButtonsContainer;
    public GameObject DropdownButtonPrefab;
    public Text DropdownHeaderText;
    public Text LessonsFoundText;


    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleDropdownClick);
        minHeight = dropdownRectTransform.sizeDelta.y;
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

        // turn arrow
        Arrow.transform.Rotate(0, 0, 180);
    }

    private float Height(bool _open)
    {
        float _height = 0;
        _height = ButtonsContainer.gameObject.GetComponent<VerticalLayoutGroup>().preferredHeight
            * (_open ? 1f : 0f) + minHeight;
        return _height;
    }

    public void CreateDropdownButtons(List<LessonItem> _lessonItems)
    {
        for (int i = 0; i < _lessonItems.Count; i++)
        {
            DropdownButtonPrefab.GetComponentInChildren<JournalDropdownButton>()
                .SetValues(_lessonItems[i].LessonId, _lessonItems[i].HeaderText);

            Instantiate(DropdownButtonPrefab, ButtonsContainer.transform);
        }
    }

    public void SetInitValues(int _sceneIndex, string _header)
    {
        SceneIndex = _sceneIndex;
        DropdownHeaderText.text = _header;
    }

    public void SetLessonsFoundText(int _value, int _max)
    {
        LessonsFoundText.text = _value + "/" + _max;
    }
}