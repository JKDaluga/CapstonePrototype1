using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class EventInfo
    {
        public string eventDescription;
    }

    public class toggleEvent : EventInfo
    {
        public bool isToggled;
        public GameObject self;

        public toggleEvent(bool toggle, GameObject obj)
        {
            isToggled = toggle;
            self = obj;
        }
    }

    public class dropEvent: EventInfo
    {
        public void dropObj()
        {
            //Drop Object
        }
    }

    public class pushEvent: EventInfo
    {
        public void pushObj()
        {
            //Push Object
        }
    }
}

