using UnityEngine;
using UnityEngine.UI;

public class GraphicsQualitySettings : MonoBehaviour
{
    private Dropdown graphicsDropdown => GetComponentInChildren<Dropdown>();

    private void Awake()
    {
        // Listen to dropdown value changes
        graphicsDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);
    }

    // Change graphics/rendering quality
    // Different levels set in Quality Settings (Project Settings)
    private void ChangeGraphicsQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }
}