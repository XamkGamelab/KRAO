using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public InteractionType interactionType;

    public string Prompt = "käyttääksesi esinettä";

    [SerializeField] private UnityEvent interactionEvents;

    private bool highlightOn = false;
    private Material highlightMaterial;

    private void Start()
    {
        highlightMaterial = GetComponent<MeshRenderer>().materials[1];
    }
    public void Interact()
    {
        interactionEvents.Invoke();
    }

    public void ToggleInteractableHighlight()
    {
        highlightOn = !highlightOn;
        highlightMaterial.SetInt("_HighlightOn", highlightOn ? 1 : 0);
    }
}
