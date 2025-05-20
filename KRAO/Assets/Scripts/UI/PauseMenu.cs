using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    private PlayerManager playerManager;

    private InputAction menu;

    private bool menuOn = true;
    private void Start()
    {
        menu = InputSystem.actions.FindAction("Menu");
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        SetCanvasState(menuOn);
    }
    private void Update()
    {
        if (menu.WasPressedThisFrame())
        {
            ToggleMenuState();
        }
    }

    public void ToggleMenuState()
    {
        menuOn = !menuOn;
        playerManager.ToggleControllerState(!menuOn);
        SetCanvasState(menuOn);
    }

    private void SetCanvasState(bool state)
    {
        canvasGroup.alpha = state ? 1f : 0f;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }
}
