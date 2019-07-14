using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ice : WaterState, IPooledObject
{
    [SerializeField]
    private float moveSpeed = 50;
    private Rigidbody body;

    // Start is called before the first frame update
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
            if ( angle > -45 && angle <= 45) body.AddForce(transform.right.normalized*moveSpeed, ForceMode.VelocityChange);
            else if (angle > 45 && angle <= 135) body.AddForce(transform.forward.normalized*moveSpeed, ForceMode.VelocityChange);
            else if (angle > 135 || angle <= -135) body.AddForce(-transform.right.normalized*moveSpeed, ForceMode.VelocityChange);
            else body.AddForce(-transform.forward.normalized*moveSpeed, ForceMode.VelocityChange);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject obj = GameManager.Instance.objectPooler.SpawnFromPool(ObjectPooler.STEAM_KEY, transform);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        float angle = Vector3.Angle(body.velocity, -other.impulse);
        if (Vector3.Angle(body.velocity, -other.impulse) <= 1)
        {
            Debug.Log("Angle is " + angle + ", and the object collided with is " + other.gameObject.name);
            body.velocity = Vector3.zero;
        }
    }

    public void OnObjectSpawn()
    {
        Debug.Log("Ice On Spawn is Called");
        if (!body) body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
    }
}
