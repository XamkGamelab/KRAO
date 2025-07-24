using UnityEngine;
using UnityEngine.UI;

public class LessonProgressBar : MonoBehaviour
{
    private Slider slider => GetComponentInChildren<Slider>();
    private Text lessonsFoundText => slider.GetComponentInChildren<Text>();

    public void ChangeSliderValue(int _value, int _maxValue)
    {
        //slider.fillRect.
        slider.value = _value;
        slider.maxValue = _maxValue;
        lessonsFoundText.text = _value + "/" + _maxValue;

        Debug.Log("progress bar updated");
    }
}