using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public int weaponDamage;
    public float attackDelay, equipTime;
    bool isAttacking;

    float attackStopWatch;
    
    public LayerMask enemyMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackStopWatch += Time.deltaTime;
        
        if (Input.GetAxis("Fire1") > 0 && attackStopWatch >= attackDelay && !isAttacking)
        {
            Attack();
            attackStopWatch = 0;
        }
        else if (attackStopWatch >= attackDelay && isAttacking)
        {
            isAttacking = false;
        }
    }

    void Attack()
    {
        isAttacking = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (isAttacking && other.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().TakeDamage(weaponDamage);
            isAttacking = false;
        }
    }
}
