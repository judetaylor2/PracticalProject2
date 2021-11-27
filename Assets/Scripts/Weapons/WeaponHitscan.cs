using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscan : WeaponController
{
    public Transform weaponModelFront;
    RaycastHit hit;
    public ParticleSystem shootEffect, bulletHole;
    Animator anim;
    public AudioSource[] shootSound;
    public AudioSource reloadSound;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;    
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(shootPoint.position, hit.point);
    }

    public override void Update()
    {
        if (currentAmmo <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            anim.Play("Reload");
            currentAmmo = maxAmmo;
            reloadSound.Play();
        }

        if (reloadSound.time >= 0.6f)
        {
            reloadSound.Stop();
            reloadSound.time = 0;
        }
        
        attackStopWatch += Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0 && attackStopWatch >= attackDelay /*&& !anim.GetBool("isAttacking")*/ && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            Shoot(100, transform.forward);
            attackStopWatch = 0;
        }
    }

    public override void Shoot(float distance, Vector3 direction)
    {
        shootEffect.time = 0;
        shootEffect.Play();
        
        anim.Play("Shoot");
        
        if (!shootSound[0].isPlaying)
        shootSound[0].Play();
        else if (!shootSound[1].isPlaying)
        shootSound[1].Play();
        else if (!shootSound[2].isPlaying)
        shootSound[2].Play();
        else if (!shootSound[3].isPlaying)
        shootSound[3].Play();
        else if (!shootSound[4].isPlaying)
        shootSound[4].Play();
        

        if (Physics.Raycast(shootPoint.position, direction, out hit, distance, enemyMask | groundMask))
        {
            
            if (hit.collider.gameObject.layer == groundMask)
            {
                Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

            }
            
            EnemyStats enemy;
            if (hit.transform.TryGetComponent<EnemyStats>(out enemy))
            {
                enemy.TakeDamage(weaponDamage);
            }
        }

        currentAmmo--;
    }

    public void StopShoot()
    {
        anim.SetBool("isAttacking", false);
    }
}
