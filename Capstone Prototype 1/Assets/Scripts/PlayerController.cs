using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;

    public float jumpForce = 5;

    private Vector3 moveDir;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveDir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float y = moveDir.y;

        moveDir = transform.right * Input.GetAxis("Horizontal");
        moveDir += transform.forward * Input.GetAxis("Vertical");
        moveDir = moveDir.normalized * moveSpeed;

        if (controller.isGrounded)
        { 
            if (Input.GetKey(KeyCode.Space)) moveDir.y = jumpForce;
        }
        else moveDir.y = y + Physics.gravity.y*Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }
}
