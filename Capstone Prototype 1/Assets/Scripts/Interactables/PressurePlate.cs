using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool SwitchState = false;
    private int pressed = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ice" || other.tag == "Player")
        {
            if(pressed == 0)
            {
                SwitchState = true;
                EventCallbacks.toggleEvent t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                GetComponent<Renderer>().material.color = Color.green;
            }
            pressed++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ice" || other.tag == "Player")
        {
            pressed--;
            if (pressed == 0)
            {
                SwitchState = false;
                EventCallbacks.toggleEvent t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
