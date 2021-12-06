using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    //weapon stats
    public int weaponDamage;
    public float maxAmmo, loadTime, reloadTime, attackDelay, hitDelay = 0.2f, maxDistance;
    float currentAmmo;
    bool isAttacking;
    
    //other
    public LayerMask enemyMask, groundMask;
    public Transform shootPoint;
    public ParticleSystem shootParticle, bulletParticle;
    public Animator anim;
    RaycastHit hit;
    
    //sound
    public AudioSource[] shootSound;
    public AudioSource reloadSound;

    //stopwatches
    float attackDelayStopwatch, hitDelayStopwatch;

    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        attackDelayStopwatch += Time.deltaTime;

        if (reloadSound.time >= 0.6f)
        {
            reloadSound.Stop();
        }
        
        if (Input.GetAxis("Fire1") > 0 && currentAmmo > 0 && attackDelayStopwatch >= attackDelay)
        {
            attackDelayStopwatch = 0;
            
            shootParticle.Play();
            anim.Play("Shoot");

            for (int i = 0; i < shootSound.Length; i++)
            {
                if (!shootSound[i].isPlaying)
                {
                    shootSound[i].Play();
                    break;
                }
                else if (shootSound[i].time >= 0.2f)
                {
                    shootSound[i].Stop();
                }
            }


            currentAmmo--;
            
            isAttacking = true;
        }
        else if (currentAmmo == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            anim.Play("Reload");
            shootParticle.Stop();
        }
        else
        {
            shootParticle.Stop();
        }

        if (isAttacking && hitDelayStopwatch >= hitDelay)
        {
            isAttacking = false;
            hitDelayStopwatch = 0;

            if (CheckRay())
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    hit.transform.GetComponent<EnemyStats>().TakeDamage(weaponDamage);
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Instantiate(bulletParticle, hit.point, Quaternion.LookRotation(hit.normal));
                }

            }
        }
        else if (isAttacking)
        {
            hitDelayStopwatch += Time.deltaTime;
        }
    }

    bool CheckRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward - new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), out hit, maxDistance, enemyMask | groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        reloadSound.time = 0.3f;
        reloadSound.Play();
    }
}
