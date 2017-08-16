using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningVideo : MonoBehaviour {
    
	void Start ()
    {
		Handheld.PlayFullScreenMovie ("Bridge_Opening_sample.mp4", Color.black, FullScreenMovieControlMode.Hidden);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
