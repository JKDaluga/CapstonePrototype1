using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> 
{
    protected StateMachine<T> m_stateMachine;

    public State(StateMachine<T> machine)
    {
        m_stateMachine = machine;
    }
    public virtual void OnEnter(){}

    public virtual void OnExit(){}

    public virtual void OnUpdate(){}

    public virtual void OnTriggerEntered(Collider2D collider) {}

    public virtual void OnTriggerStayed(Collider2D collider) {}

    public virtual void OnTriggerExited(Collider2D collider) {}

    public virtual void OnCollisionEntered(Collision2D collision) {}

    public virtual void OnCollisionStayed(Collision2D collision) {}

    public virtual void OnCollisionExited(Collision2D collision) {}

    public virtual void OnInteract(GameObject player) {}

    public bool CompareState(string other)
    {
        string state = this.GetType().ToString();
        state = state.Substring(state.LastIndexOf('.') + 1);
        return state.Equals(other);
    }
}
