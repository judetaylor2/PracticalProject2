using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitscan : WeaponController
{
    RaycastHit hit;
    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(shootPoint.position, hit.point);
    }

    public override void Shoot(float distance, Vector3 direction)
    {
        if (Physics.Raycast(shootPoint.position, direction, out hit, distance, enemyMask))
        {
            EnemyStats enemy;
            if (hit.transform.TryGetComponent<EnemyStats>(out enemy))
            {
                enemy.TakeDamage(weaponDamage);
            }
        }
    }
}
