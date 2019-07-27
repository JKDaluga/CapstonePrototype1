using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject toggler;

    private BoxCollider col;
    private MeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.toggleEvent>(changeState);
        col = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
    }

    void changeState(EventCallbacks.toggleEvent e)
    {
        if(toggler = e.self)
        {
            if(e.isToggled)
            {
                print("OPEN");
                col.enabled = false;
                rend.enabled = false;
            }
            else
            {
                print("CLOSE");
                col.enabled = true;
                rend.enabled = true;
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
