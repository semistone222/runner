using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTimer : MonoBehaviour {
	public static float CountLifeTime= 300;
	private float LifeTimesec;
	private float	LifeTimemin;
	public static string TimeText;
	private int sec;
	// Update is called once per frame

	void Update () {
		LifeTimemin = CountLifeTime / 60;
		LifeTimesec = CountLifeTime % 60;
		CountLifeTime -= Time.deltaTime;	
		sec = (int)LifeTimesec;
		TimeText = sec.ToString ().PadLeft (2,'0');
		TimeText = (int)LifeTimemin + ":" + TimeText;
		if (CountLifeTime <= 0) {
			CountLifeTime = 300;
			PlayerInfoManager.RunPoint++;
		}
	}
}
