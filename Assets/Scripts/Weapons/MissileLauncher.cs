using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public float weaponDamage, projectileSpeed, loadTime, maxAmmo, attackDelay;
    float currentAmmo;

    //other
    public ParticleSystem shootParticle;
    Animator anim;
    public AudioSource weaponSound, reloadSound;
    public GameObject grenadeProjectile;
    public Transform shootPosition;
    [HideInInspector] public int currentClips;
    public int maxClips;
    
    
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
        GetComponent<WeaponStats>().maxClips = maxClips;
        GetComponent<WeaponStats>().currentClips = currentClips;
        
        attackDelayStopwatch += Time.deltaTime;
        if (Input.GetAxis("Fire1") != 0 && attackDelayStopwatch >= attackDelay && currentAmmo > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            attackDelayStopwatch = 0;
            
            anim.Play("Shoot");
            shootParticle.Play();
            weaponSound.Play();

            currentAmmo--;
            
            
            GameObject projectile = Instantiate(grenadeProjectile, shootPosition.position, shootPosition.rotation, null);
        }
        else if (((currentAmmo <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload")) || Input.GetAxis("Reload") != 0) && currentClips > 0)
        {
            anim.Play("Reload");
        }
    }
    
    public void Reload()
    {
        if (currentClips > 0)
        {
            currentAmmo = maxAmmo;
            reloadSound.Play();
            currentClips--;
            
        }
    }
}
