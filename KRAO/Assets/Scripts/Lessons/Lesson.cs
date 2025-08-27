using UnityEngine;
using System;
using UnityEngine.UI;

public class Lesson : MonoBehaviour
{
    private LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();
    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();

    public static event Action<Lesson> OnLessonOpened;
    public static event Action<Lesson> OnLessonClosed;

    public bool lessonOpen { get; private set; } = false;      // is lesson open

    #region set in editor
    // Id to reference a specific lesson (Lesson and LessonTracker's corresponding LessonItem need to have same ids)
    public int LessonId;

    // Particle effect for unopened lesson
    [SerializeField] private ParticleSystem unopenedParticles;

    public LessonViewPoint[] FocusPoints;
    public LessonFeature[] LessonFeatures;
    #endregion

    private void Start()
    {
        // If lesson is not in journal (has not been opened/found), play particles
        if (!lessonManager.CheckIsLessonFound(this))
        {
            unopenedParticles.Play();
        }
    }

    public void ToggleLesson()
    {
        // Switch opened state
        lessonOpen = !lessonOpen;

        // If particles are playing, stop them
        if (unopenedParticles.isPlaying)
        {
            unopenedParticles.Stop();
        }

        // Lesson was opened
        if(lessonOpen)
        {
            // Set correct lesson texts to lessonWindow
            lessonWindow.SetTexts(LessonId);
            // Invoke OnLessonOpened (implemented in LessonManager)
            OnLessonOpened?.Invoke(this);
        }
        // Lesson was closed
        else
        {
            // Invoke OnLessonClosed (implemented in LessonManager)
            OnLessonClosed?.Invoke(this);
        }
    }

    // Check if lesson has multiple FocusPoints
    public bool HasMultipleFocusPoints()
    {
        // return true if over 1 FocusPoint in FocusPoints array
        return FocusPoints.Length > 1;
    }

    // Check if lesson has any LessonFeatures
    public bool HasLessonFeatures()
    {
        // return true if LessonFeatures array has anything inside
        return LessonFeatures.Length > 0;
    }
}


// Struct for different camera angles
[Serializable]
public struct LessonViewPoint
{
    // Transform the FocusView camera will focus on
    public Transform FocusTransform;
    // Orbit radius
    public float Radius;
}

// Struct for different lesson features (currently just animation)
[Serializable]
public struct LessonFeature
{
    // What happens when button is pressed
    public Button.ButtonClickedEvent ButtonEvent;
    // Text to describe what the Feature does (LessonFeatureTooltip)
    public string ButtonPrompt;
}