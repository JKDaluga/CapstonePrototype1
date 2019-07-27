using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    private bool SwitchState = false;

    public EventCallbacks.toggleEvent t;
    public GameObject handle;

    public virtual void Interact(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!SwitchState)
            {
                SwitchState = true;
                t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                //Hacky junk to give the switch visual context
                transform.Rotate(new Vector3(0, 0, 180));
                handle.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                SwitchState = false;
                t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                //Hacky junk to give the switch visual context
                transform.Rotate(new Vector3(0, 0, 180));
                handle.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        
    }
}
