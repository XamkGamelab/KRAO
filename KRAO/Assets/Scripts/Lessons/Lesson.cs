using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lesson : MonoBehaviour
{
    public LessonViewPoint[] FocusPoints;
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    public int LessonId;

    public LessonFeature[] LessonFeatures;

    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();

    private bool lessonOpen = false;

    public static event Action<Lesson> OnLessonOpened;
    public static event Action<Lesson> OnLessonClosed;

    [SerializeField] private ParticleSystem unopenedParticles;

    private void Start()
    {
        if(!lessonManager.CheckIsLessonInJournal(this))
        {
            unopenedParticles.Play();
        }
    }

    public void ToggleLesson()
    {
        lessonOpen = !lessonOpen;

        if (unopenedParticles.isPlaying)
        {
            unopenedParticles.Stop();
        }

        if(lessonOpen)
        {
            lessonWindow.SetTexts(LessonId);
            //lessonWindow.ResetScrollbox();
            OnLessonOpened?.Invoke(this);
        }
        else
        {
            OnLessonClosed?.Invoke(this);
        }
    }

    public bool HasMultipleFocusPoints()
    {
        return FocusPoints.Length > 1;
    }

    public bool HasLessonFeatures()
    {
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