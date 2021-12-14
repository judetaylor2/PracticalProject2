using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public int damage;
    public float damageDelay;

    float damageStopwatch;
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            damageStopwatch += Time.deltaTime;

            if (damageStopwatch >= damageDelay)
            {
                other.GetComponent<PlayerStats>().TakeDamage(damage);
                damageStopwatch = 0;
            }
        }
    }

    void OnTriggerExit()
    {
        damageStopwatch = 0;
    }
}
