using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class Lesson : MonoBehaviour
{
    public LessonViewPoint[] FocusPoints;
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    public int LessonId;

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
}

[Serializable]
public struct LessonViewPoint
{
    public Transform FocusTransform;
    public float Radius;
}