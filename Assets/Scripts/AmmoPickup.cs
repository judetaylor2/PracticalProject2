using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public string ammoType;
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
            string[] s = {"autofire","blastcannon","incinerator","neonpistol","plasmadestroyer","shockwaveblaster","lightningblade"};

            if (ammoType == "random")
            {
                ammoType = s[Random.Range(0, s.Length)];
            }
            
            foreach (GameObject g in other.GetComponent<WeaponSelector>().playerWeapons)
            {
                if (g.GetComponent<WeaponStats>().currentAmmo < g.GetComponent<WeaponStats>().maxAmmo)
                {
                    if (g.GetComponent<WeaponStats>().GetComponent<AutoFire>() && ammoType == "autofire" && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<AutoFire>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<BlastCannon>() && ammoType == "blastcannon" && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<BlastCannon>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<Incinerator>() && ammoType == "incinerator")
                    {
                        g.GetComponent<WeaponStats>().GetComponent<Incinerator>().Reload();
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<MissileLauncher>() && ammoType == "missilelauncher" && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<MissileLauncher>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<NeonPistol>() && ammoType == "neonpistol" && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
                    {
                        g.GetComponent<WeaponStats>().GetComponent<NeonPistol>().currentClips++;
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<PlasmaDestroyer>() && ammoType == "plasmadestroyer")
                    {
                        g.GetComponent<WeaponStats>().GetComponent<PlasmaDestroyer>().Reload();
                        DestroyPickup();
                    }
                    else if (g.GetComponent<WeaponStats>().GetComponent<ShockwaveBlaster>() && ammoType == "shockwaveblaster" && g.GetComponent<WeaponStats>().currentClips < g.GetComponent<WeaponStats>().maxClips)
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
