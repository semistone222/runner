using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class PlayerInfoManager : MonoBehaviour {
	const int StageMaxCount = 10;

	public static int RunPoint = 5;
	public static int Diamond = 330;
	public static int Gold = 2000;
	public static string SelectCharacter = "rabbit";
	public static string[,] PlayerCharacterinfo = new string[2, 4]  {{"rabbit", "turtle", "cat", "tiger"}, {"60", "2", "0", "0"}};

	public Text[] PlayerInfoText = new Text[3];


	enum PlayerInfo:int{
		Life, Diamond, Gold
	}
		
	// Use this for initialization
	void FixedUpdate () {
		if (Gold >= 999999) {
			Gold = 999999;
		}
		if (RunPoint == 0) {
			PlayerInfoText [(int)PlayerInfo.Life].text = LifeTimer.TimeText;
		} else {
			PlayerInfoText [(int)PlayerInfo.Life].text = RunPoint.ToString () + "/5";
		}
		PlayerInfoText [(int)PlayerInfo.Diamond].text = Diamond.ToString ();
		PlayerInfoText [(int)PlayerInfo.Gold].text = Gold.ToString ();
	}
}
