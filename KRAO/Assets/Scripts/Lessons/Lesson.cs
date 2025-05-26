using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class Lesson : MonoBehaviour
{
    //public GameObject ContentBox => GetComponentInChildren<ScrollRect>().gameObject;
    public GameObject LessonFocusObject;
    public float FocusRadius = 4f;
    //private GameObject interactionPrompt => GetComponentInChildren<Text>().gameObject;
    //private Text[] texts => ContentBox.GetComponentsInChildren<Text>().ToArray();
    public LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();
    
    public string HeaderText;
    [TextArea(15,20)] public string ContentText;

    public bool NewLessonFound { get; private set; } = true;
    private bool triggered = false;
    private bool lessonOpen = false;

    private Collider player;

    public static event Action<Lesson> OnLessonOpened;
    public static event Action<Lesson> OnLessonClosed;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponentInChildren<CharacterController>();

        //lessonWindow.SetTexts(HeaderText, ContentText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            // Show interaction prompt
            ToggleInteractionPrompt(1);
            triggered = true;

            lessonWindow.SetTexts(HeaderText, ContentText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            // Hide interaction prompt
            ToggleInteractionPrompt(0);

            triggered = false;
        }
    }

    private bool IsPlayer(Collider collider)
    {
        return collider == player;
    }

    private void ToggleInteractionPrompt(int _alpha)
    {
        lessonWindow.InteractionPrompt.GetComponent<CanvasGroup>().alpha = _alpha;
    }

    private void Update()
    {
        // Get player inputs when triggered
        if (triggered && Keyboard.current.eKey.wasPressedThisFrame && !lessonOpen)
        {
            lessonOpen = true;

            // Hide interaction prompt
            ToggleInteractionPrompt(0);

            OnLessonOpened?.Invoke(this);
        }

        else if (triggered && Keyboard.current.escapeKey.wasPressedThisFrame && lessonOpen)
        {
            lessonOpen = false;
            NewLessonFound = false;

            // Show interaction prompt
            ToggleInteractionPrompt(1);

            OnLessonClosed?.Invoke(this);
        }
    }

    public void ToggleLesson()
    {
        lessonOpen = !lessonOpen;

        if(lessonOpen)
        {
            lessonWindow.SetTexts(HeaderText, ContentText);
            OnLessonOpened?.Invoke(this);
        } else
        {
            NewLessonFound = false;
            OnLessonClosed?.Invoke(this);
        }
    }
}