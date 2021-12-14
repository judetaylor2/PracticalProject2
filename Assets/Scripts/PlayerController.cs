using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    Rigidbody rb;
    public float regularMoveSpeed, duckMoveSpeed, jumpHeight, duckHeight, initialGravity, constantGravity = -9.81f, slopeLimit;
    float moveSpeed, regularHeight, lastActiveInputX, lastActiveInputZ;
    Vector3 lastGroundedDirectionRight, lastGroundedDirectionForward;
    CapsuleCollider capsuleCollider;
    
    public Transform cameraTransform;
    float regularCameraPositionY;
    public float duckCameraPositionY;
    
    float fallGravity, gravityStopWatch;
    
    //Ground Check
    public bool isGrounded = true;
    public Transform groundCheck, roofCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    RaycastHit slopeHit;

    //audio
    public AudioSource moveSoundSoft;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        regularCameraPositionY = cameraTransform.localPosition.y;
        
        regularHeight = capsuleCollider.height;
        moveSpeed = regularMoveSpeed;

        fallGravity = initialGravity;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        Gizmos.DrawWireSphere(roofCheck.position, groundDistance);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //add constant gravity to the player
        if (isGrounded && slopeHit.normal.y > slopeLimit)
        {
        rb.AddForce(slopeHit.normal * constantGravity);
            
        }
        
        else if (isGrounded && slopeHit.normal.y <= slopeLimit)
        {
        rb.AddForce(Vector3.up * constantGravity * 10);

        }

        else
        {
        rb.AddForce(Vector3.up * constantGravity);

        }

        Debug.Log("Slopehit Normal y = " + slopeHit.normal.y);

        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask | LayerMask.NameToLayer("Box"));

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        
        if(Input.GetButton("Duck"))
        {
            capsuleCollider.height = duckHeight;
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, new Vector3(cameraTransform.localPosition.x, duckCameraPositionY, cameraTransform.localPosition.z), 0.5f);
            rb.AddForce(-transform.up * 6);
            //cameraTransform.position = new Vector3(cameraTransform.position.x, duckCameraPositionY, cameraTransform.position.z);
        }
        else if(!Input.GetButton("Duck") && isGrounded  && !Physics.CheckSphere(roofCheck.position, groundDistance, groundMask))
        {
            capsuleCollider.height = regularHeight;
            moveSpeed = regularMoveSpeed;

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, new Vector3(cameraTransform.localPosition.x, regularCameraPositionY, cameraTransform.localPosition.z), 0.5f);

            //cameraTransform.position = new Vector3(cameraTransform.position.x, regularCameraPositionY, cameraTransform.position.z);
        }

        //sound
        if (moveSoundSoft.time >= 4.5f)
        moveSoundSoft.Stop();

        if (isGrounded)
        {
            Physics.Raycast(transform.position, Vector3.down, out slopeHit, groundDistance * 5, groundMask);
            
            //player move
            Vector3 move = transform.right * x + transform.forward * z;
            Vector3 slopeMoveDirection = Vector3.ProjectOnPlane(move.normalized, slopeHit.normal * 5);
            Debug.Log("Slopehit normal: " + slopeHit.normal);
            rb.AddForce(slopeMoveDirection * moveSpeed);

            if (!moveSoundSoft.isPlaying && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            moveSoundSoft.Play();

            lastActiveInputX = Input.GetAxis("Horizontal");
            lastActiveInputZ = Input.GetAxis("Vertical");
            lastGroundedDirectionRight = transform.right;
            lastGroundedDirectionForward = transform.forward;

            gravityStopWatch = 0;
            fallGravity = initialGravity;

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
                rb.AddForce(-Vector3.up * fallGravity, ForceMode.Acceleration);
            }
            
            //gravity acceleration
            gravityStopWatch += Time.deltaTime;
            if (gravityStopWatch >= 0.1f && rb.velocity.y <= 0)
            {
                fallGravity += initialGravity / 10;
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
        if(Input.GetButtonDown("Jump") && isGrounded && slopeHit.normal.y > slopeLimit)
        {
            rb.AddForce(/*-Physics.gravity.y * */Vector3.up * jumpHeight, ForceMode.Acceleration);
        }

        //sound
        if (!moveSoundSoft.isPlaying)
        {
            moveSoundSoft.time = 4.1f;
        }
        
        
    }
}
