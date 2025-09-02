using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public CanvasGroup CanvasGroup => GetComponentInChildren<CanvasGroup>();

    // Buttons that open this window (set in editor)
    public Button[] OpenWindowButtons;

    // Event (implemented in MenuManager) for window change
    // Window = this window (window to be opened/closed depending on bool value)
    // bool = open/close
    public static event Action<Window, bool> OnWindowChange;

    // Is window open or not
    public bool isOpen { get; set; } = false;

    // Key that opens the window (set in editor)
    public KeyCode KeyCode;

    private void Awake()
    {
        // Add listeners to the OpenWindowButtons, so that they open the window
        OpenWindowButtons.ToList().ForEach(button => button.onClick.AddListener(OpenWindow));
    }

    private void Update()
    {
        // If KeyCode key is pressed open window
        if (Input.GetKeyDown(KeyCode))
        {
            OpenWindow();
        }

        // No escape function because WebGL/browsers are already using it for the full screen escape 
        /*else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWindow();
        }*/
    }

    // Invoke OnWindowChange event, implemented in MenuManager
    public void OpenWindow()
    {
        OnWindowChange?.Invoke(this, true);
    }

    /*public void CloseWindow()
    {
        OnWindowChange?.Invoke(this, false);
    }*/

    // Reset scrollbar, so that it's at the beginning of the content
    // Can be used in any window script
    // Needs reference to a Scrollbar UI object
    public IEnumerator ResetScrollbar(Scrollbar _scrollbar)
    {
        if (_scrollbar != null)
        {
            yield return new WaitForSeconds(0.2f);
            _scrollbar.value = 1;
        }
    }
}