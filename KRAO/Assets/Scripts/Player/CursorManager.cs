using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public CursorLockMode CurrentMode;

    private void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.stickyCursorLock = true;
        #endif
    }
    public void SetCursorState(CursorLockMode lockMode)
    {
        Debug.Log($"Cursor mode set to {lockMode}");
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
