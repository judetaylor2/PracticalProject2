using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy1
{
    public GameObject EnemyProjectile;
    public float weaponDelay = 2;
    
    float weaponStopWatch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackTrigger, playerMask);
        bool playerInFollowRange = Physics.CheckSphere(transform.position, radiusTrigger, playerMask);
        
        //Debug.Log(playerInAttackRange + " | " + playerInFollowRange);
        
        if (playerInAttackRange)
        {
            Attack(1);
            Debug.Log("attack");
        }
        else if (playerInFollowRange)
        {
            Follow();
            Attack(2);
            Debug.Log("follow");
        }
        else
        {
            Patrol();
        }
    }

    public override void Attack(int attackIndex)
    {
        if (attackIndex == 1)
        {
            //melee attack
            base.Attack(1);
        }
        else if (attackIndex == 2)
        {
            //shoot projectile
            weaponStopWatch += Time.deltaTime;
            
            if (weaponStopWatch >= weaponDelay)
            {
                Instantiate(EnemyProjectile, transform);
                weaponStopWatch = 0;
            }
        }
    }
}
