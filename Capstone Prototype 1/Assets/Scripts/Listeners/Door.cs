using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject toggler;

    // Start is called before the first frame update
    void Start()
    {
        EventCallbacks.EventSystem.Current.RegisterListener<EventCallbacks.toggleEvent>(changeState);
    }

    void changeState(EventCallbacks.toggleEvent e)
    {
        if(toggler = e.self)
        {
            if(e.isToggled)
            {
                print("OPEN");
            }
            else
            {
                print("CLOSE");
            }
        }
    }

    private void OnDestroy()
    {
        EventCallbacks.EventSystem.Current.UnregisterListener<EventCallbacks.toggleEvent>(changeState);
    }
}
