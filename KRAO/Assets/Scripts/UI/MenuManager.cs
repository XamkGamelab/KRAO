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

    /*private Window previousWindow;
    private Window opening;*/

    // Set in editor
    public Window PauseMenuWindow;
    public Window MainMenuWindow;
    public Button CloseSceneSelectionButton;
    public Button CloseSettingsButton;


    private void Start()
    {
        // Start listening to window changes
        Window.OnWindowChange += HandleWindowChange;
        // Start listening to scene changes
        SceneManager.activeSceneChanged += OnSceneChanged;

        // Add separate listeners to CloseSceneSelection and CloseSettings buttons
        // Because they can be opened both in game and in main menu,
        // they need to open either MainMenuWindow or PauseMenuWindow depending on the scene
        CloseSceneSelectionButton.onClick.AddListener(OpenMenu);
        CloseSettingsButton.onClick.AddListener(OpenMenu);

        // Open correct menu (main/pause menu) depending on which scene is open
        OpenMenu();
    }

    // Handle changing scenes (in terms of windows/UI)
    private void OnSceneChanged(Scene _current, Scene _next)
    {
        // If the next scene is main menu, open main menu window
        if (_next.buildIndex == 0)
        {
            OpenMenu();
        }
        // If next is any other scene, open hud
        else
        {
            HandleWindowChange(hudWindow, true);
            // Set pause menu and journal opening keycodes, so they can be opened with them
            PauseMenuWindow.KeyCode = KeyCode.P;
            journalWindow.KeyCode = KeyCode.J;
        }
    }

    // Open either main menu or pause menu, depending on the scene
    private void OpenMenu()
    {
        // If main menu scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            HandleWindowChange(MainMenuWindow, true);
            // Set pause menu and journal opening keycodes to none, so they can't be opened in main menu
            PauseMenuWindow.KeyCode = KeyCode.None;
            journalWindow.KeyCode = KeyCode.None;
        }
        // if any other scene, open pause menu
        else
        {
            HandleWindowChange(PauseMenuWindow, true);
        }
    }

    // Open/close window (event sent from Window scripts)
    private void HandleWindowChange(Window _window, bool _open)
    {
        // Open window if _open is true
        if (_open == true)
        {
            // If a lesson is open, close lesson before opening the next
            if (CurrentWindow() == lessonWindow)
            {
                lessonManager.CloseLesson();
            }
            // set isOpen as false for every window
            windows.ForEach(window => window.isOpen = false);
            // Set isOpen as true for the window to be opened
            _window.isOpen = true;
            // Toggle windows (close windows with isOpen as false and open the ones with isOpen as true)
            windows.ForEach(window => ToggleWindow(window));
        }
    }

    // Find the window that is currently open
    private Window CurrentWindow()
    {
        Window _window = null;
        // Get window from windows that has isOpen as true
        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].isOpen == true)
            {
                // set it as the return window
                _window = windows[i];
                break;
            }
            /*if (windows[i].CanvasGroup.alpha == 1)
            {
                _window = windows[i];
                break;
            }*/
        }
        // Return window that is open or null if none was found
        return _window;
    }

    // Open or close the window coming in depending on the _open value
    // Toggle player controller state depending on hudWindow's isOpen value
    public void ToggleWindow(Window _window, bool _open)
    {
        _window.CanvasGroup.interactable = _open;
        _window.CanvasGroup.alpha = _open ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _open;
        playerManager.ToggleControllerState(hudWindow.isOpen);
    }

    // Open or close the window coming in depending on its isOpen value
    // Toggle player controller state depending on hudWindow's isOpen value
    public void ToggleWindow(Window _window)
    {
        _window.CanvasGroup.interactable = _window.isOpen;
        _window.CanvasGroup.alpha = _window.isOpen ? 1f : 0f;
        _window.CanvasGroup.blocksRaycasts = _window.isOpen;
        playerManager.ToggleControllerState(hudWindow.isOpen);
    }

    /* Not used because having an escape functionality in a browser doesn't work well
     * 
    private void HandleWindowChange(Window _window, bool _open)
    {
        //opening _window
        if (_open)
        {
            StartCoroutine(HandleWindowOpen(_window));
        }
        //closing _window
        else
        {
            StartCoroutine(HandleWindowClose(_window));
        }
        if (opening != null)
        {
            Debug.LogWarning("OPENING: " + opening);
            Debug.LogWarning("CLOSING: " + previousWindow);

            StartCoroutine(OpenWindow());
        }
    }

    private IEnumerator HandleWindowOpen(Window _window)
    {
        if ((SceneManager.GetActiveScene().buildIndex == 0 && _window == PauseMenuWindow) ||
            (SceneManager.GetActiveScene().buildIndex != 0 && _window == MainMenuWindow))
        {
            yield return null;
        }
        else
        {
            if (previousWindow != CurrentWindow())
            {
                previousWindow = CurrentWindow();
                yield return new WaitForSeconds(0.1f);
            }

            opening = _window;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HandleWindowClose(Window _window)
    {
        if ((SceneManager.GetActiveScene().buildIndex == 0 && previousWindow == PauseMenuWindow) ||
        (SceneManager.GetActiveScene().buildIndex != 0 && previousWindow == MainMenuWindow) ||
        previousWindow == lessonWindow)
        {
            yield return null;
        }
        else
        {
            opening = previousWindow;
            yield return new WaitForSeconds(0.1f);

            if (_window != lessonWindow)
            {
                previousWindow = _window;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator OpenWindow()
    {
        Debug.LogWarning("OPENING: " + opening);
        Debug.LogWarning("CLOSING: " + previousWindow);

        if (CurrentWindow() == lessonWindow && lessonManager.openLesson != null)
        {
            lessonManager.CloseLesson();
            //previousWindow = PauseMenuWindow;
            previousWindow = hudWindow;
            yield return new WaitForSeconds(0.1f);
        }
        //set isOpen as false for all windows
        windows.ForEach(window => window.isOpen = false);
        yield return new WaitForSeconds(0.1f);

        //set isOpen as true for the window to be opened
        opening.isOpen = true;
        yield return new WaitForSeconds(0.1f);

        //toggle windows
        windows.ForEach(window => ToggleWindow(window));
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator SetPreviousWindow(Window _previous)
    {
        yield return new WaitForSeconds(0.1f);
        previousWindow = _previous;
        yield return new WaitForSeconds(0.1f);
    }*/
}