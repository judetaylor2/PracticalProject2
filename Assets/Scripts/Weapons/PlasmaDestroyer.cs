using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaDestroyer : MonoBehaviour
{
    //weapon stats
    public float weaponDamage, loadTime, maxAmmo, attackDelay;
    float currentAmmo;

    //other
    public ParticleSystem fireParticle;
    public Animator anim;
    public AudioSource weaponSound;
    public GameObject burnObject;
    bool weaponAttacking = false;

    //stopwatches
    float damageStopwatch;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        damageStopwatch += Time.deltaTime;

        if (currentAmmo <= 0)
        {
            weaponSound.Stop();
            fireParticle.Stop();
        }
        else if (Input.GetAxis("Fire1") != 0)
        {
            weaponAttacking = true;
           
            fireParticle.Play();
         

            if (!weaponSound.isPlaying)
            {
                weaponSound.time = 1;
                weaponSound.Play();
            }
            else if (weaponSound.time >= 6)
            {
                weaponSound.Stop();
            }
        }
        else
        {
            fireParticle.Stop();
            damageStopwatch = 0;

            if (weaponAttacking)
            {
                weaponSound.Stop();
                weaponAttacking = false;
                weaponSound.time = 8;
                weaponSound.Play();

            }

            if (weaponSound.time >= 9)
            {
                weaponSound.Stop();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && damageStopwatch >= attackDelay && Input.GetAxis("Fire1") > 0 && currentAmmo > 0)
        {
            other.GetComponent<EnemyStats>().TakeDamage((int) weaponDamage);
            
            currentAmmo--;
            damageStopwatch = 0;
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
