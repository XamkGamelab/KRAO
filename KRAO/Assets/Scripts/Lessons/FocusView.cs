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

        ToggleCursor(lookAction.IsPressed());
    }

    public void ToggleFocusView()
    {
        focusViewEnabled = !focusViewEnabled;
        if(focusViewEnabled)
        {
            ResetOribtalCameraValues();
        }
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

    private void ResetOribtalCameraValues()
    {
        orbitalFollow.HorizontalAxis.Value = orbitalFollow.HorizontalAxis.Center;
        orbitalFollow.VerticalAxis.Value = orbitalFollow.VerticalAxis.Center;
        orbitalFollow.RadialAxis.Value = orbitalFollow.RadialAxis.Center;
    }

    private void ToggleCursor(bool _state)
    {
        if (IsPointerOverUIElement())
        {
            ToggleCinemachineInput(false);
            return;
        }

        ToggleCinemachineInput(_state);
        CursorLockMode _mode = _state ? CursorLockMode.Locked : CursorLockMode.None;
        cursorManager.SetCursorState(_mode);
    }

    private void ToggleCinemachineInput(bool _state)
    {
        foreach(var controller in cinemachineInput.Controllers)
        {
            controller.Enabled = _state;
        }
    }

    #region UI Detection
    public bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    #endregion
}