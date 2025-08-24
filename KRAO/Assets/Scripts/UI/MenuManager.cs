using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private LessonManager lessonManager => FindFirstObjectByType<LessonManager>();
    private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    public List<Window> windows => FindObjectsByType<Window>(FindObjectsSortMode.None).ToList();
    private Window hudWindow => FindFirstObjectByType<HUDWindow>();
    private Window lessonWindow => FindFirstObjectByType<LessonWindow>();
    private Window journalWindow => FindFirstObjectByType<JournalWindow>();

    private Window previousWindow;
    private Window opening;

    public Window PauseMenuWindow;
    public Window MainMenuWindow;
    public Button CloseSceneSelectionButton;
    public Button CloseSettingsButton;


    private void Start()
    {
        Window.OnWindowChange += HandleWindowChange;
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
            HandleWindowOpened(hudWindow, PauseMenuWindow);
            PauseMenuWindow.KeyCode = KeyCode.P;
            journalWindow.KeyCode = KeyCode.J;
        }
    }

    private void OpenMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            HandleWindowOpened(MainMenuWindow, MainMenuWindow);
            PauseMenuWindow.KeyCode = KeyCode.None;
            journalWindow.KeyCode = KeyCode.None;
        }
        else
        {
            HandleWindowOpened(PauseMenuWindow, hudWindow);
        }
    }

    private void HandleWindowOpened(Window _window, Window _previous)
    {
        windows.ForEach(window => window.isOpen = false);
        _window.isOpen = true;
        windows.ForEach(window => ToggleWindow(window));

        StartCoroutine(SetPreviousWindow(_previous));
    }

    private void HandleWindowChange(Window _window, bool _open)
    {
        //opening _window
        if (_open == true)
        {
            if ((SceneManager.GetActiveScene().buildIndex == 0 && _window == PauseMenuWindow) ||
            (SceneManager.GetActiveScene().buildIndex != 0 && _window == MainMenuWindow))
            {
                return;
            }
            else
            {
                if (previousWindow != CurrentWindow())
                {
                    if (CurrentWindow() == PauseMenuWindow || CurrentWindow() == MainMenuWindow)
                    {
                        StartCoroutine(SetPreviousWindow(CurrentWindow()));
                    }
                    else
                    {
                        StartCoroutine(SetPreviousWindow(CurrentWindow()));
                    }
                }
                opening = _window;
            }
        }
        //closing _window
        else if (_open == false)
        {
            if ((SceneManager.GetActiveScene().buildIndex == 0 && previousWindow == PauseMenuWindow) ||
            (SceneManager.GetActiveScene().buildIndex != 0 && previousWindow == MainMenuWindow) ||
            previousWindow == lessonWindow)
            {
                return;
            }
            else
            {
                opening = previousWindow;
                if (_window != lessonWindow)
                {
                    StartCoroutine(SetPreviousWindow(_window));
                }
            }
        }
        if (opening != null)
        {
            if (CurrentWindow() == lessonWindow)
            {
                lessonManager.CloseLesson();
                StartCoroutine(SetPreviousWindow(PauseMenuWindow));
            }
            //close windows
            windows.ForEach(window => window.isOpen = false);

            //open window
            opening.isOpen = true;

            //toggle windows
            windows.ForEach(window => ToggleWindow(window));
        }
    }

    private IEnumerator SetPreviousWindow(Window _previous)
    {
        yield return new WaitForSeconds(0.1f);
        previousWindow = _previous;
    }

    private Window CurrentWindow()
    {
        Window _window = null;

        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].CanvasGroup.alpha == 1)
            {
                _window = windows[i];
                break;
            }
        }

        return _window;
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