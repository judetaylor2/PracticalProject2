using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    Rigidbody rb;
    public float moveSpeed, rotateSpeed;
    public Vector2 patrolTimes;
    int patrolDirection, patrolMovement;
    float patrolStopWatch, patrolTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (patrolStopWatch >= patrolTime)
        {
            patrolDirection = Random.Range(-180, 180);
            
            if (patrolMovement == 0)
            {
                patrolMovement = 1;
            }
            else if (patrolMovement == 1)
            {
                patrolMovement = 0;
            }
            
            patrolTime = Random.Range(patrolTimes.x, patrolTimes.y);
            patrolStopWatch = 0;

        }

        if (patrolMovement == 0)
        {
            transform.Rotate(new Vector3(transform.rotation.x, patrolDirection, transform.rotation.z), rotateSpeed * Time.deltaTime);
        }
        
        patrolStopWatch += Time.deltaTime;

        rb.AddForce(transform.forward * patrolMovement * moveSpeed * Time.deltaTime);
    }
}
