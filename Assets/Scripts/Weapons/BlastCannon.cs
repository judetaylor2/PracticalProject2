using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCannon : MonoBehaviour
{
    public float weaponDamage, projectileSpeed, loadTime, maxAmmo, attackDelay;
    float currentAmmo;

    //other
    public ParticleSystem fireParticle;
    public Animator anim;
    public AudioSource weaponSound;
    public GameObject grenadeProjectile;
    public Transform shootPosition;
    
    //stopwatches
    float attackDelayStopwatch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackDelayStopwatch += Time.deltaTime;
        if (Input.GetAxis("Fire1") != 0 && attackDelayStopwatch >= attackDelay)
        {
            attackDelayStopwatch = 0;
            
            GameObject projectile = Instantiate(grenadeProjectile, shootPosition.position, shootPosition.rotation, null);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        }
    }
}
