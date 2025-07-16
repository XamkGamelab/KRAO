using UnityEngine;

public class HUDWindow : Window
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    /*private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();


    void Update()
    {
        if (canvasGroup.alpha == 1)
        {
            playerManager.ToggleControllerState(true);
        }
        else
        {
            playerManager.ToggleControllerState(false);
        }
    }*/

    private void Start()
    {
        playerManager.ToggleControllerState(isOpen);
    }
}