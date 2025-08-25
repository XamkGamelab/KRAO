using Unity.Cinemachine;
using UnityEngine;
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
    private LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    public Lesson openLesson;    // Lesson that is currently open

    #region private methods
    private void Start()
    {
        // Start listening to lesson opened/closed events
        Lesson.OnLessonOpened += HandleLessonOpened;
        Lesson.OnLessonClosed += HandleLessonClosed;
    }

    // Called when lesson is closing
    private void HandleLessonClosed(Lesson _lesson)
    {
        // If lesson has multiple FocusPoints, disable the UI to swap between them
        if (_lesson.HasMultipleFocusPoints())
        {
            lessonFocusPoints.DisableFocusPoints();
        }
        // If the lesson has LessonFeatures, disable the buttons for them
        if (_lesson.HasLessonFeatures())
        {
            lessonFeatures.DisableLessonFeatures();
        }
        // Close FocusView
        focusView.ToggleFocusView();
        // Make openLesson null (because no lesson is open)
        openLesson = null;
    }

    // Called when lesson is opening
    private void HandleLessonOpened(Lesson _lesson)
    {
        openLesson = _lesson;
        // Set lessonObject as tracking target
        SetFocus(_lesson.FocusPoints[0]);

        // Reset FocusView Rotation and open FocusView
        focusView.ResetOrbitalCameraValues();
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

        // Open LessonWindow
        lessonWindow.OpenWindow();

        // If _lesson has not been previously found (IsNew)
        if (lessonTracker.LessonItemById(_lesson.LessonId).IsNew)
        {
            // Activate it in the Journal
            journal.ActivateLesson(_lesson.LessonId);
            Debug.Log("Lesson added to journal");
            // In LessonTracker, set it as found and update all trackers (progress bar, journal, scene selection)
            lessonTracker.AddLessonToTracker(SceneManager.GetActiveScene().buildIndex, _lesson.LessonId);
        }
    }
    #endregion

    #region public methods
    // Check if lesson is found
    public bool CheckIsLessonFound(Lesson _lesson)
    {
        bool _returnValue = false;

        if (!lessonTracker.LessonItemById(_lesson.LessonId).IsNew)
        {
            _returnValue = true;
        }

        return _returnValue;
    }

    // Close current open lesson
    public void CloseLesson()
    {
        openLesson.ToggleLesson();
    }

    // Set FocusView camera's focus to _focuspoint
    public void SetFocus(LessonViewPoint _focuspoint)
    {
        // Set focus transform
        focusCamera.Target.TrackingTarget = _focuspoint.FocusTransform;
        // Set orbit radius
        orbitalFollow.Radius = _focuspoint.Radius;
    }
    #endregion
}