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
    //private PlayerManager playerManager => GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

    private void Start()
    {
        Window.OnWindowOpened += HandleWindowOpened;
    }

    private void HandleWindowOpened(Window _window)
    {
        windows.ForEach(window => window.isOpen = false);
        _window.isOpen = true;
        windows.ForEach(window => ToggleWindow(window));
        //playerManager.ToggleControllerState(false);
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