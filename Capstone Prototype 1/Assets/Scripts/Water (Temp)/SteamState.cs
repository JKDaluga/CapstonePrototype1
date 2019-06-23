using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamState : State<WaterManager>
{
    public SteamState(StateMachine<WaterManager> machine): base (machine) {}

    public override void OnEnter()
    {
        m_stateMachine.controller.m_meshRenderer.material = Resources.Load<Material>("Materials/Steam");
    }

    public override void OnInteract(GameObject player)
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_stateMachine.SetState(new WaterState(m_stateMachine));
        }
    }
}
