using UnityEngine;

public class HUDWindow : Window
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();


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
    }
}