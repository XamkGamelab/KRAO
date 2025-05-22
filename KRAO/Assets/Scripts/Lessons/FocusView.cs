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