using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	const int ShopProduct_MaxCount = 10;

	public GameObject ShopBuyPopup;
	public GameObject NeedMoreDiamondPopup;
	public Button[] BuyButton = new Button[ShopProduct_MaxCount];
	public Text BuyDetailText;
	public Text NeddDiamondText;
	public int SelectButtonNumber;
    
    public AudioSource SceneManagerSE;

    public void ClickBuyProductButton(int index){
		GameObject.Find ("ShopPanel").GetComponent<ScrollRect> ().horizontal = false;
		NotActiveButton ();
		SelectButtonNumber = index;

        SceneManagerSE.Play();

		if (index == 5) {
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "런 포인트 5개를 구매 하시겠습니까?";
			NeddDiamondText.text = "50";
		}else if(index == 6){
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "런 포인트 10개를 구매 하시겠습니까?";
			NeddDiamondText.text = "100";
		}else if(index == 7){
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "런 포인트 20개를 구매 하시겠습니까?";
			NeddDiamondText.text = "200";
		}else if(index == 8){
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "골드 주머니를 구매 하시겠습니까?";
			NeddDiamondText.text = "60";
		}else if(index == 9){
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "골드 바구니를 구매 하시겠습니까?";
			NeddDiamondText.text = "500";
		}else if(index == 10){
			ShopBuyPopup.SetActive (true);
			BuyDetailText.text = "골드 수레를 구매 하시겠습니까?";
			NeddDiamondText.text = "4500";
		}
	}

	public void BuyProduct(){

        int prevGold, nextGold;
        prevGold = PlayerInfoManager.Gold;

		int index = SelectButtonNumber;
		if (index == 5) {
			if (PlayerInfoManager.Diamond >= 50) {
				PlayerInfoManager.Diamond -= 50;
				PlayerInfoManager.RunPoint += 5; 
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}else if(index == 6){
			if (PlayerInfoManager.Diamond >= 100) {
				PlayerInfoManager.Diamond -= 100;
				PlayerInfoManager.RunPoint += 10;
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}else if(index == 7){
			if (PlayerInfoManager.Diamond >= 200) {
				PlayerInfoManager.Diamond -= 200;
				PlayerInfoManager.RunPoint += 20; 
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}else if(index == 8){
			if (PlayerInfoManager.Diamond >= 60) {
				PlayerInfoManager.Diamond -= 60;
				PlayerInfoManager.Gold += 1000; 
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}else if(index == 9){
			if (PlayerInfoManager.Diamond >= 500) {
				PlayerInfoManager.Diamond -= 500;
				PlayerInfoManager.Gold += 10000; 
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}else if(index == 10){
			if (PlayerInfoManager.Diamond >= 4500) {
				PlayerInfoManager.Diamond -= 500;
				PlayerInfoManager.Gold += 100000; 
				ClickBuyProductCancelButton ();
			} else {
				ShopBuyPopup.SetActive (false);
				NeedMoreDiamondPopup.SetActive (true);
			}
		}

        nextGold = PlayerInfoManager.Gold;

        if(nextGold > prevGold)
        {
            GetComponent<AudioSource>().Play(); //play BuyGold.mp3
        }
	}

	public void ClickBuyProductCancelButton(){
		ShopBuyPopup.SetActive (false);
		ActiveButton ();
		GameObject.Find ("ShopPanel").GetComponent<ScrollRect> ().horizontal = true;
	}

	public void ClickNeedMoreDiamondPopupCancelButton(){
		NeedMoreDiamondPopup.SetActive (false);
		ActiveButton ();
		GameObject.Find ("ShopPanel").GetComponent<ScrollRect> ().horizontal = true;
	}

	public void NotActiveButton(){
		for (int i = 0; i < ShopProduct_MaxCount; i++) {
			BuyButton [i].enabled = false;
		}
	}

	public void ActiveButton(){
		for (int i = 0; i < ShopProduct_MaxCount; i++) {
			BuyButton [i].enabled = true;
		}
	}
}
