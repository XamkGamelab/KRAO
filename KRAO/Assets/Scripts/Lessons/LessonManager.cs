using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class LessonManager : MonoBehaviour
{
    private Journal journal => FindFirstObjectByType<Journal>();
    private MenuManager menuManager => FindFirstObjectByType<MenuManager>();
    private FocusView focusView => GameObject.FindWithTag("FocusView").GetComponent<FocusView>();
    private CinemachineCamera focusCamera => GameObject.FindWithTag("FocusView").GetComponent<CinemachineCamera>();
    private List<Lesson> lessons => FindObjectsByType<Lesson>(FindObjectsSortMode.None).ToList();

    private void Start()
    {
        Lesson.OnLessonOpened += HandleLessonOpened;
        Lesson.OnLessonClosed += HandleLessonClosed;

        Debug.Log("Lessons in scene: " + lessons.Count);
    }

    private void HandleLessonClosed(Lesson _lesson)
    {
        // Close FocusView
        focusView.ToggleFocusView();
        // Close lesson text box (canvas)
        menuManager.CloseWindow(_lesson.ContentBox);
    }

    private void HandleLessonOpened(Lesson _lesson)
    {
        // Set lessonObject as tracking target
        focusCamera.Target.TrackingTarget = _lesson.LessonFocusObject.transform;
        // Open FocusView
        focusView.ToggleFocusView();
        // Open lesson text box (canvas)
        menuManager.OpenWindow(_lesson.ContentBox);

        if (_lesson.NewLessonFound)
        {
            AddLessonToJournal(_lesson);
            UpdateHUD();
        }
    }

    private void AddLessonToJournal(Lesson _lesson)
    {
        journal.AddNewLessonButton(_lesson.HeaderText, _lesson.ContentText);

        Debug.Log("Lesson added to journal");
    }

    private void UpdateHUD()
    {
        //show message "New lesson found" ???
        //progress bar in scene ???
    }
}