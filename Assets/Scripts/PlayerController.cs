using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    Rigidbody rb;
    public float regularMoveSpeed, duckMoveSpeed, jumpHeight, duckHeight, gravity, forwardJumpMultiplierValue;
    float moveSpeed, regularHeight, lastActiveInputX, lastActiveInputZ;
    Vector3 lastGroundedDirectionRight, lastGroundedDirectionForward;
    
    float jumpStopWatch, forwardJumpMultiplier;
    
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

        forwardJumpMultiplier = forwardJumpMultiplierValue;
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
            moveSpeed = duckMoveSpeed;
        }
        else if(!Input.GetButton("Duck") && isGrounded)
        {
            transform.localScale = new Vector3(1f, regularHeight, 1f);
            moveSpeed = regularMoveSpeed;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(-Physics.gravity.y * Vector3.up * jumpHeight);
        }

        if (rb.velocity.y <= 0 && !isGrounded)
        {
            rb.AddForce(-Vector3.up * gravity);
        }

        if (isGrounded)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            rb.AddForce(move.normalized * moveSpeed);


            lastActiveInputX = x;
            lastActiveInputZ = z;
            lastGroundedDirectionRight = transform.right;
            lastGroundedDirectionForward = transform.forward;

            jumpStopWatch = 0;
            forwardJumpMultiplier = forwardJumpMultiplierValue;
        }
        else
        {
            Vector3 move1 = lastGroundedDirectionRight * lastActiveInputX + lastGroundedDirectionForward * lastActiveInputZ;

            jumpStopWatch += Time.deltaTime;
            if (jumpStopWatch >= 0.4f && forwardJumpMultiplier > 4.5)
            {
                forwardJumpMultiplier --;
            }
            
            rb.AddForce(move1.normalized * rb.velocity.magnitude * forwardJumpMultiplier);

            Debug.Log(forwardJumpMultiplier);
        }

    }
}
