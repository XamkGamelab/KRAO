using UnityEngine;
using UnityEngine.UI;

public class JournalDropdownButton : MonoBehaviour
{
    private Button journalButton => GetComponent<Button>();
    private JournalWindow journal => FindAnyObjectByType<JournalWindow>();
    private Text headerText => GetComponentInChildren<Text>();
    
    public int LessonId;
    public Image CheckMarkImg;
    [SerializeField] private Sprite BasicBackground;
    [SerializeField] private Sprite HighlightedBackground;

    private void Awake()
    {
        journalButton.interactable = false;
        CheckMarkImg.gameObject.SetActive(false);

        journalButton.onClick.AddListener(HandleJournalButtonClick);
    }

    public void HandleJournalButtonClick()
    {
        journal.SetLessonTexts(LessonId);
        journal.dropdownButtons.ForEach(b => b.journalButton.image.sprite = BasicBackground);
        journalButton.image.sprite = HighlightedBackground;
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