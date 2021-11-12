using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //main
    protected Rigidbody rb;
    public int attackDamage = 2;
    public float moveSpeed, rotateSpeed, radiusTrigger, attackTrigger, attackDelay = 2;
    
    //patrol state
    public Vector2 patrolTimes;
    protected int patrolDirection, patrolMovement;
    protected float patrolStopWatch, patrolTime, attackStopWatch;

    //script references
    public LayerMask playerMask;
    protected PlayerStats playerStats;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackTrigger);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackTrigger, playerMask);
        bool playerInFollowRange = Physics.CheckSphere(transform.position, radiusTrigger, playerMask);
        
        //Debug.Log(playerInAttackRange + " | " + playerInFollowRange);
        
        if (playerInAttackRange)
        {
            Attack();
            Debug.Log("attack");
        }
        else if (playerInFollowRange)
        {
            Follow();
            Debug.Log("follow");
        }
        else
        {
            Patrol();
        }
    }

    public virtual void Attack(int attackIndex = 1)
    {        
        attackStopWatch += Time.deltaTime;

        if (attackStopWatch >= attackDelay)
        {
            playerStats.health -= attackDamage;
            attackStopWatch = 0;
        }
    }

    public void Follow()
    {

    }

    public void Patrol()
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
