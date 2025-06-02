using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public InteractionType interactionType;

    public string Prompt = "k‰ytt‰‰ksesi esinett‰";

    [SerializeField] private UnityEvent interactionEvents;

    public void Interact()
    {
        interactionEvents.Invoke();
    }
}
