using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneButton : MonoBehaviour
{
    private Button button => GetComponent<Button>();

    [SerializeField] private int sceneToLoadBuildIndex;

    private void Update()
    {
        if (SceneToLoadIsActive())
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
    }

    public void LoadScene()
    {
        SceneLoader.Instance.SwitchScene(sceneToLoadBuildIndex);
    }

    private bool SceneToLoadIsActive()
    {
        bool _returnValue = false;

        Scene[] _loadedScenes = LoadedScenes();

        for(int i = 0; i < _loadedScenes.Length; i++)
        {
            if (_loadedScenes[i].buildIndex == sceneToLoadBuildIndex)
            {
                _returnValue = true;
            }
        }

        return _returnValue;
    }

    private Scene[] LoadedScenes()
    {
        int _countLoaded = SceneManager.sceneCount;
        Scene[] _returnValue = new Scene[_countLoaded];

        for (int i = 0; i < _countLoaded; i++)
        {
            _returnValue[i] = SceneManager.GetSceneAt(i);
        }

        return _returnValue;
    }
}
