using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //public string ammoType;
    public int clipsAmount;
    AudioSource healthSound;
    BoxCollider healthCollider;
    MeshRenderer meshRenderer;
    bool hasCollided;

    void Start()
    {
        healthSound = GetComponent<AudioSource>();
        healthCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (hasCollided && !healthSound.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        PlayerStats p;
        if (other.TryGetComponent<PlayerStats>(out p))
        {   
        
            if (other.GetComponent<WeaponSelector>().currentWeapon.GetComponent<WeaponStats>())
            {
                GameObject g = other.GetComponent<WeaponSelector>().currentWeapon;
                
                if (g.GetComponent<WeaponStats>().currentAmmo < g.GetComponent<WeaponStats>().maxAmmo)
                {
                    if (g.GetComponent<WeaponStats>().GetComponent<AutoFire>() && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<AutoFire>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<BlastCannon>() && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<BlastCannon>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<Incinerator>())
                    {
                        g.GetComponent<WeaponStats>().GetComponent<Incinerator>().Reload();
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<MissileLauncher>() && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<MissileLauncher>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<NeonPistol>() && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<NeonPistol>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<PlasmaDestroyer>())
                    {
                        g.GetComponent<WeaponStats>().GetComponent<PlasmaDestroyer>().Reload();
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<ShockwaveBlaster>() && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<ShockwaveBlaster>().currentClips++;
                        DestroyPickup();
                    }
                    
                }
                
            }
            

        
            
        }
    }

    void DestroyPickup()
    {
        healthSound.Play();
        healthCollider.enabled = false;
        meshRenderer.enabled = false;
        hasCollided = true;
    }
}
