using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CursorManager cursorManager;
    [SerializeField]
    float interactDistance = 3;
    GameObject currentTarget = null;

    System.Action<GameObject> onInteract;
    bool canInteract = false;
    [HideInInspector]
    public bool holdingObj = false;

    private void OnEnable() 
    {
        GameManager.DisableOnPause += Activate;
    }

    private void OnDisable() 
    {
        GameManager.DisableOnPause -= Activate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }

        if (!GameManager.Instance.gameIsPaused)
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

            if (holdingObj)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EventCallbacks.dropEvent dropObj = new EventCallbacks.dropEvent();
                    EventCallbacks.EventSystem.Current.TriggerEvent(dropObj);
                    holdingObj = false;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    EventCallbacks.pushEvent pushObj = new EventCallbacks.pushEvent();
                    EventCallbacks.EventSystem.Current.TriggerEvent(pushObj);
                    holdingObj = false;
                }
            }
        }
    }

    private void Activate(bool value)
    {
        GetComponent<PlayerMovement>().enabled = !value;
        GetComponentInChildren<CameraController>().enabled = !value;
    }
}
