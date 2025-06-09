using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalSceneDropdown : MonoBehaviour
{
    public Button SceneButton;
    public GameObject LessonButtons;
    private bool open = true;

    private void Awake()
    {
        SceneButton.onClick.AddListener(OnSceneButtonClicked);
    }

    private void OnSceneButtonClicked()
    {
        open = !open;
        LessonButtons.SetActive(open);
    }
}