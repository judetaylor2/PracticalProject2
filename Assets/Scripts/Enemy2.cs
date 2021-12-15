using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy1
{
    public Transform shootPoint;
    public GameObject EnemyProjectile;
    public float weaponDelay = 2;
    public ParticleSystem shootParticle, bulletParticle;
    RaycastHit hit;
    public LayerMask groundMask;

    public int attackType;
    
    protected float weaponStopWatch;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    void OnDrawGizmos()
    {
        if (hit.point != null)
        Gizmos.DrawLine(shootPoint.position, hit.point);
        Gizmos.DrawSphere(hit.point, 0.5f);
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
            transform.LookAt(new Vector3(playerStats.transform.position.x, transform.position.y , playerStats.transform.position.z));
        }
        else if (playerInShootRange)
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("isFollowing", false);
            anim.SetBool("isAttacking", false);
            //Attack(attackType);
            agent.SetDestination(transform.position);
            transform.LookAt(new Vector3(playerStats.transform.position.x, transform.position.y , playerStats.transform.position.z));
        }
        else if (playerInFollowRange)
        {
            anim.SetBool("isFollowing", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isShooting", false);
            Follow();
            //Attack(attackType);
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
                Instantiate(EnemyProjectile, shootPoint);
                weaponStopWatch = 0;
            }
        }
        else if (attackIndex == 3)
        {
            //weaponStopWatch += Time.deltaTime;
            
            //if (weaponStopWatch >= weaponDelay)
            //{
                if (CheckRay())
                {
                    shootParticle.Play();
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        hit.transform.GetComponent<PlayerStats>().TakeDamage(attackDamage);
                    }
                    else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        Instantiate(bulletParticle, hit.point, Quaternion.LookRotation(hit.normal));
                    }

                    weaponStopWatch = 0;

                }
            //}
        }
    }
    
    bool CheckRay()
    {
        shootPoint.LookAt(GameObject.FindWithTag("Player").transform);
        if (Physics.Raycast(shootPoint.position, shootPoint.forward /* - new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f))*/, out hit, 30, playerMask | groundMask | LayerMask.NameToLayer("Box")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

