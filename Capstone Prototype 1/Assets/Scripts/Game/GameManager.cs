using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class GameManager : MonoBehaviour
{

    private static GameManager m_instance;
    public static GameManager Instance
    {
        get 
        {
            return m_instance;
        }
    }
    [HideInInspector]
    public ObjectPooler objectPooler;

    private void Awake()
    {
        if (m_instance) Destroy(this);
        else
        {
            m_instance = this;
            objectPooler = new ObjectPooler();   
        }
    }
}
