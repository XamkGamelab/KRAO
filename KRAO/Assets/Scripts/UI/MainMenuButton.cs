using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    public void MainMenu()
    {
        playerManager.MainMenu();
    }
}
