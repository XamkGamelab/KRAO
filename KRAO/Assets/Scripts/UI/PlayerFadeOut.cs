using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerFadeOut : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    public float FadeTime = 1.0f;

    private float fadeTick;

    private enum FadeState { Idle, FadeOut, FadeIn }
    private FadeState fadeState;

    private bool continuous = false;

    #region Public Methods
    
    //The method used by other scripts to call for the Fade In or Out
    public void ToggleFade(bool _state)
    {
        fadeTick = Time.time;
        if(_state)
        {
            if(canvasGroup.alpha != 1f)
            {
                fadeState = FadeState.FadeIn;
            }
        } else
        {
            if(canvasGroup.alpha != 0f)
            {
                fadeState = FadeState.FadeOut;
            }
        }
    }

    public bool FadeIsIdle()
    {
        return fadeState == FadeState.Idle;
    }
    #endregion
    #region Private Methods
    void Update()
    {
        if(fadeState == FadeState.Idle)
        {
            return;
        }

        Fade();
    }

    private void Fade()
    {
        if(fadeState == FadeState.Idle)
        {
            return;
        }

        float _alpha = GetFadeAlpha((Time.time - fadeTick) / FadeTime);

        canvasGroup.alpha = Mathf.Clamp(_alpha, 0f, 1f);

        if(Time.time - fadeTick >= FadeTime)
        {
            fadeState = FadeState.Idle;
        }
    }
    
    private float GetFadeAlpha(float _time)
    {
        if(fadeState == FadeState.FadeIn)
        {
            return _time;
        } else if(fadeState == FadeState.FadeOut)
        {
            return 1f - _time;
        }

        return 0f;
    }
    #endregion
}
