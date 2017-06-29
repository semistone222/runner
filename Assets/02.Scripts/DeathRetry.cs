using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathRetry : MonoBehaviour {
	public Text PlayerDiamondText;
	public GameObject Life1;
	public GameObject Life2;
	public GameObject ShopMovePopup;

	public static bool DeathRetryPopupAgain = false;

	private GameObject SoundManager;

	void Start(){
		SoundManager = GameObject.Find ("SoundManager");
		PlayerDiamondText.text = "현재 보석 수 : "+PlayerInfoManager.Diamond;
		DeathRetryPopupAgain = true;
	}

	public void CancelDeathRetry(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		ResultManager.InitItems();
		Time.timeScale = 1;
		Application.LoadLevel ("SelectMode");
	} 
	public void RetryButton(){

		if (PlayerInfoManager.Diamond >= 10) {
			SoundManager.GetComponent<SoundManager> ().PlayUseGemSE ();
			PlayerInfoManager.Diamond -= 10;
			GimmickDeath.LifeCount = 2;
			Life1.SetActive (true);
			Life2.SetActive (true);
			Time.timeScale = 1;
			GameObject.Find ("DeathRetry").SetActive (false);
		} else {
			GameObject.Find ("DeathPopup").SetActive (false);
			ShopMovePopup.SetActive (true);
		}
	}
	
}
