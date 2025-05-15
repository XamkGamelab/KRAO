using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Window GameObjects
    public GameObject MainMenuWindow;
    public GameObject SettingsWindow;
    public GameObject CreditsWindow;
    public GameObject SceneSelectionWindow;

    // Buttons
    public Button OpenSettingsButton;
    public Button CloseSettingsButton;
    
    public Button OpenCreditsButton;
    public Button CloseCreditsButton;

    public Button OpenSceneSelectionButton;
    public Button CloseSceneSelectionButton;
    public Button OpenSampleSceneButton;
    public Button OpenJessicaTestSceneButton;

    private void Awake()
    {
        // Add listeners to buttons
        OpenSettingsButton.onClick.AddListener(HandleOpenSettingsButtonClicked);
        CloseSettingsButton.onClick.AddListener(HandleCloseSettingsButtonClicked);

        OpenCreditsButton.onClick.AddListener(HandleOpenCreditsButtonClicked);
        CloseCreditsButton.onClick.AddListener(HandleCloseCreditsButtonClicked);

        OpenSceneSelectionButton.onClick.AddListener(HandleOpenSceneSelectionButtonClicked);
        CloseSceneSelectionButton.onClick.AddListener(HandleCloseSceneSelectionButtonClicked);
        OpenSampleSceneButton.onClick.AddListener(HandleOpenSampleSceneButtonClicked);
        OpenJessicaTestSceneButton.onClick.AddListener(HandleOpenJessicaTestSceneButtonClicked);
    }




    public void OpenWindow(GameObject window)
    {
        window.GetComponent<CanvasGroup>().interactable = true;
        window.GetComponent<CanvasGroup>().alpha = 1;
        window.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CloseWindow(GameObject window)
    {
        window.GetComponent<CanvasGroup>().interactable = false;
        window.GetComponent<CanvasGroup>().alpha = 0;
        window.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void HandleOpenSettingsButtonClicked()
    {
        OpenWindow(SettingsWindow);
        CloseWindow(MainMenuWindow);
    }

    private void HandleCloseSettingsButtonClicked()
    {
        OpenWindow(MainMenuWindow);
        CloseWindow(SettingsWindow);
    }

    private void HandleOpenSceneSelectionButtonClicked()
    {
        OpenWindow(SceneSelectionWindow);
        CloseWindow(MainMenuWindow);
    }

    private void HandleCloseSceneSelectionButtonClicked()
    {
        OpenWindow(MainMenuWindow);
        CloseWindow(SceneSelectionWindow);
    }

    private void HandleOpenSampleSceneButtonClicked()
    {
        CloseWindow(MainMenuWindow);
        SceneManager.LoadScene("SampleScene");
    }

    private void HandleOpenJessicaTestSceneButtonClicked()
    {
        CloseWindow(MainMenuWindow);
        SceneManager.LoadScene("JessicaTestScene");
    }

    private void HandleOpenCreditsButtonClicked()
    {
        OpenWindow(CreditsWindow);
        CloseWindow(MainMenuWindow);
    }

    private void HandleCloseCreditsButtonClicked()
    {
        OpenWindow(MainMenuWindow);
        CloseWindow(CreditsWindow);
    }
}
