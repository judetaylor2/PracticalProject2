using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform t;
    
    void Update()
    {
        transform.position = t.position;
        transform.rotation = t.rotation;
        Debug.Log(t.position);
    }
}
