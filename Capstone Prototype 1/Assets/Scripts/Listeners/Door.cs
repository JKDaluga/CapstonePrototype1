using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum doorTypes
{
    and,
    or
}

public class Door : MonoBehaviour
{
    [Header("Event Settings")]
    public GameObject[] toggler;
    public doorTypes type = doorTypes.and;
    public bool isInverted = false;
    
    private BoxCollider col;
    private MeshRenderer rend;
    private int togglerCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.toggleEvent>(changeState);
        col = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
    }

    void changeState(EventCallbacks.toggleEvent e)
    {
        if(Array.IndexOf(toggler, e.self) > -1)
        {
            if(e.isToggled == !isInverted)
            {
                togglerCounter++;
                if (type == doorTypes.or)
                {
                    col.enabled = false;
                    rend.enabled = false;
                }
                else if (type == doorTypes.and && togglerCounter == toggler.Length)
                {
                    col.enabled = false;
                    rend.enabled = false;
                }
            }
            else
            {
                togglerCounter--;
                if(type == doorTypes.or && togglerCounter == 0)
                {
                    col.enabled = true;
                    rend.enabled = true;
                }
                else if (type == doorTypes.and && togglerCounter < toggler.Length)
                {
                    col.enabled = true;
                    rend.enabled = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(EventCallbacks.EventSystem.Current != null)
        {
            EventCallbacks.EventSystem.Current.UnregisterListener<EventCallbacks.toggleEvent>(changeState);
        }
        else
        {
            print("EventSystem destroyed (This better mean the the world is shutting down)");
        }
    }
}
