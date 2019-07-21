using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
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
}

