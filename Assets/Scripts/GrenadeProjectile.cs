using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public GameObject explosionObject;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            Instantiate(explosionObject, transform.position, Quaternion.Euler(0, 0, 0), null);
            Destroy(gameObject);
        }
    }
}
