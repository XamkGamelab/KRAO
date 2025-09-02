using UnityEngine;

public class HUDWindow : Window
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    [SerializeField] private GameObject ControlsGuide;
    [SerializeField] private GameObject WarningSign;

    private void Start()
    {
        // Set player controller state to HUD window's isOpen state
        playerManager.ToggleControllerState(isOpen);
    }

    // Show/Hide controls guide in HUD depending on _state
    public void ShowControlsGuide(bool _state)
    {
        ControlsGuide.SetActive(_state);
    }

    // Show/Hide warning sign in HUD depending on _state
    public void ShowWarningSign(bool _state)
    {
        WarningSign.SetActive(_state);
    }
}