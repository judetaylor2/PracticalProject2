using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSound : MonoBehaviour
{
    AudioSource[] audioSources;
    public GameObject audioSourcesObject;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSources = audioSourcesObject.GetComponentsInChildren<AudioSource>();
    }

    public void PlaySound(int index)
    {
        audioSources[index].Play();
        Debug.Log("playing audio");
    }
    
}
