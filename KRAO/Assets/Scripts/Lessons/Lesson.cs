using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lesson : MonoBehaviour
{
    private LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();
    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();

    public static event Action<Lesson> OnLessonOpened;
    public static event Action<Lesson> OnLessonClosed;

    public LessonViewPoint[] FocusPoints;
    public LessonFeature[] LessonFeatures;

    private bool lessonOpen = false;       // is lesson open

    // SET IN EDITOR
    // Id to reference a specific lesson (Lesson and LessonTracker's corresponding LessonItem need to have same ids)
    public int LessonId { get; private set; }

    // Particle effect for unopened lesson
    [SerializeField] private ParticleSystem unopenedParticles;

    private void Start()
    {
        // If lesson is not in journal (has not been opened/found), play particles
        if (!lessonManager.CheckIsLessonInJournal(this))
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
        // return true if over 1 FocusPoint in FocusPoints list
        return FocusPoints.Length > 1;
    }

    // Check if lesson has any LessonFeatures
    public bool HasLessonFeatures()
    {
        // return true if LessonFeatures list has anything inside
        return LessonFeatures.Length > 0;
    }
}

[Serializable]
public struct LessonViewPoint
{
    public Transform FocusTransform;
    public float Radius;
}

[Serializable]
public struct LessonFeature
{
    public Button.ButtonClickedEvent ButtonEvent;
    public string ButtonPrompt;
}