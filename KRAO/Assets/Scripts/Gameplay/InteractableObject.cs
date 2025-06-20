using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public InteractionType interactionType;

    public string Prompt = "käyttääksesi esinettä";

    [SerializeField] private UnityEvent interactionEvents;

    public void Interact()
    {
        interactionEvents.Invoke();
    }
}
