using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public int weaponDamage;
    public float attackDelay, equipTime;
    bool isAttacking;

    float attackStopWatch;
    
    public LayerMask enemyMask, groundMask;

    Animator anim;
    public AudioSource attackSound, hitSound;
    public ParticleSystem attackHole;
    [HideInInspector] public bool canReload;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
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

        if (hitSound.time >= 12.5f)
        {
            hitSound.Stop();
        }
    }

    public void Attack()
    {
        isAttacking = true;
        
        RaycastHit r;
        if (Physics.Raycast(transform.position, transform.forward, out r, 3, groundMask))
        {
            Instantiate(attackHole, r.point, Quaternion.LookRotation(r.normal));
            
            hitSound.time = 9.3f;
            hitSound.Play();
            
        }
    }

    public void PlayAttackSound()
    {
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
