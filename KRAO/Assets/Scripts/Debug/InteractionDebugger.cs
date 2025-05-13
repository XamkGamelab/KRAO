using UnityEngine;

public class InteractionDebugger : MonoBehaviour
{
    [SerializeField] private string debugMessage;

    public void ShowMessage()
    {
        Debug.Log(debugMessage);
    }
}
