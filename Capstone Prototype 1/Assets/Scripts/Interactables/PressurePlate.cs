using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool SwitchState = false;
    private int pressed = 0;
    private Collider other;
    private bool player = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") player = true;
        else this.other = other;
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

        if (other.tag == "Player") player = false;
        if (other.tag == "Ice" || other.tag == "Player")
        {
            pressed--;
            if (pressed <= 0)
            {
                SwitchState = false;
                EventCallbacks.toggleEvent t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                GetComponent<Renderer>().material.color = Color.red;
                if (pressed < 0) pressed = 0;
                if (other.tag == "Ice")  this.other = null;
            }
        }
    }

    private void Update()
    {
        if (other != null)
        {
            if (!(other.tag == "Ice" || other.tag == "Player") && pressed > 0 && !player)
            {
                Debug.Log("Gone but we still here What is going on my man uh oh yikes it still exists it just aint ice");
                SwitchState = false;
                EventCallbacks.toggleEvent t = new EventCallbacks.toggleEvent(SwitchState, gameObject);
                EventCallbacks.EventSystem.Current.TriggerEvent(t);
                GetComponent<Renderer>().material.color = Color.red;
                pressed--;
                other = null;
            }
        }
    }
}
