using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool switchable;
    public GameObject toggler;

    public Transform exitSpot;

    public Pipe exitPipe1;
    public Pipe exitPipe2;
    private bool mode = true;

    // Start is called before the first frame update
    void Start()
    {
        if(switchable)
        {
            EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.toggleEvent>(changePipe);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePipe(EventCallbacks.toggleEvent e)
    {
        if (toggler == e.self)
        {
            mode = e.isToggled;
            if (mode)
            {
                this.GetComponent<Renderer>().material.color = exitPipe1.gameObject.GetComponent<Renderer>().material.color;
            }
            else
            {
                this.GetComponent<Renderer>().material.color = exitPipe2.gameObject.GetComponent<Renderer>().material.color;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Steam")
        {
            if (mode)
            {
                if(exitPipe1 != null && exitPipe1.exitSpot != null)
                other.gameObject.transform.position = exitPipe1.exitSpot.position;
            }
            else
            {
                if (exitPipe2 != null && exitPipe2.exitSpot != null)
                    other.gameObject.transform.position = exitPipe2.exitSpot.position;
            }
        }
    }

    private void OnDestroy()
    {
        if(switchable)
        {
            if (EventCallbacks.EventSystem.Current != null)
            {
                EventCallbacks.EventSystem.Current.UnregisterListener<EventCallbacks.toggleEvent>(changePipe);
            }
            else
            {
                print("EventSystem destroyed (This better mean the the world is shutting down)");
            }
        }
    }
}
