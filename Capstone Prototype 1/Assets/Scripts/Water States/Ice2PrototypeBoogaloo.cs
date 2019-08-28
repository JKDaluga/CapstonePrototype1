using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ice2PrototypeBoogaloo : WaterState, IPooledObject
{
    [SerializeField]
    private float moveSpeed = 50;
    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

    }

    public override void Interact(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 direction = transform.position - player.transform.position;
            float angle = Vector3.SignedAngle(direction, transform.right, Vector3.up);
            if (angle > -45 && angle <= 45) body.velocity = transform.right.normalized * moveSpeed; //body.AddForce(transform.right.normalized * moveSpeed, ForceMode.VelocityChange);
            else if (angle > 45 && angle <= 135) body.velocity = transform.forward.normalized * moveSpeed;
            else if (angle > 135 || angle <= -135) body.velocity = -transform.right.normalized * moveSpeed;
            else body.velocity = -transform.forward.normalized * moveSpeed;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.WATER_KEY, transform);
            obj.transform.localScale = new Vector3(2, 0.2f, 2);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        float angle = Vector3.Angle(body.velocity, -other.impulse);
        print(Vector3.Angle(body.velocity, -other.impulse));
        if (true/*Vector3.Angle(body.velocity, -other.impulse) <= 1*/)
        {
            if(other.impulse.magnitude >=10)
            {
                body.velocity = new Vector3(other.impulse.x, 0, other.impulse.z).normalized * moveSpeed;
                Invoke("stop", .06f);
            }
        }
    }

    private void stop()
    {
        print("stop");
        body.velocity = Vector3.zero;
    }

    public void OnObjectSpawn()
    {
        Debug.Log("Ice On Spawn is Called");
        if (!body) body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
    }
}
