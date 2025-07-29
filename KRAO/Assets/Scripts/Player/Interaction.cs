using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public bool InteractionEnabled = false;

    [SerializeField] private float interactionRadius = 2.5f;
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private InteractionPrompt interactionPrompt;

    private List<InteractableObject> interactableObjects = new List<InteractableObject>();
    private InteractableObject selectedObject = null;

    private InputAction interact;

    private void Start()
    {
        interact = InputSystem.actions.FindAction("Interact");
    }
    void Update()
    {
        if (!InteractionEnabled)
        {
            if(interactionPrompt != null)
            {
                interactionPrompt.HidePrompt();
            }
            return;
        }

        CheckForInteractablesInRange();

        if(interactableObjects.Count > 0)
        {
            InteractionRaycast();
        }

        if (CanInteract())
        {
            interactionPrompt.ShowPrompt(selectedObject.Prompt, selectedObject.interactionType);
            if (interact.WasPressedThisFrame())
            {
                selectedObject.Interact();
            }
        }
    }

    private void InteractionRaycast()
    {
        RaycastHit _hit;
        Ray _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(_ray, out _hit, interactionRadius, interactionMask))
        {
            if(_hit.collider.GetComponent<InteractableObject>() != null)
            {
                InteractableObject _rayTarget = _hit.collider.GetComponent<InteractableObject>();
                if (IsObjectInList(_rayTarget))
                {
                    if(selectedObject != _rayTarget)
                    {
                        selectedObject = _rayTarget;
                    }
                    return;
                }
            }
        }

        if(selectedObject != null)
        {
            interactionPrompt.HidePrompt();
            selectedObject = null;
        }
    }
    private void CheckForInteractablesInRange()
    {
        Collider[] _collidersInRange = Physics.OverlapSphere(transform.position + Vector3.up * 1.5f, interactionRadius, interactionMask);
        InteractableObject[] _objectsInRange = GetInteractableObjects(_collidersInRange);

        //Add InteractableObjects in range to the list
        if( _objectsInRange.Length > 0)
        {
            foreach (InteractableObject obj in _objectsInRange)
            {
                if (!IsObjectInList(obj))
                {
                    interactableObjects.Add(obj);
                }
            }
        }

        //Remove InteractableObjects no longer in range from the list
        if(interactableObjects.Count > 0 )
        {
            foreach (InteractableObject obj in interactableObjects)
            {
                if (!_objectsInRange.Contains(obj))
                {
                    interactableObjects.Remove(obj);
                    break;
                }
            }
        }
    }

    private InteractableObject[] GetInteractableObjects(Collider[] colliders)
    {
        List<InteractableObject> _returnValue = new List<InteractableObject>();
        foreach(Collider collider in colliders)
        {
            if(collider.GetComponent<InteractableObject>() != null)
            {
                _returnValue.Add(collider.GetComponent<InteractableObject>());
            }
        }

        return _returnValue.ToArray();
    }

    private bool IsObjectInList(InteractableObject interactableObject)
    {
        if(interactableObjects.Contains(interactableObject))
        {
            return true;
        }
        else return false;
    }

    private bool CanInteract()
    {
        return selectedObject != null;
    }

    public void ClearInteractables()
    {
        interactableObjects.Clear();
    }
}
