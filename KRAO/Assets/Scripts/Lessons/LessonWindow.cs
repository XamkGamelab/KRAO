using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LessonWindow : Window
{
    public Text HeaderText;
    public Text ContentText;

    private Scrollbar scrollbar => GetComponentInChildren<Scrollbar>();
    private LessonTracker lessonTracker => FindFirstObjectByType<LessonTracker>();

    public void SetTexts(int _lessonId)
    {
        HeaderText.text = lessonTracker.LessonItemById(_lessonId).HeaderText;
        ContentText.text = lessonTracker.LessonItemById(_lessonId).ContentText;
        StartCoroutine(ResetScrollbar(scrollbar));
    }
}