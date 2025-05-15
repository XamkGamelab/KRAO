using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusView : MonoBehaviour
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    private bool focusViewEnabled = false;

    private CinemachineCamera rotatingCamera => GetComponent<CinemachineCamera>();

    public void ToggleFocusView()
    {
        focusViewEnabled = !focusViewEnabled;
        playerManager.ToggleControllerState();
        rotatingCamera.enabled = focusViewEnabled;
    }
}

/* Create a method for the Focus View to be activated using the Basic Interaction system.
 * When activated, Player Controller movement and camera rotation should be turned off,
 * and when leaving the "Focus View", movement and camera rotation should be turned back on.
 */