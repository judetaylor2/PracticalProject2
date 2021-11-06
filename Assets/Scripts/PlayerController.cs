using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public float normalSpeed, duckSpeed, normalHeight, duckHeight ,gravity, jumpHeight;

    float moveSpeed, playerHeight;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        moveSpeed = normalSpeed;
        transform.localScale = new Vector3(transform.localScale.x, duckHeight, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //player move
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        //gravity move
        controller.Move(velocity * Time.deltaTime);

        //player duck
        if (Input.GetButton("Duck"))
        {
            transform.localScale = new Vector3(transform.localScale.x, duckHeight, transform.localScale.z);
            moveSpeed = duckSpeed;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
            moveSpeed = normalSpeed;
        }

    }
}
