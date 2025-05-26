using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public InteractionType interactionType;

    public string Prompt = "k‰ytt‰‰ksesi esinett‰";

    [SerializeField] private UnityEvent interactionEvents;

    private bool highlightOn = false;
    [SerializeField] private Material highlightMaterial;

    //private void Start()
    //{
    //    highlightMaterial = GetComponent<MeshRenderer>().materials[0];
    //}
    public void Interact()
    {
        interactionEvents.Invoke();
    }

    //public void ToggleInteractableHighlight()
    //{
    //    highlightOn = !highlightOn;
    //    highlightMaterial.SetInt("_HighlightOn", highlightOn ? 1 : 0);
    //}
}
