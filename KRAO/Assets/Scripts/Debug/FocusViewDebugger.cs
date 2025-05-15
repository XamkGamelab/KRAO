using UnityEngine;
using UnityEngine.InputSystem;

public class FocusViewDebugger : MonoBehaviour
{
    private FocusView focusView => GameObject.FindWithTag("FocusView").GetComponent<FocusView>();

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("wow");
            focusView.ToggleFocusView();
        }
    }
}