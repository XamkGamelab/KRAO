using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsQualitySettings : MonoBehaviour
{
    private Dropdown graphicsDropdown => GetComponentInChildren<Dropdown>();

    private void Awake()
    {
        graphicsDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);
    }

    private void ChangeGraphicsQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }
}