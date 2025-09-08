using Unity.Cinemachine;
using UnityEngine;

public class MainMenuCameraManager : MonoBehaviour
{
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    [SerializeField] private float cameraSwitchTime = 10f;

    private bool wasActive = true;

    private Transform[] mainMenuCameras = null;

    private int currentCameraIndex = -1;

    private float lastSwitchTime = 0f;

    private void Start()
    {
        StartCameras();
    }
    private void Update()
    {
        if(!playerManager.InMainMenu)
        {
            if(wasActive)
            {
                wasActive = false;
            }
            return;
        }

        if(!wasActive)
        {
            StartCameras();
            wasActive = true;
        }
        UpdateCameras();
    }

    private void StartCameras()
    {
        mainMenuCameras = FindFirstObjectByType<MainMenuCameras>().CameraPositions;
        currentCameraIndex = Random.Range(0, mainMenuCameras.Length);
        MoveCamera(mainMenuCameras[currentCameraIndex].transform);
        lastSwitchTime = Time.time;
    }

    private void UpdateCameras()
    {
        if(Time.time - lastSwitchTime >= cameraSwitchTime)
        {
            currentCameraIndex = GetNextCameraInArray(currentCameraIndex);
            MoveCamera(mainMenuCameras[currentCameraIndex]);
            lastSwitchTime = Time.time;
        }
    }

    private void MoveCamera(Transform _target)
    {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }

    private int GetNextCameraInArray(int _index)
    {
        if(_index++ >= mainMenuCameras.Length)
        {
            return 0;
        } else
        {
            return _index++;
        }
    }
}
