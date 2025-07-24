using Unity.Cinemachine;
using UnityEngine;

public class CutsceneCameraManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera[] cameras;

    private void Start()
    {
        cameras[0].Prioritize();
    }

    public void ChangeCamera(int cameraIndex)
    {
        cameras[cameraIndex].Prioritize();
    }

    public void ChangeCamera(string cameraName)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].name == cameraName)
            {
                cameras[i].Prioritize();
                return;
            }
        }

        Debug.LogError($"No Camera with name {cameraName} was found in array.");
    }
}
