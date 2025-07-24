using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : Window
{
    private HUDWindow hudWindow => FindFirstObjectByType<HUDWindow>();

    public Toggle ControlsGuideToggle;

    private void Awake()
    {
        ControlsGuideToggle.onValueChanged.AddListener(ToggleControlsGuide);
    }

    private void ToggleControlsGuide(bool _state)
    {
        hudWindow.ShowControlsGuide(!_state);
    }
}