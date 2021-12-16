using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class OptionsMenu : MonoBehaviour
{
    //public Slider MusicSlider, soundEffectSlider;
    public AudioMixer mainMixer;
    public AudioSource volumeTest1, volumeTest2;
    public PostProcessVolume postProcessVolume;
    public CameraController cameraController; 
    MotionBlur motionBlur;

    public void ChangeMusicVolume(float amount)
    {
        if (amount < -20)
        mainMixer.SetFloat("Music", -80);
        else
        mainMixer.SetFloat("Music", amount);
        volumeTest1.Play();
    }

    public void ChangeEffectVolume(float amount)
    {
        if (amount < -20)
        mainMixer.SetFloat("Effects", -80);
        else
        mainMixer.SetFloat("Effects", amount);
        volumeTest2.Play();
    }

    public void ChangeQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
    }

    public void ChangeMotionBlur(int i)
    {
        motionBlur = postProcessVolume.profile.GetSetting<MotionBlur>();
        
        if (i == 0)
        motionBlur.active = false;
        else if (i == 1)
        motionBlur.active = true;
    }

    public void ChangeSensitivity(float amount)
    {
        cameraController.mouseSensitivity = amount;
    }
}
