using Unity.Cinemachine;
using UnityEngine;

public class FocusView : MonoBehaviour
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    private CinemachineCamera rotatingCamera => GetComponent<CinemachineCamera>();
    private CinemachineInputAxisController cinemachineInput => rotatingCamera.GetComponent<CinemachineInputAxisController>();
    
    private bool focusViewEnabled = false;

    public void ToggleFocusView()
    {
        focusViewEnabled = !focusViewEnabled;
        playerManager.ToggleControllerState();
        rotatingCamera.enabled = focusViewEnabled;
    }

    public void SetFocusViewCameraSensitivity(float _sensitivity)
    {
        // Look Orbit X
        cinemachineInput.Controllers[0].Input.Gain = _sensitivity;
        // Look Orbit Y
        cinemachineInput.Controllers[1].Input.Gain = -_sensitivity;
    }
}