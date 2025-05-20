using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionPrompt;

    private InputAction interact;

    public void ShowPrompt(string prompt, InteractionType interactionType)
    {
        string _prompt = GetPromptFromType(interactionType) + prompt;
        if(interactionPrompt.text != _prompt)
        {
            interactionPrompt.text = _prompt;
        }

        if (!interactionPrompt.enabled)
        {
            interactionPrompt.enabled = true;
        }
    }

    public void HidePrompt()
    {
        if (interactionPrompt.enabled)
        {
            interactionPrompt.enabled = false;
        }
    }

    private string GetPromptFromType(InteractionType type)
    {
        string _returnValue = string.Empty;
        switch (type)
        {
            case InteractionType.Press:
                _returnValue = "Paina 'E' ";
                break;
            case InteractionType.Hold:
                _returnValue = "Pidä 'E' pohjassa ";
                break;
            case InteractionType.Stay:
                break;
        }
        return _returnValue;
    }
}
