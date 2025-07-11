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
    public string HeaderText;
    [TextArea(15,20)] public string ContentText;

    public bool NewLessonFound { get; private set; } = true;
    private bool lessonOpen = false;

    public static event Action<Lesson> OnLessonOpened;
    public static event Action<Lesson> OnLessonClosed;

    public void ToggleLesson()
    {
        lessonOpen = !lessonOpen;

        if(lessonOpen)
        {
            lessonWindow.SetTexts(HeaderText, ContentText);
            lessonWindow.ResetScrollbox();
            OnLessonOpened?.Invoke(this);
        }
        else
        {
            NewLessonFound = false;
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