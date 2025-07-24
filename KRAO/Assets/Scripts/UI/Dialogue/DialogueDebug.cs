using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DialogueBox))]
public class DialogueDebug : MonoBehaviour
{
    private DialogueBox dialogueBox => GetComponent<DialogueBox>();
    [SerializeField] private Dialogue dialogueToDisplay;
    void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            dialogueBox.ShowDialogue(dialogueToDisplay);
        }
    }
}
