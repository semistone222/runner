using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterButton : MonoBehaviour {
	public Image img;
	public UnityEngine.UI.Button btn;
	float coolTime = 30.0f;
	public bool disableOnStart = true;
	private bool BoosterOn = false;
	float leftTime = 30.0f;
	float BoosterTime = 0;
	float BoosteringTime ;
	public float BoosterSpeed;

	// Use this for initialization
	void Start () {
		if (img == null) {
			img = gameObject.GetComponent<Image> ();
		}
		if (btn == null) {
			btn = gameObject.GetComponent<UnityEngine.UI.Button> ();
		}
		if (disableOnStart){
		//	ResetBoostertime ();
		}
		btn.enabled = false;
		BoosterSpeed = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerOff> ().MOVESPD_ORIGIN;

		// 각 캐릭터 부스터 유지 시간
		BoosteringTime = 2 * System.Convert.ToSingle (CharacterManager.CharacterInfoList [ ((CharacterManager.SelectCharacterNumber) * 60) +  System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, CharacterManager.SelectCharacterNumber])-1].Booster);
	}

	// Update is called once per frame
	void Update () {	
		if (StageManager.ItemChecked [2] == true) { // Booster 구매시 
			leftTime = 0;
		}

		if(BoosterOn){
			BoosterTime += Time.deltaTime;
			if (BoosterTime > BoosteringTime) {
				BoosterOn = false;
				Debug.Log ("BoosteroFF");
				BoosterTime = 0;
				ResetBoosterSpeed ();
			}
		}

			if (leftTime >= 0) {
				leftTime -= Time.deltaTime;
				if (leftTime < 0) {
					leftTime = 0;
					if (btn)
						btn.enabled = true;
				}
			}
			float ratio = 1.0f - (leftTime / coolTime);

			if (img)
				img.fillAmount = ratio;
	}

	public bool CheckBoostertime(){
		if (leftTime > 0) {
			return false;
		} else {
			return true;

		}
		
	}
	public void ResetBoostertime(){  // 클릭시
		Debug.Log("Click Button");
		GameObject.FindGameObjectWithTag ("Player").GetComponent<TrailRenderer> ().enabled = true;
		StageManager.ItemChecked [2] = false;
		leftTime = 30 + BoosteringTime; 
		if (btn)
			btn.enabled = false;
		BoosterSpeed = BoosterSpeed + (BoosterSpeed * 0.2f);
		Debug.Log ("BoosterSpeed: "+BoosterSpeed);
		GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerControllerOff> ().MOVESPD_ORIGIN= BoosterSpeed;
        GetComponent<AudioSource>().Play();
        BoosterOn = true;
	}

	public void ResetBoosterSpeed(){
		BoosterSpeed = PlayerControllerOff.DeathBeforeSpeed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerControllerOff> ().MOVESPD_ORIGIN= BoosterSpeed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<TrailRenderer> ().enabled = false;
	}
}
