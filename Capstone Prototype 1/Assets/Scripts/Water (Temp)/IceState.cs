using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceState : State<WaterManager>
{
    public IceState(StateMachine<WaterManager> machine): base (machine) {}

    public override void OnEnter()
    {
        m_stateMachine.controller.m_meshRenderer.material = Resources.Load<Material>("Materials/Ice");
    }

    public override void OnInteract(GameObject player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_stateMachine.SetState(new WaterState(m_stateMachine));
        }
    }
}
