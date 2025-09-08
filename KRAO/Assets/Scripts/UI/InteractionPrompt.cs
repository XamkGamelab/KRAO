using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text interactionPrompt;
    [SerializeField] private CanvasGroup interactionCanvasGroup;
    [SerializeField] private float fadeTime = 0.5f;

    private bool promptActive = false;

    private InputAction interact;

    private void Update()
    {
        if(interactionCanvasGroup.alpha != GetInteractionPromptTargetAlpha(promptActive))
        {
            interactionCanvasGroup.alpha = Mathf.MoveTowards(interactionCanvasGroup.alpha, GetInteractionPromptTargetAlpha(promptActive), Time.deltaTime / fadeTime);
        }
    }

    public void ShowPrompt(string prompt, InteractionType interactionType)
    {
        string _prompt = GetPromptFromType(interactionType) + prompt;
        if(interactionPrompt.text != _prompt)
        {
            interactionPrompt.text = _prompt;
        }

        promptActive = true;
    }

    public void HidePrompt()
    {
        promptActive = false;
    }

    private float GetInteractionPromptTargetAlpha(bool state)
    {
        return state ? 1f : 0f;
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
