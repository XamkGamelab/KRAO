using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    public Transform Spawnpoint;
    private void Awake()
    {
        SetPlayerToSpawn();
    }

    private void SetPlayerToSpawn()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerManager>().TransportPlayer(Spawnpoint.position, Spawnpoint.rotation);
        } else
        {
            SceneLoader.Instance.AddScene(1);
            SceneManager.sceneLoaded += OnPlayerSceneLoaded;
        }
    }
    
    private void OnPlayerSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerManager>().TransportPlayer(Spawnpoint.position, Spawnpoint.rotation);
        }
        SceneManager.sceneLoaded -= OnPlayerSceneLoaded;
    }
}
