using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    Rigidbody rb;
    public float regularMoveSpeed, duckMoveSpeed, jumpHeight, duckHeight, initialGravity;
    float moveSpeed, regularHeight, lastActiveInputX, lastActiveInputZ;
    Vector3 lastGroundedDirectionRight, lastGroundedDirectionForward;
    
    float gravity, gravityStopWatch;
    
    //Ground Check
    bool isGrounded = true;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        regularHeight = transform.localScale.y;
        moveSpeed = regularMoveSpeed;

        gravity = initialGravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        
        if(Input.GetButtonDown("Duck"))
        {
            transform.localScale = new Vector3(1f, duckHeight, 1f);
            
        }
        else if(!Input.GetButton("Duck") && isGrounded)
        {
            transform.localScale = new Vector3(1f, regularHeight, 1f);
            moveSpeed = regularMoveSpeed;
        }


        if (isGrounded)
        {
            //player move
            Vector3 move = transform.right * x + transform.forward * z;
            rb.AddForce(move.normalized * moveSpeed);


            lastActiveInputX = Input.GetAxis("Horizontal");
            lastActiveInputZ = Input.GetAxis("Vertical");
            lastGroundedDirectionRight = transform.right;
            lastGroundedDirectionForward = transform.forward;

            gravityStopWatch = 0;
            gravity = initialGravity;

            //only change movespeed when grounded to avoid decrease in midair momentum
            if (Input.GetButton("Duck"))
            {
                moveSpeed = duckMoveSpeed;
            }
        }
        else
        {

            if (rb.velocity.y <= 0 && !isGrounded)
            {
                rb.AddForce(-Vector3.up * gravity, ForceMode.Acceleration);
            }
            
            //gravity acceleration
            gravityStopWatch += Time.deltaTime;
            if (gravityStopWatch >= 0.1f && rb.velocity.y <= 0)
            {
                gravity += initialGravity / 10;
            }
            
            //forward jump acceleration
            Vector3 move = lastGroundedDirectionRight * lastActiveInputX + lastGroundedDirectionForward * lastActiveInputZ;
            
            rb.AddForce(move.normalized * moveSpeed);
            //Debug.Log("x: "lastActiveInputX + " z:" + lastActiveInputZ);

        }

    }

    void Update()
    {
        //jump in regular update method to avoid inconsistent movement
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(/*-Physics.gravity.y * */Vector3.up * jumpHeight, ForceMode.Acceleration);
        }
    }
}
