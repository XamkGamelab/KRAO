using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    private Slider mouseSensitivitySlider => GetComponentInChildren<Slider>();
    private Look look => FindFirstObjectByType<Look>();
    private FocusView focusView => FindFirstObjectByType<FocusView>();

    private void Awake()
    {
        // Listen to slider value changes
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    // Set camera sensitivities
    private void SetMouseSensitivity(float _sensitivity)
    {
        look.lookSensitivity = _sensitivity*10;                    // player
        focusView.SetFocusViewCameraSensitivity(_sensitivity*10);  // focus view
    }
}