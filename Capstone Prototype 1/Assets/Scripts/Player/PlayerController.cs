using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CursorManager cursorManager;
    [SerializeField]
    float interactDistance = 3;
    bool paused = false;
    GameObject currentTarget = null;

    System.Action<GameObject> onInteract;
    bool canInteract = false;

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

        if (!paused)
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

    private void Activate(bool value)
    {
        paused = value;
        GetComponent<PlayerMovement>().enabled = !value;
        GetComponentInChildren<CameraController>().enabled = !value;
    }
}
