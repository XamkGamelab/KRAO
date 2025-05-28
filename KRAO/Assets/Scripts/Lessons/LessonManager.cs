using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

/* The object that this script is connected to should have a Rigidbody
 * and the lesson object should have a collider as trigger
 */

public class LessonManager : MonoBehaviour
{
    private LessonProgressBar progressBar => FindFirstObjectByType<LessonProgressBar>();
    private Journal journal => FindFirstObjectByType<Journal>();
    private MenuManager menuManager => FindFirstObjectByType<MenuManager>();
    private FocusView focusView => GameObject.FindWithTag("FocusView").GetComponent<FocusView>();
    private CinemachineCamera focusCamera => GameObject.FindWithTag("FocusView").GetComponent<CinemachineCamera>();
    private CinemachineOrbitalFollow orbitalFollow => GameObject.FindWithTag("FocusView").GetComponent<CinemachineOrbitalFollow>();
    private List<Lesson> lessons => FindObjectsByType<Lesson>(FindObjectsSortMode.None).ToList();
    //private LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    private Lesson openLesson;


    private int foundLessons = 0;

    private void Start()
    {
        Lesson.OnLessonOpened += HandleLessonOpened;
        Lesson.OnLessonClosed += HandleLessonClosed;

        Debug.Log("Lessons in scene: " + lessons.Count);
    }

    private void HandleLessonClosed(Lesson _lesson)
    {
        openLesson = null;
        // Close FocusView
        focusView.ToggleFocusView();
        // Close lesson text box (canvas)
        menuManager.CloseWindow(lessonWindow.gameObject);
    }

    public void CloseLesson()
    {
        openLesson.ToggleLesson();
    }

    private void HandleLessonOpened(Lesson _lesson)
    {
        openLesson = _lesson;
        // Set lessonObject as tracking target
        focusCamera.Target.TrackingTarget = _lesson.LessonFocusObject.transform;
        orbitalFollow.Radius = _lesson.FocusRadius;
        // Open FocusView
        focusView.ToggleFocusView();

        // Open lesson text box (canvas)
        menuManager.OpenWindow(lessonWindow.gameObject);

        if (_lesson.NewLessonFound)
        {
            foundLessons++;
            AddLessonToJournal(_lesson);
            UpdateProgressBar();
        }
    }

    private void AddLessonToJournal(Lesson _lesson)
    {
        journal.AddNewLessonButton(_lesson.HeaderText, _lesson.ContentText);

        Debug.Log("Lesson added to journal");
    }

    private void UpdateProgressBar()
    {
        progressBar.ChangeSliderValue(foundLessons, lessons.Count);
    }
}