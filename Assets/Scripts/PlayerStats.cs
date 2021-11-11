using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public int health;
    public int Maxhealth = 100, fallDamageDistance = 20;
    public float fallDamageMultiplier = 1, damageUITime = 0.01f;
    float previousVelocity;
    Rigidbody rb;
    PlayerController player;
    public TextMeshProUGUI healthText;
    public Slider healthBar;
    public Image healthImage, damageImage;
    public Gradient healthColors;

    // Start is called before the first frame update
    void Start()
    {
        health = Maxhealth;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();

        previousVelocity = rb.velocity.y;

        damageImage.color = new Color (0, 0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (previousVelocity >= fallDamageDistance && player.isGrounded && health > 0)
        {
            int damage = (int)(previousVelocity * fallDamageMultiplier);
            TakeDamage(damage);
            
            StartCoroutine("DamageUI");
            
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

    void Update()
    {
        healthText.text = health.ToString();
        
        healthBar.value = health;

        healthImage.color = healthText.color = healthBar.fillRect.GetComponent<Image>().color = healthColors.Evaluate(healthBar.value / 100);

        //damageImage.color = Color.Lerp(Color.red, Color.blue, 0.5f);
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Damage: {(int)(previousVelocity * fallDamageMultiplier)} | health {health}");
        
        if (damage > 30)
        StartCoroutine("DamageUI");
    }
    
    IEnumerator DamageUI()
    {
        for (float i = 0; i < 1; i+= 0.15f)
        {
            damageImage.color = Color.Lerp(damageImage.color, new Color(1, 0, 0, 0.5f), i);
            yield return new WaitForSeconds(damageUITime);

        }

        yield return new WaitForSeconds(2);

        for (float i = 0; i < 1; i+= 0.01f)
        {
            damageImage.color = Color.Lerp(damageImage.color, new Color(0, 0, 0, 0), i);
            yield return new WaitForSeconds(damageUITime * 5000000);

        }

    }

}
