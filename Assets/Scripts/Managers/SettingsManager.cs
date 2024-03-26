using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private Sprite settingOnSprite;

    [SerializeField]
    private Sprite settingOffSprite;

    [SerializeField]
    private Image soundButtonImage;

    [Header("Settings")]
    private bool soundState = true;


    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("sound", 1) == 1;
    }

    private void Start()
    {
        Setup();
    }

    public void ChangeSoundState()
    {
        if(soundState)
        {
            DisableSound();
        }
        else
        {
            EnableSound();
        }

        soundState = !soundState;

        PlayerPrefs.SetInt("sound", soundState ? 1 : 0);
    }
    
    private void Setup()
    {
        if (soundState)
        {
            EnableSound();
        }
        else
        {
            DisableSound();
        }
    }

    private void DisableSound()
    {
        soundManager.DisableSound();

        soundButtonImage.sprite = settingOffSprite;
    }

    private void EnableSound()
    {
        soundManager.EnableSound();

        soundButtonImage.sprite = settingOnSprite;
    }
}
