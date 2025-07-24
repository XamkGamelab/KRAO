using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueBox : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TypewriterEffect dialogueTypewriter;

    private Dialogue NextDialogue;
    void Start()
    {
        dialogueTypewriter.DialogueBox = this;
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        if (!IsDialogueBoxVisible())
        {
            canvasGroup.alpha = 1.0f;
        }

        speakerText.text = dialogue.speakerName;
        dialogueTypewriter.SetText(dialogue.content);

        if(dialogue.NextDialogue != null)
        {
            NextDialogue = dialogue.NextDialogue;
        } else if(NextDialogue != null)
        {
            NextDialogue = null;
        }
    }

    public void FinishDialogue()
    {
        if(NextDialogue != null)
        {
            ShowDialogue(NextDialogue);
        } else
        {
            canvasGroup.alpha = 0f;
        }
    }

    private bool IsDialogueBoxVisible()
    {
        return canvasGroup.alpha != 0;
    }
}
