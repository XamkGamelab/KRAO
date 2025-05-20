using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    public string Prompt;
    [SerializeField] private int sceneToLoadBuildIndex;

    private InteractionPrompt interactionPrompt;

    private void Start()
    {
        interactionPrompt = GameObject.Find("InteractionPrompt").GetComponent<InteractionPrompt>();
    }

    public void LoadScene()
    {
        interactionPrompt.HidePrompt();
        SceneLoader.Instance.SwitchScene(sceneToLoadBuildIndex);
    }

    public void ShowSceneName()
    {
        interactionPrompt.ShowPrompt(Prompt, InteractionType.Stay);
    }

    public void HideSceneName()
    {
        interactionPrompt.HidePrompt();
    }
}
