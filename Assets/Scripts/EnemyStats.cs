using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    public AudioSource hitSound;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if (currentHealth <= 0 && !hitSound.isPlaying)
        {
            Destroy(gameObject);
        }
        else if (currentHealth <= 0 && hitSound.isPlaying)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }    

        if (hitSound.time >= 3f)
        {
            hitSound.Stop();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        hitSound.time = 0;
        hitSound.Play();
    }
}
