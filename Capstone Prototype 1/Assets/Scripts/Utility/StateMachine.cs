using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
    {
        T m_controller;
        public T controller
        {
            get
            {
                return m_controller;
            }
        }

        State<T> m_currentState = null;
        public State<T> currentState
        {
            get
            {
                return m_currentState;
            }
        }

        public StateMachine(T stateController)
        {
            m_controller = stateController;
        }

		public void SetState(State<T> newState)
		{
            if (m_currentState != null)
            {
                m_currentState.OnExit();
            }
            m_currentState = newState;
            m_currentState.OnEnter();
		}

        public void OnUpdate()
        {
            m_currentState.OnUpdate();
        }

        public void OnTriggerEntered(Collider2D collider) 
        {
            m_currentState.OnTriggerEntered(collider);
        }

		public void OnTriggerStayed(Collider2D collider) 
        {
            m_currentState.OnTriggerStayed(collider);
        }

		public void OnTriggerExited(Collider2D collider) 
        {
            m_currentState.OnTriggerExited(collider);
        }

		public void OnCollisionEntered(Collision2D collision) 
        {
            m_currentState.OnCollisionEntered(collision);
        }

		public void OnCollisionStayed(Collision2D collision) 
        {
            m_currentState.OnCollisionStayed(collision);
        }

		public void OnCollisionExited(Collision2D collision) 
        {
            m_currentState.OnCollisionExited(collision);
        }

        public void OnInteract(GameObject player)
        {
            m_currentState.OnInteract(player);
        }
	}
