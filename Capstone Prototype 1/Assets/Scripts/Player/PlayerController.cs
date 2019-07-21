using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CursorManager cursorManager;
    [SerializeField]
    float interactDistance = 10;
    GameObject currentTarget = null;

    System.Action<GameObject> onInteract;
    bool canInteract = false;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit, interactDistance))
        {
            GameObject target = hit.collider.gameObject;
            if (currentTarget == null || !hit.collider.gameObject.Equals(currentTarget))
            {
                if (canInteract)
                {
                    canInteract = false;
                    if (currentTarget != null) onInteract -= currentTarget.GetComponent<IInteractable>().Interact;
                }

                IInteractable interactable = target.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    onInteract += interactable.Interact;
                    canInteract = true;
                }
                cursorManager.SetCurser(target);
                currentTarget = target;
            }
        }
        else if (currentTarget != null)
        {
            if (canInteract)
            {
                canInteract = false;
                onInteract -= currentTarget.GetComponent<IInteractable>().Interact;
            }
            cursorManager.SetCurser(null);
            currentTarget = null;
        }

        if (canInteract)
        {
            if (onInteract != null) onInteract.Invoke(gameObject);
        }
    }
}
