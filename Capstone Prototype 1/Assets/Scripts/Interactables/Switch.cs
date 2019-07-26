using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    private bool SwitchState = false;

    public EventCallbacks.toggleEvent t;

    public virtual void Interact(GameObject player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("RECIEVED");
            if (!SwitchState)
            {
                SwitchState = true;
                t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                //EventCallbacks.EventSystem.Current.TriggerEvent(t);
            }
            else
            {
                SwitchState = false;
                t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                //EventCallbacks.EventSystem.Current.TriggerEvent(t);
            }
        }
    }
}
