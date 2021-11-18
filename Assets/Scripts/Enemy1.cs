using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    //main
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    public int attackDamage = 2;
    public float moveSpeed, rotateSpeed, radiusTrigger, attackTrigger, shootTrigger,attackDelay = 2;
    protected Animator anim;
    
    //patrol state
    public Vector2 patrolTimes;
    protected int patrolDirection, patrolMovement;
    protected float patrolStopWatch, patrolTime, attackStopWatch;

    //script references
    public LayerMask playerMask;
    protected PlayerStats playerStats;


    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackTrigger);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusTrigger);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, shootTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackTrigger, playerMask);
        bool playerInFollowRange = Physics.CheckSphere(transform.position, radiusTrigger, playerMask);
        
        //Debug.Log(playerInAttackRange + " | " + playerInFollowRange);
        
        if (playerInAttackRange)
        {
            anim.SetBool("isAttacking", true);
            Attack();
            Debug.Log("attack");
            
            transform.LookAt(new Vector3(playerStats.transform.position.x, transform.position.y , playerStats.transform.position.z));
        }
        else if (playerInFollowRange)
        {
            anim.SetBool("isFollowing", true);
            anim.SetBool("isAttacking", false);
            Follow();
            Debug.Log("follow");
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isFollowing", false);
            Patrol();
            agent.SetDestination(transform.position);
        }
    }

    public virtual void Attack(int attackIndex = 1)
    {        
        attackStopWatch += Time.deltaTime;

        if (attackStopWatch >= attackDelay)
        {
            playerStats.TakeDamage(attackDamage);
            attackStopWatch = 0;
        }
    }

    public virtual void Follow()
    {
        agent.SetDestination(playerStats.transform.position);
    }

    public virtual void Patrol()
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
