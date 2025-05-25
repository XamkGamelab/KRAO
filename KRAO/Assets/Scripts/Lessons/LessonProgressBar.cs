using UnityEngine;
using UnityEngine.UI;

public class LessonProgressBar : MonoBehaviour
{
    private Slider slider => GetComponentInChildren<Slider>();

    public void ChangeSliderValue(int _value, int _maxValue)
    {
        slider.value = _value;
        slider.maxValue = _maxValue;
    }
}