using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonPistol : MonoBehaviour
{
    //weapon stats
    public int weaponDamage;
    public float maxAmmo, loadTime, reloadTime, attackDelay, hitDelay = 0.2f, maxDistance;
    float currentAmmo;
    bool isAttacking;
    
    //other
    public LayerMask enemyMask, groundMask;
    public Transform shootPoint;
    public ParticleSystem shootParticle, bulletParticle, bulletSparksParticle;
    public Animator anim;
    RaycastHit hit;
    [HideInInspector] public int currentClips;
    public int maxClips;
    
    
    //sound
    public AudioSource[] shootSound;
    public AudioSource reloadSound;

    //stopwatches
    float attackDelayStopwatch, hitDelayStopwatch;

    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        currentClips = maxClips;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<WeaponStats>().currentAmmo = (int)currentAmmo;
        GetComponent<WeaponStats>().maxAmmo = (int)maxAmmo;
        GetComponent<WeaponStats>().maxClips = maxClips;
        GetComponent<WeaponStats>().currentClips = currentClips;
        
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

            currentAmmo--;
            
            isAttacking = true;
        }
        else if (((currentAmmo == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload")) || Input.GetAxis("Reload") != 0) && currentClips > 0)
        {
            anim.Play("Reload");
        }

        if (isAttacking && hitDelayStopwatch >= hitDelay)
        {
            isAttacking = false;
            hitDelayStopwatch = 0;

            if (CheckRay())
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Box"))
                {
                    hit.transform.GetComponent<EnemyStats>().TakeDamage(weaponDamage);
                    Instantiate(bulletSparksParticle, hit.point, Quaternion.LookRotation(hit.normal));
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
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, maxDistance, enemyMask | groundMask))
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
        if (currentClips > 0)
        {
            currentAmmo = maxAmmo;
            reloadSound.time = 0.3f;
            reloadSound.Play();
            currentClips--;
            
        }
    }
}
