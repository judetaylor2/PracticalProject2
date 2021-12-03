using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    public int burnDamage;
    
    public float burnTime, burnDamageDelay;
    [HideInInspector] public float burnDamageStopwatch, burnTimeStopwatch;

    public ParticleSystem fireParticles;

    // Update is called once per frame
    void Update()
    {
        burnDamageStopwatch += Time.deltaTime;
        burnTimeStopwatch += Time.deltaTime;

        if (burnTimeStopwatch >= burnTime)
        {
            Destroy(gameObject);
        }
        else if (burnDamageStopwatch >= burnDamageDelay)
        {
            //transform.parent.GetComponent<EnemyStats>().TakeDamage(burnDamage);
            burnDamageStopwatch = 0;
        }

    }
}
