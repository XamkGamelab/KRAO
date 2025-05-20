using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private int sceneToLoadBuildIndex;

    public void LoadScene()
    {
        SceneLoader.Instance.SwitchScene(sceneToLoadBuildIndex);
    }
}
