using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LessonWindow : Window
{
    public Text HeaderText;
    public Text ContentText;

    private Scrollbar scrollbar => GetComponentInChildren<Scrollbar>();

    public void SetTexts(string _header, string _content)
    {
        HeaderText.text = _header;
        ContentText.text = _content;
    }

    public void ResetScrollbox()
    {
        scrollbar.value = 1f;
    }
}
