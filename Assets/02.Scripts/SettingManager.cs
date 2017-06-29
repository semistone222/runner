using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    
    public GameObject UISetting;
    private bool isUISettingOn = false;

    private GameObject CameraObject;
    public Image CameraToggleLevelSprite;
    public Sprite[] CameraToggleSprites;
    private float[] CameraValues;
    private int spriteIndex = 0;

    public GameObject BGMToggle;
    public GameObject SEToggle;
    public GameObject PadToggle;
    public GameObject BGMCheckMark;
    public GameObject SECheckMark;
    public GameObject PadCheckMark;

    private AudioSource clickSound;

    private void Awake()
    {
        clickSound = GetComponent<AudioSource>();
        clickSound.playOnAwake = false;

        CameraValues = new float[3];
        CameraValues[0] = UIValue.camValueLow;
        CameraValues[1] = UIValue.camValueMid;
        CameraValues[2] = UIValue.camValueHigh;
    }

    private void Start()
    {
        isUISettingOn = false;
        UISetting.SetActive(false);
    }

    private void ClickSound()
    {
        if (clickSound.isPlaying == false)
        {
            clickSound.Play();
        }
    }

    private void ClickSound2()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayClickSound3();
    }

    public void UISettingToggle()
    {
        if (isUISettingOn)
        {
            ClickSound();
        }
        else
        {
            ClickSound2();
        }
        isUISettingOn = !isUISettingOn;
        UISetting.SetActive(isUISettingOn);
        SetSprites();
    }

    void SetSprites()
    {
        //camToggle
        if (UIValue.camValue == UIValue.camValueLow)
        {
            CameraToggleLevelSprite.sprite = CameraToggleSprites[0];
            spriteIndex = 0;
        }
        else if (UIValue.camValue == UIValue.camValueMid)
        {
            CameraToggleLevelSprite.sprite = CameraToggleSprites[1];
            spriteIndex = 1;
        }
        else if (UIValue.camValue == UIValue.camValueHigh)
        {
            CameraToggleLevelSprite.sprite = CameraToggleSprites[2];
            spriteIndex = 2;
        }

        //bgm toggle
        BGMToggle.GetComponent<Toggle>().isOn = UIValue.bgmOn;
        //se toggle
        SEToggle.GetComponent<Toggle>().isOn = UIValue.seOn;
        //pad toggle
        PadToggle.GetComponent<Toggle>().isOn = UIValue.snapsToFinger;
    }

    public void UICamToggle()
    {
        int length;
        length = CameraToggleSprites.Length;

        if (length == CameraValues.Length)
        {
            ClickSound();
            spriteIndex++;

            spriteIndex = spriteIndex % length;

            CameraToggleLevelSprite.sprite = CameraToggleSprites[spriteIndex];
            UIValue.camValue = CameraValues[spriteIndex];
        }
    }

    public void UIBGMToggle()
    {
        ClickSound();
        UIValue.bgmOn = BGMToggle.GetComponent<Toggle>().isOn;
        Debug.Log(UIValue.bgmOn);
    }

    public void UISEToggle()
    {
        ClickSound();
        UIValue.seOn = SEToggle.GetComponent<Toggle>().isOn;
    }
    public void UIPadToggle()
    {
        ClickSound();
        UIValue.snapsToFinger = PadToggle.GetComponent<Toggle>().isOn;
    }

}