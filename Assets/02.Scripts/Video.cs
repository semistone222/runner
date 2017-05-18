using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Video : MonoBehaviour {

//	public MovieTexture movie;
//	private AudioSource audio;
	void Start () {
		/*	GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();*/

		Handheld.PlayFullScreenMovie ("Bridge_Opening_sample.mp4", Color.black, FullScreenMovieControlMode.Hidden);
		Application.LoadLevel ("SelectMode");
	}
}
