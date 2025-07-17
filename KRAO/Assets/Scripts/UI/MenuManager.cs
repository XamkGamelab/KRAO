using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<Window> windows => FindObjectsByType<Window>(FindObjectsSortMode.None).ToList();
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    private Window hudWindow => FindFirstObjectByType<HUDWindow>();
    public Window PauseMenuWindow;
    public Window MainMenuWindow;
    public Button CloseSceneSelectionButton;
    public Button CloseSettingsButton;

    private void Start()
    {
        Window.OnWindowOpened += HandleWindowOpened;
        SceneManager.sceneLoaded += OnSceneChanged;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            OpenMainMenu();
            CloseSceneSelectionButton.onClick.AddListener(OpenMainMenu);
            CloseSettingsButton.onClick.AddListener(OpenMainMenu);
            PauseMenuWindow.KeyCode = KeyCode.None;
        }
    }

    private void OnSceneChanged(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.buildIndex == 0)
        {
            HandleWindowOpened(MainMenuWindow);
            CloseSceneSelectionButton.onClick.AddListener(OpenMainMenu);
            CloseSettingsButton.onClick.AddListener(OpenMainMenu);
            PauseMenuWindow.KeyCode = KeyCode.None;
        }
        else
        {
            HandleWindowOpened(hudWindow);
            CloseSceneSelectionButton.onClick.AddListener(OpenPauseMenu);
            CloseSettingsButton.onClick.AddListener(OpenPauseMenu);
            PauseMenuWindow.KeyCode = KeyCode.Escape;
        }
    }

    private void OpenPauseMenu()
    {
        HandleWindowOpened(PauseMenuWindow);
    }

    private void OpenMainMenu()
    {
        HandleWindowOpened(MainMenuWindow);
    }

    private void HandleWindowOpened(Window _window)
    {
        windows.ForEach(window => window.isOpen = false);
        _window.isOpen = true;
        windows.ForEach(window => ToggleWindow(window));
        playerManager.ToggleControllerState(hudWindow.isOpen);
    }

    public void ToggleWindow(Window _window, bool _open)
    {
        _window.CanvasGroup.interactable = _open;
        _window.CanvasGroup.alpha = _open ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _open;
    }

    public void ToggleWindow(Window _window)
    {
        _window.CanvasGroup.interactable = _window.isOpen;
        _window.CanvasGroup.alpha = _window.isOpen ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _window.isOpen;
    }
}