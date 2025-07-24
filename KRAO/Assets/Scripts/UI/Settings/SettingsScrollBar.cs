using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScrollBar : MonoBehaviour
{
    private Slider slider => GetComponent<Slider>();
    private Text sliderValueText => slider.handleRect.GetComponentInChildren<Text>();
    public Text MaxValueText;
    public Text MinValueText;

    private void Awake()
    {
        slider.onValueChanged.AddListener(ShowSliderValue);
        slider.value = 0.5f;
        ShowSliderValue(slider.value);
        MaxValueText.text = ((int)(slider.maxValue * 100)).ToString();
        MinValueText.text = ((int)(slider.minValue * 100)).ToString();
    }

    private void ShowSliderValue(float _value)
    {
        sliderValueText.text = ((int)(_value*100)).ToString();
    }
}