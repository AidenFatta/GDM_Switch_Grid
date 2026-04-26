using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsController : MonoBehaviour
{

    public Slider volumeSlider;
    public Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.ChangeSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
