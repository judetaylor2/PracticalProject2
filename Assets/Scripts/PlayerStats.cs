using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int health;
    public int Maxhealth = 100, fallDamageDistance = 20;
    public float fallDamageMultiplier = 1;
    float previousVelocity;
    Rigidbody rb;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        health = Maxhealth;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();

        previousVelocity = rb.velocity.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (previousVelocity >= fallDamageDistance && player.isGrounded && health > 0)
        {
            health -= (int)(previousVelocity * fallDamageMultiplier);
            Debug.Log($"Fall damage: {(int)(previousVelocity * fallDamageMultiplier)} | health {health}");
            previousVelocity = 0;
        }

        if (health <= 0)
        {
            
        }

        if (!player.isGrounded)
        {
            previousVelocity = -rb.velocity.y;
            //Debug.Log(previousVelocity.y);
            
        }
    }
}
