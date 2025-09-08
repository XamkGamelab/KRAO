using UnityEngine;

public class OutOfBoundsWarning : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    public void ShowWarning()
    {
        canvasGroup.alpha = 1;
    }

    public void HideWarning()
    {
        canvasGroup.alpha = 0;
    }
}
