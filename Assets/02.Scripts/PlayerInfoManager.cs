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
	void Start(){
		/*PlayerCharacterinfo[0,0] = "rabbit";
		PlayerCharacterinfo[0,1] = "turtle";
		PlayerCharacterinfo[0,2] = "cat";
		PlayerCharacterinfo[0,3] = "tiger";
		PlayerCharacterinfo[1,0] = "1";
		PlayerCharacterinfo[1,1] = "0";
		PlayerCharacterinfo[1,2] = "0";
		PlayerCharacterinfo[1,3] = "0";*/
//		Debug.Log ("PlayerCharacterinfo[1,0]"	+PlayerCharacterinfo[1,0]);

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
