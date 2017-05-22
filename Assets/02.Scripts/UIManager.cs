using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject BGMToggle;
    public GameObject SEToggle;

    public GameObject UISetting;
    private bool isUISettingOn = false;

    public GameObject UIJoystick;
    public GameObject PadToggle;

    private GameObject CameraObject;
    public Image CameraToggleLevelSprite;
    public Sprite[] CameraToggleSprites;
    public float[] CameraValues;
    private int spriteIndex = 0;

    private AudioSource clickSound;

    private void Awake()
    {
        clickSound = GetComponent<AudioSource>();
        clickSound.playOnAwake = false;

        //spriteIndex = 현재 카메라 감도 값에 비례해서 대입한 후
        CameraObject = Camera.main.gameObject;
        CameraObject.GetComponent<CameraController>().rotationSpeed = CameraValues[spriteIndex % CameraValues.Length];
    }

    private void Start()
    {
        isUISettingOn = false;
        UISetting.SetActive(false);

        //현재 SnapsToFinger 값에 따라 Toggle 변경
        PadToggle.GetComponent<Toggle>().isOn = UIJoystick.GetComponent<SimpleJoystick>().SnapsToFinger;

    }

    private void InitiateSound()
    {
        /*나중에 PlayerPrefs 등으로 수정해야 함*/
        BGMToggle.GetComponent<Toggle>().isOn = true;
        SEToggle.GetComponent<Toggle>().isOn = true;

        audioMixer.SetFloat("BGMVol", 0f);
        audioMixer.SetFloat("SEVol", 0f);
    }

	private void ClickSound()
    {
        if (clickSound.isPlaying == false)
        {
            clickSound.Play();
        }
    }

    public void UISettingToggle()
    {
       // ClickSound();
        isUISettingOn = !isUISettingOn;
        UISetting.SetActive(isUISettingOn);
    }

    public void UIPadToggle( )
    {
        ClickSound();
        UIJoystick.GetComponent<SimpleJoystick>().SnapsToFinger = !PadToggle.GetComponent<Toggle>().isOn;
    }

    public void UICamToggle()
    {
        int length;
        length = CameraToggleSprites.Length;

        if(length == CameraValues.Length)
        {
            ClickSound();
            spriteIndex++;

            spriteIndex = spriteIndex % length;

            CameraToggleLevelSprite.sprite = CameraToggleSprites[spriteIndex];
            CameraObject.GetComponent<CameraController>().rotationSpeed = CameraValues[spriteIndex];
        }
    }

    public void UISoundToggle(string s)
    {
        float vol;
        audioMixer.GetFloat(s, out vol);

        Debug.Log(vol +" "+ s);

        if (vol > -80f)
        {
            audioMixer.SetFloat(s, -80f);
        }
        else
        {
            audioMixer.SetFloat(s, 0f);
        }


        ClickSound();
    }
}
