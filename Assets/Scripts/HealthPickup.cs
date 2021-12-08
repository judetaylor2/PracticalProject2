using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int health;
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
            
            if (p.health != p.Maxhealth)
            {
                if (p.health + health >= p.Maxhealth)
                {
                    p.health = p.Maxhealth;
                }
                else
                {
                    p.health += health;
                }

                healthSound.Play();
                healthCollider.enabled = false;
                meshRenderer.enabled = false;
                hasCollided = true;
            }
            
        }
    }
}
