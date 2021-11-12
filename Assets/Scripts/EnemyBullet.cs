using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletType;
    Transform player;
    public float bulletSpeed, timeBeforeDestroy = 5;
    float destroyStopWatch;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(player);
    }

    // Update is called once per frame

    void Update()
    {
        if (bulletType == 1)
        {
            transform.position += transform.forward * bulletSpeed;

            destroyStopWatch += Time.deltaTime;

            if (destroyStopWatch >= timeBeforeDestroy)
            {
                Destroy(gameObject);
            }
        }
        else if (bulletType == 2)
        {
            transform.position += transform.forward * bulletSpeed;

            destroyStopWatch += Time.deltaTime;

            if (destroyStopWatch < timeBeforeDestroy / 2)
            {
                transform.LookAt(player);
            }
            else if (destroyStopWatch >= timeBeforeDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
