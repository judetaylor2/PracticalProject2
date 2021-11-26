using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscan : WeaponController
{
    public Transform weaponModelFront;
    RaycastHit hit;
    public ParticleSystem shootEffect, bulletHole;
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(shootPoint.position, hit.point);
    }

    public override void Update()
    {
        attackStopWatch += Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0 && attackStopWatch >= attackDelay && !anim.GetBool("isAttacking"))
        {
            Shoot(100, transform.forward);
            attackStopWatch = 0;
            anim.SetBool("isAttacking", true);
        }
    }

    public override void Shoot(float distance, Vector3 direction)
    {
        Instantiate(shootEffect, weaponModelFront.position, weaponModelFront.rotation/*Quaternion.LookRotation(hit.normal)*/, weaponModelFront);
        
        if (Physics.Raycast(shootPoint.position, direction, out hit, distance, enemyMask | groundMask))
        {
            Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            
            EnemyStats enemy;
            if (hit.transform.TryGetComponent<EnemyStats>(out enemy))
            {
                enemy.TakeDamage(weaponDamage);
            }
        }
    }

    public void StopShoot()
    {
        anim.SetBool("isAttacking", false);
    }
}
