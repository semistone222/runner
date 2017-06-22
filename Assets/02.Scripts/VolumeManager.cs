using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour {
    public AudioMixer audioMixer;

	// Update is called once per frame
	void Update ()
    {
        if(UIValue.bgmOn == true)
        {
            audioMixer.SetFloat("BGMVol", UIValue.volMAX);
        }
        else if(UIValue.bgmOn == false)
        {
            audioMixer.SetFloat("BGMVol", UIValue.volMIN);
        }

        if (UIValue.seOn == true)
        {
            audioMixer.SetFloat("SEVol", UIValue.volMAX);
        }
        else if (UIValue.seOn == false)
        {
            audioMixer.SetFloat("SEVol", UIValue.volMIN);
        }
    }
}
