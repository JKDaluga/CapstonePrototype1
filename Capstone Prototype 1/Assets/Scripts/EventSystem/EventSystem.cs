using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class EventSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if (__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }
        
        public delegate void EventListener(EventInfo e);
        Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener(System.Type eventType, EventListener listener)
        {
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            eventListeners[eventType].Add(listener);
        }

        public bool unregisterListener(System.Type eventType, EventListener listener)
        {
            if (eventListeners == null || eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                // there isn't a listener for that event
                return false;
            }

            return eventListeners[eventType].Remove(listener);
        }

        public void TriggerEvent(EventInfo e)
        {
            System.Type eventType = e.GetType();
            if (eventListeners == null || eventListeners[eventType] == null)
            {
                //No one is listening for this event
                return;
            }

            foreach (EventListener listener in eventListeners[eventType])
            {
                listener(e);
            }
        }
    }
}
