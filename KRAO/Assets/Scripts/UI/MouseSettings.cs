using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Mouse Sensitivity(Slider, 1f - 100f)

public class MouseSettings : MonoBehaviour
{
    private Slider mouseSensitivitySlider => GetComponentInChildren<Slider>();
    private Look look => FindFirstObjectByType<Look>();

    private void Awake()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    private void SetMouseSensitivity(float _sensitivity)
    {
        look.lookSensitivity = _sensitivity;
    }
}