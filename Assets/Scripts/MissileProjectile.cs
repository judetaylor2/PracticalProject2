using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    public GameObject explosionObject;
    public float projectileSpeed;
    Rigidbody rb;
    Transform t;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;    
    }
    
    void Update()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyList != null)
        {
            Debug.Log("enemy list not null");
            
            //transform.LookAt(enemyList[0].transform);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(enemyList[0].transform.position), 0.1f);

            rb.AddForce(transform.forward * projectileSpeed);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(explosionObject, transform.position, Quaternion.Euler(0, 0, 0), null);
            Destroy(gameObject);
        }
    }
}
