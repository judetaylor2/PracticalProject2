using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount;
    public float knockback, triggerDestroyTime;
    SphereCollider sc;
    AudioSource explosionSound;
    
    float DestroyStopWatch;

    void Start()
    {
        sc = GetComponent<SphereCollider>();
        explosionSound = GetComponent<AudioSource>();
        explosionSound.time = 0.5f;
    }
    
    void Update()
    {
        DestroyStopWatch += Time.deltaTime;
        
        if (DestroyStopWatch >= triggerDestroyTime)
        {
            sc.enabled = false;
        }

        if (explosionSound.time >= 2f)
        {
            explosionSound.Stop();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(damageAmount);
            
            Rigidbody rb;
            if (other.TryGetComponent<Rigidbody>(out rb))
            {
                rb.AddForce((rb.transform.position - transform.position) * knockback);
            }
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(damageAmount, 0);

            Rigidbody rb;
            if (other.TryGetComponent<Rigidbody>(out rb))
            {
                rb.AddForce((rb.transform.position - transform.position) * knockback);
            }
        }

    }
}
