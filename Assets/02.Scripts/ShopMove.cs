﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMove : MonoBehaviour {
	public GameObject ShowMovePopup;
	public static bool StageToStore = false;
	public static bool DeathRetryToStore = false;

	private GameObject SoundManager;

	private void Awake()
	{
		SoundManager = GameObject.Find ("SoundManager");
	}

	public void ClickShopMoveCancelStage(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		SendSence.RunPointShop = false;
		ShowMovePopup.SetActive (false);	
	}
	public void ClickShopMoveCancel(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
		ShowMovePopup.SetActive (false);
	}

	public void ClickShopMoveInStage(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound2 ();
		for(int i = 0 ; i < 3 ; i++){
			if(StageManager.ItemChecked [i] ==true){
				PlayerInfoManager.Gold += 1000;
				StageManager.ItemChecked [i] = false;
			}
			StageToStore = true;  // 씬을 넘겨야 되서 static 변수로 구분 
			Application.LoadLevel ("SelectMode");}
	}

	public void ClickShopMoveInUpgradeButton(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound2 ();
		if (CharacterManager.ShopMovePopupNumber == 1) { // 캐릭터 창에서 보석이 부족할 경우 
			GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
			GameObject.Find ("MainSceneManager").GetComponent<MainSceneManager> ().ClickShopButton ();
			ShowMovePopup.SetActive (false);
		} else if(CharacterManager.ShopMovePopupNumber == 2 ){ // 캐릭터 창에서 골드가 부족할 경우 
			GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
			GameObject.Find ("MainSceneManager").GetComponent<MainSceneManager> ().ClickShopButton ();
			ShowMovePopup.SetActive (false);
			GameObject.Find ("ShopPopup").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-510, 0);
		}
	}
	public void ClickShopMoveInDeathRetry(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound2 ();
		ResultManager.InitItems();
		Time.timeScale = 1;
		DeathRetryToStore = true; // 씬을 넘겨야 되서 static 변수로 구분 
		Application.LoadLevel ("SelectMode");
	}

}
