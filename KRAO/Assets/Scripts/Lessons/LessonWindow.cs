using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LessonWindow : Window
{
    public GameObject InteractionPrompt => GetComponentInChildren<Text>().gameObject;
    public GameObject ContentBox => GetComponentInChildren<ScrollRect>().gameObject;
    public Text[] Texts => ContentBox.GetComponentsInChildren<Text>().ToArray();

    private Scrollbar scrollbar => GetComponentInChildren<Scrollbar>();

    public void SetTexts(string _header, string _content)
    {
        Texts[0].text = _header;
        Texts[1].text = _content;
    }

    public void ResetScrollbox()
    {
        scrollbar.value = 1f;
    }
}
