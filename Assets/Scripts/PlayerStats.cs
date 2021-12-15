using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject camNode;

    float damageAmount, damageStopWatch;

    //sound
    [Header("Sounds")] public AudioSource meleeDamageSound; 
    public AudioSource bulletDamageSound;

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

        if (previousVelocity >= fallDamageDistance && player.isGrounded)
        {
            int damage = (int)(previousVelocity * fallDamageMultiplier);
            TakeDamage(damage);
            
            StartCoroutine("DamageUI");
            
            previousVelocity = 0;
        }

        if (health <= 0)
        {
            Debug.Log("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!player.isGrounded)
        {
            previousVelocity = -rb.velocity.y;
            //Debug.Log(previousVelocity.y);
            
        }
    }

    void Update()
    {
        damageStopWatch += Time.deltaTime;
        if (damageStopWatch >= 0.8f)
        {
            damageAmount = 0;
            damageStopWatch = 0;
        }

        healthText.text = health.ToString();
        
        healthBar.value = health;

        healthImage.color = healthText.color = healthBar.fillRect.GetComponent<Image>().color = healthColors.Evaluate(healthBar.value / 100);

        Debug.Log(damageAmount);
        
        //damageImage.color = Color.Lerp(Color.red, Color.blue, 0.5f);
    }

    public void TakeDamage(int damage)
    {
        damageStopWatch = 0;
        damageAmount += damage;

        health -= damage;
        Debug.Log($"Damage: {(int)(previousVelocity * fallDamageMultiplier)} | health {health}");
        
        if (damageAmount >= 30)
        {
            StopCoroutine("DamageUI");
            //damageImage.color = new Color(1, 0, 0, 0);
            camNode.transform.localRotation = Quaternion.Euler(camNode.transform.localRotation.x, camNode.transform.localRotation.y, 0);
            StartCoroutine("DamageUI");
            
            
        
        
            bulletDamageSound.Play();    
        
        }
        else
        {
            meleeDamageSound.Play();
            
        }
    }
    
    IEnumerator DamageUI()
    {
        Quaternion q = Quaternion.Euler(camNode.transform.localRotation.x, camNode.transform.localRotation.y, Mathf.Clamp((damageAmount / 3) * (Random.Range(0f, 1f) >= 0.5f? 1:-1), -15, 15));

        float damageOpacity = damageAmount / 50;
        
        for (float i = 0; i < 1; i+= 0.02f)
        {
            damageImage.color = Color.Lerp(damageImage.color, new Color(1, 0, 0, Mathf.Clamp(damageOpacity, 0, 0.5f)), i);
            camNode.transform.localRotation = Quaternion.Lerp(camNode.transform.localRotation, q, i);
            yield return new WaitForSeconds(damageUITime);

        }

        yield return new WaitForSeconds(0.5f);

        //damageAmount = 0;
        
        for (float i = 0; i < 1; i+= 0.01f)
        {
            damageImage.color = Color.Lerp(damageImage.color, new Color(0, 0, 0, 0), i);
            camNode.transform.localRotation = Quaternion.Lerp(camNode.transform.localRotation, Quaternion.Euler(camNode.transform.localRotation.x, camNode.transform.localRotation.y, 0), i);
            yield return new WaitForSeconds(damageUITime * 5000000);

        }


    }

}
