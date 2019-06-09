using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public enum PhysicState
    {
        ice,
        water,
        steam,
    }
    PhysicState state;

    private void Start()
    {
        state = PhysicState.water;
        WaterState.Instance.SetState(state, GetComponent<MeshRenderer>());
    }

    public void ChangeWaterState(bool heat)
    {
        Debug.Log("Chnaging Water State with " + (heat ? "Heat" : "Cold"));
        bool changedState = false;
        if (heat)
        {
            if (state != PhysicState.steam)
            {
                state++;
                changedState = true;
            }
        }
        else if (state != PhysicState.ice)
        {
            state--;
            changedState = true;
        }

        if (!changedState) return;
        Debug.Log("Water State Changed to " + state.ToString());

        WaterState.Instance.SetState(state, GetComponent<MeshRenderer>());
    }
}
