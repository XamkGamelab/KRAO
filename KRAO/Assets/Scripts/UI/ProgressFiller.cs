using UnityEngine;
using UnityEngine.UI;

public class ProgressFiller : MonoBehaviour
{
    [SerializeField] Image fillImage;

    public void ShowProgress(float fillAmount)
    {
        fillImage.enabled = true;
        fillImage.fillAmount = fillAmount;
    }

    public void ResetProgress()
    {
        fillImage.enabled = false;
        fillImage.fillAmount = 0f;
    }
}
