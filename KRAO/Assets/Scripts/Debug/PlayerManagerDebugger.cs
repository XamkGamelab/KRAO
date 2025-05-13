using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerDebugger : MonoBehaviour
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            playerManager.ToggleControllerState();
        }
    }
}
