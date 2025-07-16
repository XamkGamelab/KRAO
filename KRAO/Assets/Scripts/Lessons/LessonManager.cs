using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class LessonManager : MonoBehaviour
{
    private JournalWindow journal => FindFirstObjectByType<JournalWindow>();
    private FocusView focusView => GameObject.FindWithTag("FocusView").GetComponent<FocusView>();
    private CinemachineCamera focusCamera => GameObject.FindWithTag("FocusView").GetComponent<CinemachineCamera>();
    private CinemachineOrbitalFollow orbitalFollow => GameObject.FindWithTag("FocusView").GetComponent<CinemachineOrbitalFollow>();
    private LessonTracker lessonTracker => FindFirstObjectByType<LessonTracker>();
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    private Lesson openLesson;


    private void Start()
    {
        Lesson.OnLessonOpened += HandleLessonOpened;
        Lesson.OnLessonClosed += HandleLessonClosed;
    }

    private void HandleLessonClosed(Lesson _lesson)
    {
        openLesson = null;
        // Close FocusView
        focusView.ToggleFocusView();
    }

    public void CloseLesson()
    {
        openLesson.ToggleLesson();
    }

    private void HandleLessonOpened(Lesson _lesson)
    {
        openLesson = _lesson;
        // Set lessonObject as tracking target
        SetFocus(_lesson);

        // Open FocusView
        focusView.ToggleFocusView();

        // Open lesson text box (canvas)
        lessonWindow.OpenWindow();

        if (_lesson.NewLessonFound)
        {
            AddLessonToJournal(_lesson);
            lessonTracker.AddLessonToTracker(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void AddLessonToJournal(Lesson _lesson)
    {
        journal.ActivateLesson(_lesson.LessonId);
        Debug.Log("Lesson added to journal");
    }

    private void SetFocus(Lesson _lesson)
    {
        if(_lesson.FocusPoints.Length > 0)
        {
            focusCamera.Target.TrackingTarget = _lesson.FocusPoints[0].FocusTransform;
            orbitalFollow.Radius = _lesson.FocusPoints[0].Radius;
        }
        else
        {
            focusCamera.Target.TrackingTarget = _lesson.transform;
            orbitalFollow.Radius = 2f;
        }
    }
}