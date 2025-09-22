using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MobilePopup : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#endif
        return false;
    }

    void Start()
    {
        
    }
}
