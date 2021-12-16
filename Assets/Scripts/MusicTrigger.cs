using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource music;
    MusicManager musicManager;
    bool hasCollided;
    
    // Start is called before the first frame update
    void Start()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasCollided)
        {
            musicManager.PlaySound(music);
            hasCollided = true;

        }
    }
}
