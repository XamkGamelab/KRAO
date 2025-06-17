using UnityEngine;
using UnityEngine.UI;

public class JournalDropdownButton : MonoBehaviour
{
    private Button journalButton => GetComponent<Button>();
    private JournalManager journalManager => FindAnyObjectByType<JournalManager>();
    private Text headerText => GetComponentInChildren<Text>();
    public string contentText;
    public int LessonId;

    public Image CheckMarkImg;

    private void Awake()
    {
        journalButton.interactable = false;
        CheckMarkImg.gameObject.SetActive(false);

        journalButton.onClick.AddListener(HandleJournalButtonClick);
    }

    public void HandleJournalButtonClick()
    {
        journalManager.SetLessonTexts(headerText.text, contentText);
    }

    public void SetValues(int _id, string _header, string _content)
    {
        LessonId = _id;
        headerText.text = _header;
        contentText = _content;
    }

    public void ActivateButton()
    {
        // make button interactable
        journalButton.interactable = true;
        // show checkmark
        CheckMarkImg.gameObject.SetActive(true);
    }

    public bool IsInteractable()
    {
        return journalButton.interactable;
    }
}