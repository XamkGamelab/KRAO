using UnityEngine;
using UnityEngine.UI;

public class JournalDropdownButton : MonoBehaviour
{
    private Button journalButton => GetComponent<Button>();
    private JournalWindow journal => FindAnyObjectByType<JournalWindow>();
    private Text headerText => GetComponentInChildren<Text>();

    public bool isOpen {  get; private set; }

    public int LessonId;
    public Image CheckMarkImg;

    private void Awake()
    {
        journalButton.interactable = false;
        CheckMarkImg.gameObject.SetActive(false);

        journalButton.onClick.AddListener(HandleJournalButtonClick);
    }

    public void ToggleOpenState(bool _state)
    {
        isOpen = _state;
    }

    public void HandleJournalButtonClick()
    {
        //set texts
        journal.SetLessonTexts(LessonId);
        //set isOpen state
        journal.dropdownButtons.ForEach(b => b.ToggleOpenState(false));
        isOpen = true;
        //reset scrollbar
        journal.ResetScrollbar();
    }

    public void SetValues(int _id, string _header)
    {
        LessonId = _id;
        headerText.text = _header;
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