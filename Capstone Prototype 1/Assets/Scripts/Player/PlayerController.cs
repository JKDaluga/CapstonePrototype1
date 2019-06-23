﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CursorManager cursorManager;
    GameObject currentTarget = null;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;
            if (currentTarget == null || !hit.collider.gameObject.Equals(currentTarget))
            {
                cursorManager.SetCurser(target);
                currentTarget = target;
            }

            Interactable interactable = target.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}