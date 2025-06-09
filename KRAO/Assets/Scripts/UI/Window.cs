using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Window : MonoBehaviour
{
    public CanvasGroup CanvasGroup => GetComponentInChildren<CanvasGroup>();

    public Button[] OpenWindowButtons;

    public static event Action<Window> OnWindowOpened;

    public bool isOpen = false;

    public KeyCode KeyCode;

    private void Awake()
    {
        OpenWindowButtons.ToList().ForEach(button => button.onClick.AddListener(OpenWindow));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode))
        {
            OpenWindow();
        }
    }

    private void OpenWindow()
    {
        //isOpen = !isOpen;
        OnWindowOpened?.Invoke(this);
    }
}