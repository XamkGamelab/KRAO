using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public CanvasGroup CanvasGroup => GetComponentInChildren<CanvasGroup>();

    public Button[] OpenWindowButtons;

    public static event Action<Window> OnWindowOpened;

    public bool isOpen { get; set; } = false;

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

    public void OpenWindow()
    {
        OnWindowOpened?.Invoke(this);
    }

    public IEnumerator ResetScrollbar(Scrollbar _scrollbar)
    {
        if (_scrollbar != null)
        {
            yield return new WaitForSeconds(0.02f);
            _scrollbar.value = 1;
        }
    }
}