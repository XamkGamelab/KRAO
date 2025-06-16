using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    private Slider mouseSensitivitySlider => GetComponentInChildren<Slider>();
    private Look look => FindFirstObjectByType<Look>();

    private FocusView focusView => FindFirstObjectByType<FocusView>();

    private void Awake()
    {
        SetMouseSensitivity(mouseSensitivitySlider.value);
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    private void SetMouseSensitivity(float _sensitivity)
    {
        look.lookSensitivity = _sensitivity;                    // player
        focusView.SetFocusViewCameraSensitivity(_sensitivity);  // focus view
    }
}