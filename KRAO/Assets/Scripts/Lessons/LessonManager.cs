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
    private LessonFocusPoints lessonFocusPoints => FindFirstObjectByType<LessonFocusPoints>();
    private LessonFeatures lessonFeatures => FindFirstObjectByType<LessonFeatures>();
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    private Lesson openLesson;


    private void Start()
    {
        Lesson.OnLessonOpened += HandleLessonOpened;
        Lesson.OnLessonClosed += HandleLessonClosed;
    }

    private void HandleLessonClosed(Lesson _lesson)
    {
        if (_lesson.HasMultipleFocusPoints())
        {
            lessonFocusPoints.DisableFocusPoints();
        }

        if (_lesson.HasLessonFeatures())
        {
            lessonFeatures.DisableLessonFeatures();
        }
        // Close FocusView
        focusView.ToggleFocusView();

        openLesson = null;
    }

    //Called in LessonWindow (prefab) CloseLessonButton onClick
    public void CloseLesson()
    {
        openLesson.ToggleLesson();
    }

    private void HandleLessonOpened(Lesson _lesson)
    {
        openLesson = _lesson;
        // Set lessonObject as tracking target
        SetFocus(_lesson.FocusPoints[0]);

        // Reset FocusView Rotation and open FocusView
        focusView.ResetOribtalCameraValues();
        focusView.ToggleFocusView();

        // If the lesson has multiple focus points for the camera, enable the UI to swap between them
        if (_lesson.HasMultipleFocusPoints())
        {
            lessonFocusPoints.EnableFocusPoints(_lesson.FocusPoints);
        }

        // If the lesson has any features, generate buttons for them
        if (_lesson.HasLessonFeatures())
        {
            Debug.Log("Features found");
            lessonFeatures.EnableLessonFeatures(_lesson.LessonFeatures);
        }

        // Open lesson text box (canvas)
        lessonWindow.OpenWindow();

        if (lessonTracker.LessonItemById(_lesson.LessonId).IsNew)
        {
            AddLessonToJournal(_lesson);
            lessonTracker.AddLessonToTracker(SceneManager.GetActiveScene().buildIndex, _lesson.LessonId);
        }
    }

    public bool CheckIsLessonInJournal(Lesson _lesson)
    {
        return journal.CheckIsLessonActivated(_lesson.LessonId);
    }

    private void AddLessonToJournal(Lesson _lesson)
    {
        journal.ActivateLesson(_lesson.LessonId);
        Debug.Log("Lesson added to journal");
    }

    public void SetFocus(LessonViewPoint _focuspoint)
    {
        focusCamera.Target.TrackingTarget = _focuspoint.FocusTransform;
        orbitalFollow.Radius = _focuspoint.Radius;
    }
}