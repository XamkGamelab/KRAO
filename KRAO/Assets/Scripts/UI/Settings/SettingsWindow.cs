using UnityEngine.UI;

public class SettingsWindow : Window
{
    private HUDWindow hudWindow => FindFirstObjectByType<HUDWindow>();
    private LessonWindow lessonWindow => FindFirstObjectByType<LessonWindow>();

    public Toggle ControlsGuideToggle;

    private void Awake()
    {
        // Start listening to toggle clicks
        ControlsGuideToggle.onValueChanged.AddListener(ToggleControlsGuide);
    }

    // Show/hide guides in HUD and Lesson windows
    private void ToggleControlsGuide(bool _state)
    {
        hudWindow.ShowControlsGuide(!_state);
        lessonWindow.ShowControlsGuide(!_state);
    }
}