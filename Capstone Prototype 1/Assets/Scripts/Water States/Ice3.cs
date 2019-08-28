using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ice3 : WaterState, IPooledObject
{
    private Transform target;
    private Rigidbody body;
    private bool beingHeld = false;

    // Start is called before the first frame update
    private void Awake()
    {
        target = null;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (beingHeld)
        {
            Vector3 distance = target.position - transform.position;
            if (distance.magnitude > 5)
            {
                DropObj();
                body.velocity = Vector3.ClampMagnitude(body.velocity, 5);
            }
            else if (distance.magnitude > 0.05f)
            {
                Vector3 direction = Vector3.ClampMagnitude(distance * Mathf.Max(distance.sqrMagnitude, 10), 25);
                body.velocity = Vector3.Lerp(body.velocity, direction, 0.125f);
            }
            else if (body.velocity.magnitude > 0)
            {
                body.velocity = body.velocity * 0.01f;
            }
        }
    }

    public override void Interact(GameObject player)
    {
        if (beingHeld)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DropObj();
                Vector3 direction = Vector3.Normalize(transform.position - Camera.main.transform.position);
                body.AddForce(Vector3.ClampMagnitude(direction * body.mass * 10, 25), ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (beingHeld)
            {
                DropObj();
            }
            else
            {
                PickUpObj(Camera.main.transform.GetChild(0));
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DropObj();
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.STEAM_KEY, transform);
            gameObject.SetActive(false);
        }
    }

    public void OnObjectSpawn()
    {
        Debug.Log("Ice On Spawn is Called");
        if (!body) body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        Vector3 scale = transform.localScale;
        body.mass = (scale.x * scale.y * scale.z) * 5;
        DropObj();
    }

    private void PickUpObj(Transform targetPos)
    {
        beingHeld = true;
        body.useGravity = false;
        target = targetPos;
    }

    private void DropObj()
    {
        beingHeld = false;
        body.useGravity = true;
        target = null;
    }
}