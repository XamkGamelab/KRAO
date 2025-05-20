using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private Movement movement => GetComponentInChildren<Movement>();
    private Look look => GetComponentInChildren<Look>();

    [SerializeField] private Transform playerTransform;
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

    public void TransportPlayer(Vector3 position, Quaternion rotation)
    {
        bool wasControlEnabled = controllerEnabled;
        controllerEnabled = false;
        playerTransform.position = position;
        playerTransform.rotation = rotation;
        controllerEnabled = wasControlEnabled;
    }
    #endregion
    #region Private Functions
    private void Start()
    {
        CheckComponents();
        Transform newSpawn = GameObject.FindWithTag("Bootstrapper").GetComponent<SceneBootstrapper>().Spawnpoint;
        TransportPlayer(newSpawn.position, newSpawn.rotation);
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
