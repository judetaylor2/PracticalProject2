using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int attackType;

    public void Shoot()
    {
        transform.parent.GetComponent<Enemy2>().Attack(attackType);
    }
}
