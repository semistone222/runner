using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class SoundControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        bgmSlider.value = PlayerPrefs.GetInt("BGM", 0);
        sfxSlider.value = PlayerPrefs.GetInt("SFX", 0);
    }
    
    public void SetBGMLevel(float level)
    {
        audioMixer.SetFloat("BGMVol", level);
        PlayerPrefs.SetInt("BGM", (int)level);

    }

    public void SetSFXLevel(float level)
    {
        audioMixer.SetFloat("SFXVol", level);
        PlayerPrefs.SetInt("SFX", (int)level);
    }
}
