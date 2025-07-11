using UnityEngine;
using UnityEngine.UI;

public class Journal : Window
{
    private GameObject LessonButtonsContainer => GetComponentInChildren<VerticalLayoutGroup>().gameObject;

    public Text HeaderText;
    public Text LessonText;

    public GameObject JournalButtonPrefab;
    private JournalButton journalButton => JournalButtonPrefab.GetComponent<JournalButton>();

    public void AddNewLessonButton(string _header, string _content)
    {
        journalButton.HeaderText.text = _header;
        journalButton.LessonContentText = _content;
        JournalButtonPrefab.name = _header;

        Instantiate(JournalButtonPrefab, LessonButtonsContainer.transform);
    }
}