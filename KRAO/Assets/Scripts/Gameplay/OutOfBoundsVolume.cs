using UnityEngine;

public class OutOfBoundsVolume : MonoBehaviour
{
    private OutOfBoundsWarning warning => FindFirstObjectByType<OutOfBoundsWarning>();
    public void ActivateWarning()
    {
        warning.ShowWarning();
    }

    public void DeactivateWarning()
    {
        warning.HideWarning();
    }
}
