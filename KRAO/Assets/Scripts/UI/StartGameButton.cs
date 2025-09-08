using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();
    private MenuManager menuManager => FindFirstObjectByType<MenuManager>();

    public void StartGame()
    {
        playerManager.StartGame();
        menuManager.CloseMainMenu();
    }
}
