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

    Animator anim;
    AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        attackStopWatch += Time.deltaTime;
        
        if (Input.GetAxis("Fire1") > 0 && attackStopWatch >= attackDelay && !isAttacking && !anim.GetBool("isAttacking"))
        {
            anim.SetBool("isAttacking", true);
            attackStopWatch = 0;
        }
        else if (attackStopWatch >= attackDelay && isAttacking)
        {
            isAttacking = false;
        }
    }

    public void Attack()
    {
        isAttacking = true;
        attackSound.Play();
    }

    void OnTriggerStay(Collider other)
    {
        if (isAttacking && other.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().TakeDamage(weaponDamage);
            isAttacking = false;
        }
    }

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
