using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    AudioSource pickupSound;
    
    WeaponSelector weaponSelector;
    public GameObject weapon;
    
    void Start()
    {
        weaponSelector = GameObject.FindWithTag("Player").GetComponent<WeaponSelector>();
        pickupSound = GetComponent<AudioSource>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponSelector.GiveWeapon(weapon);

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
