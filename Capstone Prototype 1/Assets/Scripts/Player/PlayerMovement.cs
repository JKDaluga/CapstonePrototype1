using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    public float jumpForce = 5;
    public float drag = 60;
    public LayerMask canJump;

    private Vector3 moveDir;
    private Rigidbody body;
    private new CapsuleCollider collider;

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

        moveDir = new Vector3(Input.GetAxisRaw(InputManager.HORIZONTAL), 0, Input.GetAxisRaw(InputManager.VERTICAL));

        if (moveDir.magnitude > 0)
        {
            moveDir = transform.rotation * moveDir;
            moveDir = moveDir.normalized * moveSpeed;
            moveDir.y = body.velocity.y;

            body.velocity = moveDir;
        }
        else if (body.velocity.x != 0 || body.velocity.z != 0)
        {
            ApplyDrag();
        }

        if (Input.GetKey(KeyCode.Space) && Physics.Raycast(transform.position - new Vector3(0, collider.height/2 , 0), Vector3.down, 0.35f, canJump))
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }
    }

    private void ApplyDrag()
    {
        body.velocity = new Vector3(body.velocity.x * (1 - Mathf.Clamp01(drag*Time.deltaTime)), body.velocity.y, body.velocity.z * (1 - Mathf.Clamp01(drag*Time.deltaTime)));
    }
}
