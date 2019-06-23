using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterState : State<WaterManager>
{
    public WaterState(StateMachine<WaterManager> machine): base (machine) {}

    public override void OnEnter()
    {
        m_stateMachine.controller.m_meshRenderer.material = Resources.Load<Material>("Materials/Water");
    }

    public override void OnInteract(GameObject player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_stateMachine.SetState(new SteamState(m_stateMachine));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            m_stateMachine.SetState(new IceState(m_stateMachine));
        }
    }
}
