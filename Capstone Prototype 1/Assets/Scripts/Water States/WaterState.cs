using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterState : MonoBehaviour, IInteractable
{
    public virtual void Interact(GameObject player)
    {
        Debug.Log("WaterState Interact() is being called!");
    }
}