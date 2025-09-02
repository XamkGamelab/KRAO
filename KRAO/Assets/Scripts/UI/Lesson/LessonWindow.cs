using UnityEngine;
using UnityEngine.UI;

public class LessonWindow : Window
{
    public Text HeaderText;
    public Text ContentText;

    // GameObject with guides on how to use the lesson/focusview camera
    [SerializeField] private GameObject ControlsGuide;

    private Scrollbar scrollbar => GetComponentInChildren<Scrollbar>();
    private LessonTracker lessonTracker => FindFirstObjectByType<LessonTracker>();

    // Set texts for the lesson with a lessonId value
    public void SetTexts(int _lessonId)
    {
        // Get header and content texts from lesson tracker (lesson item with the correct id)
        HeaderText.text = lessonTracker.LessonItemById(_lessonId).HeaderText;
        ContentText.text = lessonTracker.LessonItemById(_lessonId).ContentText;
        // Set scrollbar to the top
        StartCoroutine(ResetScrollbar(scrollbar));
    }

    // Set the controls guide as visible/invisible depending on _state value
    public void ShowControlsGuide(bool _state)
    {
        ControlsGuide.SetActive(_state);
    }
}