using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveBGM : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public static void SetBGM(int k)
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().clip =
        Resources.Load<AudioClip>("BGM/BGM" + k.ToString());

        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
    }
	
}
