using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : WaterState,IPooledObject
{
    private Rigidbody body;

    public override void Interact(GameObject player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.ICE_KEY, transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.STEAM_KEY, transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        if (!body) body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
    }
}
