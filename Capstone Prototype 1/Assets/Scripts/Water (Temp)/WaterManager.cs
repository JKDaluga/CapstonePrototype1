using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : Interactable
{

    private StateMachine<WaterManager> m_stateMachine;
    [HideInInspector]
    public MeshRenderer m_meshRenderer;

    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();

        //Setup State Machine
        m_stateMachine = new StateMachine<WaterManager>(this);
        m_stateMachine.SetState(new WaterState(m_stateMachine));
    }

    private void Update()
    {
        m_stateMachine.OnUpdate();
    }

    public override void Interact(GameObject player)
    {
        m_stateMachine.OnInteract(player);
    }
}
