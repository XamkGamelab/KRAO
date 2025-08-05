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
    private Window lessonWindow => FindFirstObjectByType<LessonWindow>();
    private Window journalWindow => FindFirstObjectByType<JournalWindow>();

    private Window previousWindow;

    public Window PauseMenuWindow;
    public Window MainMenuWindow;
    public Button CloseSceneSelectionButton;
    public Button CloseSettingsButton;


    private void Start()
    {
        Window.OnWindowOpened += HandleWindowOpened;
        SceneManager.activeSceneChanged += OnSceneChanged;
        CloseSceneSelectionButton.onClick.AddListener(OpenMenu);
        CloseSettingsButton.onClick.AddListener(OpenMenu);

        OpenMenu();
    }

    private void OnSceneChanged(Scene _current, Scene _next)
    {
        if (_next.buildIndex == 0)
        {
            OpenMenu();
        }
        else
        {
            HandleWindowOpened(hudWindow);
            PauseMenuWindow.KeyCode = KeyCode.P;
            journalWindow.KeyCode = KeyCode.J;
        }
    }

    private void OpenMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            HandleWindowOpened(MainMenuWindow);
            PauseMenuWindow.KeyCode = KeyCode.None;
            journalWindow.KeyCode = KeyCode.None;
        }
        else
        {
            HandleWindowOpened(PauseMenuWindow);
        }
    }

    private void HandleWindowOpened(Window _window)
    {
        //forbid opening other windows when in lesson
        if (previousWindow == lessonWindow && _window != hudWindow)
        {
            return;
        }

        windows.ForEach(window => window.isOpen = false);
        _window.isOpen = true;
        windows.ForEach(window => ToggleWindow(window));

        previousWindow = _window;
    }

    public void ToggleWindow(Window _window, bool _open)
    {
        _window.CanvasGroup.interactable = _open;
        _window.CanvasGroup.alpha = _open ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _open;
        playerManager.ToggleControllerState(hudWindow.isOpen);
    }

    public void ToggleWindow(Window _window)
    {
        _window.CanvasGroup.interactable = _window.isOpen;
        _window.CanvasGroup.alpha = _window.isOpen ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _window.isOpen;
        playerManager.ToggleControllerState(hudWindow.isOpen);
    }
}