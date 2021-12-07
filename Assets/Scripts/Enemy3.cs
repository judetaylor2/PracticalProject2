using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy2
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackTrigger, playerMask);
        bool playerInFollowRange = Physics.CheckSphere(transform.position, radiusTrigger, playerMask);
        
        if (playerInFollowRange)
        {
            Attack(attackType);
            transform.LookAt(playerStats.transform);
            
            if (!playerInAttackRange)
            {
                Follow();
                Debug.Log("follow");

            }
        }
        else
        {
            Patrol();
        }
    }

    public override void Follow()
    {
        rb.AddForce(transform.forward * moveSpeed);
    }
}
