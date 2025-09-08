using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CursorManager CursorManager => GetComponent<CursorManager>();
    private Movement movement => GetComponentInChildren<Movement>();
    private Look look => GetComponentInChildren<Look>();
    private Interaction interaction => GetComponentInChildren<Interaction>();

    public bool ControllerEnabled = false;
    public bool HasCertificate = false;
    public bool InMainMenu = true;

    public static event Action<PlayerManager> OnCertificateActivation;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private CinemachineCamera mainMenuCamera;
    [SerializeField] private CinemachineCamera gameCamera;

    private CinemachineCamera currentCamera;

    #region Public Functions
    public void ToggleControllerState()
    {
        ControllerEnabled = !ControllerEnabled;

        movement.MovementEnabled = ControllerEnabled;
        look.LookEnabled = ControllerEnabled;
        interaction.InteractionEnabled = ControllerEnabled;
        crosshair.SetActive(ControllerEnabled);

        CursorManager.SetCursorState(GetCursorLockModeFromControllerState(ControllerEnabled));
    }

    public void ToggleControllerState(bool state)
    {
        ControllerEnabled = state;

        movement.MovementEnabled = ControllerEnabled;
        look.LookEnabled = ControllerEnabled;
        interaction.InteractionEnabled = ControllerEnabled;
        crosshair.SetActive(ControllerEnabled);

        CursorManager.SetCursorState(GetCursorLockModeFromControllerState(ControllerEnabled));
    }

    public void TransportPlayer(Vector3 position, Quaternion rotation)
    {
        bool wasControlEnabled = ControllerEnabled;

        //set controller state to false
        ControllerEnabled = false;
        ToggleControllerState(ControllerEnabled);

        interaction.ClearInteractables();
        playerTransform.position = position;
        playerTransform.rotation = rotation;

        //set controller state to original state (state before transport)
        ControllerEnabled = wasControlEnabled;
        ToggleControllerState(ControllerEnabled);
    }

    public void StartGame()
    {
        ToggleControllerState(true);
        InMainMenu = false;
        SwitchCamera(gameCamera);
    }

    public void MainMenu()
    {
        ToggleControllerState(false);
        InMainMenu = true;
        SwitchCamera(mainMenuCamera);
    }

    public void ActivateCertificate()
    {
        HasCertificate = true;
        OnCertificateActivation?.Invoke(this);
    }
    #endregion
    #region Private Functions
    private void Start()
    {
        CheckComponents();
        Transform newSpawn = GameObject.FindWithTag("Bootstrapper").GetComponent<SceneBootstrapper>().Spawnpoint;
        TransportPlayer(newSpawn.position, newSpawn.rotation);
        currentCamera = mainMenuCamera;
    }

    private void SwitchCamera(CinemachineCamera _camera)
    {
        currentCamera = _camera;

        if(currentCamera == mainMenuCamera)
        {
            mainMenuCamera.enabled = true;
            gameCamera.enabled = false;
        } else
        {
            gameCamera.enabled = true;
            mainMenuCamera.enabled = false;
        }
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
            Debug.LogError($"PlayerManager missing {_missingComponents} components:{_errorMessage}");
        }
    }

    private CursorLockMode GetCursorLockModeFromControllerState(bool state)
    {
        if(state)
        {
            return CursorLockMode.Locked;
        } else
        {
            return CursorLockMode.None;
        }
    }
    #endregion
}
