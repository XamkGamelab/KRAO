using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    private InputAction menu;

    private bool menuOn = false;
    private void Start()
    {
        menu = InputSystem.actions.FindAction("Menu");
        SetCanvasState(menuOn);
    }
    private void Update()
    {
        if (menu.WasPressedThisFrame())
        {
            ToggleMenuState();
        }
    }

    private void ToggleMenuState()
    {
        menuOn = !menuOn;
        SetCanvasState(menuOn);
    }

    private void SetCanvasState(bool state)
    {
        canvasGroup.alpha = state ? 1f : 0f;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }
}
