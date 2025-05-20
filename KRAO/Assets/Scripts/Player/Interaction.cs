using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 2.5f;
    [SerializeField] private LayerMask interactionMask;

    private List<InteractableObject> interactableObjects = new List<InteractableObject>();
    private InteractableObject selectedObject = null;

    private InputAction interact;

    private void Start()
    {
        interact = InputSystem.actions.FindAction("Interact");
    }
    void Update()
    {
        CheckForInteractablesInRange();

        if(interactableObjects.Count > 0)
        {
            InteractionRaycast();
        }

        if (CanInteract())
        {
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
                        selectedObject.ToggleInteractableHighlight();
                    }
                    return;
                }
            }
        }

        if(selectedObject != null)
        {
            selectedObject.ToggleInteractableHighlight();
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
                    Debug.Log($"Added {obj.name} to Interactable Objects within reach.");
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
                    Debug.Log($"Removed {obj.name} from Interactable Objects within reach.");
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
}
