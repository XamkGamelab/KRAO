using UnityEngine;
using UnityEngine.UI;

public class LessonProgressBar : MonoBehaviour
{
    private Slider slider => GetComponentInChildren<Slider>();
    private Text lessonsFoundText => slider.GetComponentInChildren<Text>();

    // Change slider fill and lesson found text, set max value 
    public void ChangeSliderValue(int _value, int _maxValue)
    {
        slider.maxValue = _maxValue;
        lessonsFoundText.text = _value + "/" + _maxValue;
        slider.value = _value;
    }
}