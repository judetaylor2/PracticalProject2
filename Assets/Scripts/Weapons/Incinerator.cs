using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MonoBehaviour
{
    //weapon stats
    public float weaponDamage, loadTime, maxAmmo, attackDelay;
    float currentAmmo;

    //other
    public ParticleSystem fireParticle;
    public Animator anim;
    public AudioSource weaponSound;
    public GameObject burnObject;

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
            anim.Play("Reload");
            weaponSound.Stop();
            fireParticle.Stop();
        }
        else if (Input.GetAxis("Fire1") != 0)
        {
            if (!fireParticle.isPlaying)
            fireParticle.Play();

            if (!weaponSound.isPlaying)
            {
                weaponSound.time = 10;
                weaponSound.Play();
            }
            else if (weaponSound.time >= 12)
            {
                weaponSound.Stop();
            }
        }
        else
        {
            fireParticle.Stop();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && damageStopwatch >= attackDelay && Input.GetAxis("Fire1") > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            other.GetComponent<EnemyStats>().TakeDamage((int) weaponDamage);

            if (!other.gameObject.GetComponentInChildren<Burn>())
            {
                Instantiate(burnObject, other.transform.position, other.transform.rotation, other.transform);
            }
            else
            {
                other.gameObject.GetComponentInChildren<Burn>().burnTimeStopwatch = 0;
            }
            
            currentAmmo--;
            damageStopwatch = 0;
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
