using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    public float jumpForce = 5;
    public float drag = 100;

    private Vector3 moveDir;
    private Rigidbody body;
    private CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        moveDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        moveDir = new Vector3(Input.GetAxis(InputManager.HORIZONTAL), 0, Input.GetAxis(InputManager.VERTICAL));

        if (moveDir.magnitude > 0)
        {
            moveDir = transform.rotation * moveDir;
            moveDir = moveDir.normalized * moveSpeed;
            moveDir.y = body.velocity.y;

            body.velocity = moveDir;
        }
        else if (body.velocity.magnitude > 0)
        {
            ApplyDrag();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position - new Vector3(0, collider.height/2, 0), Vector3.down, 0.1f))
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }
    }

    private void ApplyDrag()
    {
        body.velocity = new Vector3(body.velocity.x * (1 - drag*Time.deltaTime), body.velocity.y, body.velocity.z * (1 - drag*Time.deltaTime));
    }
}
