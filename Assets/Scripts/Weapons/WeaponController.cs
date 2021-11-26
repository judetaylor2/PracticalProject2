using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int currentAmmo, maxAmmo, weaponDamage;
    public float attackDelay, equipTime, reloadTime;
    public Transform shootPoint;
    public LayerMask enemyMask, groundMask;

    protected float attackStopWatch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void Shoot(float distance, Vector3 direction)
    {
        
    }
}
