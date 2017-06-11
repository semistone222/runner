using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour {
	const int StageMaxCount = 10;

	public static int Life = 70;
	public static int Diamond = 3500;
	public static int Gold = 20000;

	public Text[] PlayerInfoText = new Text[3];


	enum PlayerInfo:int{
		Life, Diamond, Gold
	}
	enum StageInfo:int{
		Stage1,Stage2,Stage3,Stage4,Stage5,Stage6,Stage7,Stage8,Stage9,Stage10
	}


	// Use this for initialization
	void FixedUpdate () {
		PlayerInfoText [(int)PlayerInfo.Life].text = Life.ToString()+"/5";
		PlayerInfoText [(int)PlayerInfo.Diamond].text = Diamond.ToString ();
		PlayerInfoText [(int)PlayerInfo.Gold].text = Gold.ToString ();
	}
}
