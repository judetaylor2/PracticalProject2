using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount;
    public float knockback, triggerDestroyTime;
    SphereCollider sc;
    
    float DestroyStopWatch;

    void Update()
    {
        if (DestroyStopWatch >= triggerDestroyTime)
        {
            sc.enabled = false;
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
