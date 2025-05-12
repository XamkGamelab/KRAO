using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Movement movement => GetComponentInChildren<Movement>();
    private Look look => GetComponentInChildren<Look>();

    private bool controllerEnabled = false;

    #region Public Functions
    public void ToggleControllerState()
    {
        controllerEnabled = !controllerEnabled;

        Debug.Log($"Controller state switched to {controllerEnabled}");

        movement.MovementEnabled = controllerEnabled;
        look.LookEnabled = controllerEnabled;

        GetCursorLockModeFromControllerState(controllerEnabled);
    }
    #endregion
    #region Private Functions
    private void Start()
    {
        CheckComponents();
    }

    private void CheckComponents()
    {
        string _errorMessage = string.Empty;
        int _missingComponents = 0;

        if(movement == null)
        {
            _errorMessage += " [Movement]";
            _missingComponents++;
        }

        if(look == null)
        {
            _errorMessage += " [Look]";
            _missingComponents++;
        }

        if(_missingComponents > 0)
        {
            Debug.LogError($"PlayerManager missing {_missingComponents.ToString()} components:{_errorMessage}");
        }
    }

    private CursorLockMode GetCursorLockModeFromControllerState(bool state)
    {
        if(state)
        {
            Cursor.visible = false;
            return CursorLockMode.Locked;
        } else
        {
            Cursor.visible = true;
            return CursorLockMode.None;
        }
    }
    #endregion
}
