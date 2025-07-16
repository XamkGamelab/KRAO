using System;
using UnityEngine;
using UnityEngine.UI;

public class JournalButton : MonoBehaviour
{
    private Button journalButton => GetComponent<Button>();
    private Journal journal => FindAnyObjectByType<Journal>();
    public Text HeaderText => GetComponentInChildren<Text>();
    public string LessonContentText;

    private void Awake()
    {
        journalButton.onClick.AddListener(HandleJournalButtonClick);
    }

    private void HandleJournalButtonClick()
    {
        journal.HeaderText.text = HeaderText.text;
        journal.LessonText.text = LessonContentText;
    }
}