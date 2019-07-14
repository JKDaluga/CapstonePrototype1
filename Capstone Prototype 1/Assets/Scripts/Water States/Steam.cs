using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Steam : WaterState, IPooledObject
{
    Rigidbody body;
    [SerializeField]
    //[Range(1, 10)]
    float acceleration = 100;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
    }

    public override void Interact(GameObject player)
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.ICE_KEY, transform);
            gameObject.SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        if (!body) body = GetComponent<Rigidbody>(); 
        body.AddForce(Vector3.up*acceleration, ForceMode.Acceleration);
    }
}
