using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class PlayerInfoManager : MonoBehaviour {
	const int StageMaxCount = 10;

	public static int RunPoint = 5;
	public static int Diamond = 3500;
	public static int Gold = 20000;
	public static string SelectCharacter = "rabbit";
	public static string[,] PlayerCharacterinfo = new string[2, 4]  {{"rabbit", "turtle", "cat", "tiger"}, {"1", "2", "0", "0"}};


	public Text[] PlayerInfoText = new Text[3];


	enum PlayerInfo:int{
		Life, Diamond, Gold
	}
	enum StageInfo:int{
		Stage1,Stage2,Stage3,Stage4,Stage5,Stage6,Stage7,Stage8,Stage9,Stage10
	}

	// Use this for initialization
	void FixedUpdate () {
		if (RunPoint == 0) {
			PlayerInfoText [(int)PlayerInfo.Life].text = LifeTimer.TimeText;
		} else {
			PlayerInfoText [(int)PlayerInfo.Life].text = RunPoint.ToString () + "/5";
		}
		PlayerInfoText [(int)PlayerInfo.Diamond].text = Diamond.ToString ();
		PlayerInfoText [(int)PlayerInfo.Gold].text = Gold.ToString ();
	}
}
