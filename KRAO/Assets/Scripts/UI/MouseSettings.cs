using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    private Slider mouseSensitivitySlider => GetComponentInChildren<Slider>();
    private Look look => FindFirstObjectByType<Look>();

    private FocusView focusView => FindFirstObjectByType<FocusView>();

    private void Awake()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    private void Update()
    {
        // Disable focus view camera movement when over EventSystem object
        if (EventSystem.current.IsPointerOverGameObject())
        {
            focusView.SetFocusViewCameraSensitivity(0);
        }
        else
        {
            focusView.SetFocusViewCameraSensitivity(mouseSensitivitySlider.value);
        }
    }

    private void SetMouseSensitivity(float _sensitivity)
    {
        look.lookSensitivity = _sensitivity;                    // player
        focusView.SetFocusViewCameraSensitivity(_sensitivity);  // focus view
    }
}