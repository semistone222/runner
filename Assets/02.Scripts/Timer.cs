using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public float pastTime;
	public static float CompareTime;
	public int minute;
	public static string timesec = "";
	public Text timeTime;

	// Use this for initialization


	void Start(){
		pastTime = 0;
		minute = 0;
	}
	// Update is called once per frame
	void Update () {
		pastTime += Time.deltaTime;
		CompareTime += Time.deltaTime;

		if (pastTime >= 60) {
			minute++;
			pastTime = pastTime % 60;
		}
		timesec = string.Format ("{0:F2}", pastTime);	

		if (minute < 10) {

			timesec = "0" + minute + ":" + pastTime.ToString ("00.00");
		} else {
			timesec = minute + ":" + pastTime.ToString ("00.00");
		}
		timesec = timesec.Replace (".", ":");
		timeTime.text = timesec;
	
	}
}
