using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPoolTag objectTag;
    bool useTransform = true;
    Vector3 offset;
    Vector3 rotation;
    Vector3 scale = Vector3.one;

    private void Start()
    {
        if (useTransform) GameManager.Instance.objectPooler.SpawnFromPool(objectTag, transform);
        else
        {
            GameManager.Instance.objectPooler.SpawnFromPool(objectTag, transform.position + offset, rotation, scale);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        switch (objectTag)
        {
            case ObjectPoolTag.Ice3:
            {
                if (useTransform) Gizmos.DrawWireCube(transform.position, transform.localScale);
                else Gizmos.DrawWireCube(transform.position + offset, scale);
                break;
            }
            case ObjectPoolTag.Ice:
            {
                if (useTransform) Gizmos.DrawWireCube(transform.position, transform.localScale);
                else Gizmos.DrawWireCube(transform.position + offset, scale);
                break;
            }
            default:
            {
                if (useTransform) Gizmos.DrawWireSphere(transform.position, transform.localScale.x/2);
                else Gizmos.DrawWireSphere(transform.position + offset, scale.x/2);
                break;
            }
        }
    }
}