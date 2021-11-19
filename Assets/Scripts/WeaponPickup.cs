using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    WeaponController weaponController;
    public int weaponIndex;
    
    void Start()
    {
        weaponController = GameObject.FindWithTag("Player").GetComponent<WeaponController>();    
    }

    void OnTriggerEnter()
    {
        weaponController.GiveWeapon(weaponIndex);
        Destroy(gameObject);
    }
}
