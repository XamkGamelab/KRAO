using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMono<SceneLoader>
{
    private int sceneToLoad;
    private int sceneToUnload;
    public static event Action<int> SetProgressBarValues;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddScene(int sceneBuildIndex)
    {
        SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
    }
    public void SwitchScene(int sceneBuildIndex)
    {
        sceneToUnload = SceneManager.GetActiveScene().buildIndex;
        sceneToLoad = sceneBuildIndex;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;

        SetProgressBarValues?.Invoke(sceneBuildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        //Teleport player
        SceneManager.UnloadSceneAsync(sceneToUnload);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}