using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCannon : MonoBehaviour
{
    public float weaponDamage, projectileSpeed, loadTime, maxAmmo, attackDelay;
    float currentAmmo;

    //other
    public ParticleSystem shootParticle;
    Animator anim;
    public AudioSource weaponSound, reloadSound;
    public GameObject grenadeProjectile;
    public Transform shootPosition;
    
    //stopwatches
    float attackDelayStopwatch;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<WeaponStats>().currentAmmo = (int)currentAmmo;
        GetComponent<WeaponStats>().maxAmmo = (int)maxAmmo;
        
        attackDelayStopwatch += Time.deltaTime;
        if (Input.GetAxis("Fire1") != 0 && attackDelayStopwatch >= attackDelay && currentAmmo > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            attackDelayStopwatch = 0;
            
            anim.Play("Shoot");
            shootParticle.Play();
            weaponSound.Play();

            currentAmmo--;
            
            
            GameObject projectile = Instantiate(grenadeProjectile, shootPosition.position, shootPosition.rotation, null);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        }
        else if (currentAmmo <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            anim.Play("Reload");
        }
    }
    
    public void Reload()
    {
        currentAmmo = maxAmmo;
        reloadSound.Play();
    }
}
