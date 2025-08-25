using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FocusView : MonoBehaviour
{
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    private CursorManager cursorManager => playerManager.CursorManager;
    private CinemachineCamera rotatingCamera => GetComponent<CinemachineCamera>();
    private CinemachineOrbitalFollow orbitalFollow => GetComponent<CinemachineOrbitalFollow>();
    private CinemachineInputAxisController cinemachineInput => rotatingCamera.GetComponent<CinemachineInputAxisController>();
    
    private bool focusViewEnabled = false;
    [SerializeField] private InputAction lookAction => InputSystem.actions.FindAction("Click");

    private void Update()
    {
        if(!focusViewEnabled)
        {
            return;
        }
        // Update mouse input behavior - control camera if mouse left is pressed...
        ToggleCursor(lookAction.IsPressed());
    }

    // Turn FocusView on/off
    public void ToggleFocusView()
    {
        // Switch enabled state
        focusViewEnabled = !focusViewEnabled;
        // Switch controller state
        playerManager.ToggleControllerState();
        // Switch between FocusView camera and Player camera
        rotatingCamera.enabled = focusViewEnabled;
    }

    // Set camera sensitivity in each axis
    public void SetFocusViewCameraSensitivity(float _sensitivity)
    {
        // Look Orbit X
        cinemachineInput.Controllers[0].Input.Gain = _sensitivity;
        // Look Orbit Y
        cinemachineInput.Controllers[1].Input.Gain = -_sensitivity;
        // Zoom
        cinemachineInput.Controllers[2].Input.Gain = -_sensitivity;
    }

    public void ResetOrbitalCameraValues()
    {
        orbitalFollow.HorizontalAxis.Value = orbitalFollow.HorizontalAxis.Center;
        orbitalFollow.VerticalAxis.Value = orbitalFollow.VerticalAxis.Center;
        orbitalFollow.RadialAxis.Value = orbitalFollow.RadialAxis.Center;
    }

    private void ToggleCursor(bool _state)
    {
        // If mouse is over UI, don't control camera
        if (IsPointerOverUIElement())
        {
            ToggleCinemachineInput(false);
            return;
        }
        // Enable/disable camera inputs
        ToggleCinemachineInput(_state);
        // Lock/unlock cursor (hide+center / free to move+visible)
        CursorLockMode _mode = _state ? CursorLockMode.Locked : CursorLockMode.None;
        cursorManager.SetCursorState(_mode);
    }

    // Enable/disable camera inputs
    private void ToggleCinemachineInput(bool _state)
    {
        foreach(var controller in cinemachineInput.Controllers)
        {
            controller.Enabled = _state;
        }
    }

    #region UI Detection
    // Check if mouse pointer is over a UI element
    public bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    #endregion
}