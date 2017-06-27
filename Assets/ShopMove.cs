using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMove : MonoBehaviour {
	public GameObject ShowMovePopup;
	public static bool StageToStore;

	public void ClickShopMoveCancelStage(){
		ShowMovePopup.SetActive (false);	
	}
	public void ClickShopMoveCancel(){
		GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
		ShowMovePopup.SetActive (false);
	}

	public void ClickShopMoveInStage(){
		for(int i = 0 ; i < 3 ; i++){
			if(StageManager.ItemChecked [i] ==true){
				PlayerInfoManager.Gold += 1000;
				StageManager.ItemChecked [i] = false;
			}
		}
		StageToStore = true;
		Application.LoadLevel ("SelectMode");
	}

	public void ClickShopMoveInUpgradeButton(){
		Debug.Log (CharacterManager.ShopMovePopupNumber);
		if (CharacterManager.ShopMovePopupNumber == 1) {
			GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
			GameObject.Find ("MainSceneManager").GetComponent<MainSceneManager> ().ClickShopButton ();
			ShowMovePopup.SetActive (false);
		} else if(CharacterManager.ShopMovePopupNumber == 2 ){
			GameObject.Find ("CharacterManager").GetComponent<CharacterManager> ().DoTouchButton ();
			GameObject.Find ("MainSceneManager").GetComponent<MainSceneManager> ().ClickShopButton ();
			ShowMovePopup.SetActive (false);
		}
	}

}
