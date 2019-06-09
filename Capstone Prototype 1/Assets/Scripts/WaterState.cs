using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterState
{
    private static WaterState m_instance;
    public static WaterState Instance
    {
        get 
        {
            if (m_instance == null)
                m_instance = new WaterState();
            return m_instance;
        }
    }
    Material ice;
    Material water;
    Material steam;

    WaterState()
    {
        ice = Resources.Load<Material>("Materials/Ice");
        water = Resources.Load<Material>("Materials/Water");
        steam = Resources.Load<Material>("Materials/Steam");
    }

    public void SetState(Water.PhysicState state, MeshRenderer renderer)
    {
        switch (state)
        {
            case Water.PhysicState.ice:
            {
                renderer.material = ice;
                break;
            }
            case Water.PhysicState.water:
            {
                renderer.material = water;
                break;
            }
            case Water.PhysicState.steam:
            {
                renderer.material = steam;
                break;
            }
        }
    }
}
