using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public AudioSource pickupSound;
    
    WeaponController weaponController;
    public int weaponIndex;
    
    void Start()
    {
        weaponController = GameObject.FindWithTag("Player").GetComponent<WeaponController>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponController.GiveWeapon(weaponIndex);

            pickupSound.time = 1.0f;
            pickupSound.Play();
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
        }
    }

    void Update()
    {
        if (pickupSound.time >= 1.5f && !gameObject.GetComponent<MeshRenderer>().enabled)
        {
            pickupSound.Stop();
            Destroy(gameObject);

        }

    }
}
