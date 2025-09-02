using UnityEngine;
using UnityEngine.UI;

// Attached to settings sliders that need to show min, max and current values as text
public class SettingsScrollBar : MonoBehaviour
{
    private Slider slider => GetComponent<Slider>();
    private Text sliderValueText => slider.handleRect.GetComponentInChildren<Text>();
    // Set in editor
    public Text MaxValueText;
    public Text MinValueText;

    private void Awake()
    {
        // Listen to slider value changes
        slider.onValueChanged.AddListener(ShowSliderValue);
        // Set slider to half value as default
        slider.value = 0.5f;
        ShowSliderValue(slider.value);
        // Set/show min and max values
        MaxValueText.text = (slider.maxValue * 100).ToString();
        MinValueText.text = (slider.minValue * 100).ToString();
    }

    // Show slider value as text on the slider handle
    // Add a legacy UI text as child of slider handle
    private void ShowSliderValue(float _value)
    {
        float _fValue = _value*100;
        sliderValueText.text = ((int)(_fValue)).ToString();
    }
}
// Values are multiplied by 100 because the slider range is 0-1