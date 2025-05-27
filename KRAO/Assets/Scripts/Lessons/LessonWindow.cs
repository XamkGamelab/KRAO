using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LessonWindow : MonoBehaviour
{
    public GameObject InteractionPrompt => GetComponentInChildren<Text>().gameObject;
    public GameObject ContentBox => GetComponentInChildren<ScrollRect>().gameObject;
    public Text[] Texts => ContentBox.GetComponentsInChildren<Text>().ToArray();

    public void SetTexts(string _header, string _content)
    {
        Texts[0].text = _header;
        Texts[1].text = _content;
    }
}
