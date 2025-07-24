using UnityEngine;

public class HUDWindow : Window
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    [SerializeField] private GameObject ControlsGuide;
    [SerializeField] private GameObject WarningSign;

    private void Start()
    {
        playerManager.ToggleControllerState(isOpen);
    }

    public void ShowControlsGuide(bool _state)
    {
        ControlsGuide.SetActive(_state);
    }
    
    public void ShowWarningSign(bool _state)
    {
        WarningSign.SetActive(_state);
    }
}