using UnityEngine;
using UnityEngine.UI;

/*
 * Look into making two separate graphics modes (Low and High). Should affect Lighting settings,
 * max texture resolution settings. Unity can automate some of it, but some things
 * (dynamic lights in scene) might have to be programmed by hand.
*/
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
