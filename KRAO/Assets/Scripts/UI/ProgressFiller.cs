using UnityEngine;
using UnityEngine.UI;

public class ProgressFiller : MonoBehaviour
{
    [SerializeField] Image fillImage;

    public void ShowProgress(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }

    public void ResetProgress()
    {
        fillImage.fillAmount = 0f;
    }
}
