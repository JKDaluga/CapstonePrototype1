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

    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit, interactDistance))
        {
            GameObject target = hit.collider.gameObject;
            if (currentTarget == null || !hit.collider.gameObject.Equals(currentTarget))
            {
                cursorManager.SetCurser(target);
                currentTarget = target;
            }

            IInteractable interactable = target.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}
