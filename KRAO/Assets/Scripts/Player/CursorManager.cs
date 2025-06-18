using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public CursorLockMode CurrentMode;
    public void SetCursorState(CursorLockMode lockMode)
    {
        if(lockMode == CurrentMode)
        {
            return;
        }

        CurrentMode = lockMode;
        Cursor.lockState = CurrentMode;

        switch (lockMode)
        {
            case CursorLockMode.None:
                Cursor.visible = true;
                break;
            case CursorLockMode.Locked:
                Cursor.visible = false;
                break;
            case CursorLockMode.Confined:
                Cursor.visible = true;
                break;
        }
    }
}
