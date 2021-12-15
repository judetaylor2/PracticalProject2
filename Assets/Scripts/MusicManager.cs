using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource currentSound;

    void Start()
    {
        currentSound = GetComponent<AudioSource>();
        currentSound.Play();
    }
    
    public void PlaySound(AudioSource a)
    {
        
        currentSound.Stop();
        
        a.Play();
        
        currentSound = a;
    }
}
