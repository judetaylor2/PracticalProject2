using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy1
{
    public GameObject EnemyProjectile;
    public float weaponDelay = 2;
    
    float weaponStopWatch;

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
        bool playerInShootRange = Physics.CheckSphere(transform.position, shootTrigger, playerMask);
        
        //Debug.Log(playerInAttackRange + " | " + playerInFollowRange);
        
        if (playerInAttackRange)
        {
            anim.SetBool("isAttacking", true);
            Attack(1);
            Debug.Log("attack");
            transform.LookAt(playerStats.transform.position);
        }
        else if (playerInShootRange)
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("isFollowing", false);
            anim.SetBool("isAttacking", false);
            Attack(2);
            agent.SetDestination(transform.position);
            transform.LookAt(playerStats.transform.position);
        }
        else if (playerInFollowRange)
        {
            anim.SetBool("isFollowing", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isShooting", false);
            Follow();
            Attack(2);
            Debug.Log("follow");
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFollowing", false);
            anim.SetBool("isShooting", false);
            Patrol();
            agent.SetDestination(transform.position);
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
