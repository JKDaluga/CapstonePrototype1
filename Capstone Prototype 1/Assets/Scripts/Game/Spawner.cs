using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPoolTag objectTag;

    private void Start()
    {
        GameManager.Instance.objectPooler.SpawnFromPool(objectTag, transform);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
    }
}