using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent interactionEvents;
    public void Interact()
    {
        interactionEvents.Invoke();
    }
}
