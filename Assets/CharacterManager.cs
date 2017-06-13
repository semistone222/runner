using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.UI;

public class CharacterInfo{
	public int Level;
	public string Name;
	public double MaxSpeed;
	public double Jump;
	public double Booster;
	public double Shiled;
	public int UpgradeCost;
}

public class CharacterManager : MonoBehaviour {
	const int CharacterMax_Count = 4;

	public Text[] CharacterNameText = new Text[CharacterMax_Count];
	public Text[] UpgradeLevelText = new Text[CharacterMax_Count];
	public GameObject[] SelectButton = new GameObject[CharacterMax_Count];

	public Sprite RunningImage;
	public Sprite SelectImage;
	public Sprite BuyImage;

	public int ClickButtonNunmber;
	public int CharacterBuyPrice;


	public GameObject Scrollrect;
	public GameObject BuyPopup;
	public Text BuyPriceText;
	public Text BuyCharacterText;

	public List<CharacterInfo> itemList = new List<CharacterInfo>(); 


	// Use this for initialization
	void Start () {
		itemList = Read("CharacterData"); 
		for (int i = 0; i < CharacterMax_Count; i++) {   	// 이름 표기 
			if(itemList [i * 60].Name == "rabbit"){
				CharacterNameText [i].text = "토순이";
			}else if(itemList [i * 60].Name == "turtle"){
				CharacterNameText [i].text = "거북이";
			}else if(itemList [i * 60].Name == "cat"){
				CharacterNameText [i].text = "냥냥이";
			}else{
				CharacterNameText [i].text = "호돌이";
			}
		}
	}

	void FixedUpdate(){
		for (int i = 0; i < CharacterMax_Count; i++) {
			UpgradeLevelText[i].text = "LV." +PlayerInfoManager.PlayerCharacterinfo [1, i];
			if ( itemList [i * 60].Name == PlayerInfoManager.SelectCharacter) {// 현재 선택한 캐릭터 표기 
				SelectButton [i].GetComponent<Image>().sprite = RunningImage;	
			}

			if (System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, i]) == 0) { // 선택하지 않은 캐릭터 상태 구매하지 않은 상태 표기  
			//	Debug.Log ("Convert.ToInt32 "+PlayerInfoManager.PlayerCharacterinfo [1, 2]);
				SelectButton [i].GetComponent<Image>().sprite = BuyImage;	
				UpgradeLevelText [i].text = " LV.1"; //구매하지 않을 경우 lv1로 표기   
			}
			else if (PlayerInfoManager.SelectCharacter != PlayerInfoManager.PlayerCharacterinfo[0,i]){ // 현재 선택 가능한 캐릭터 표기
				SelectButton [i].GetComponent<Image>().sprite = SelectImage;	
			}


			if (SelectButton [i].GetComponent<Image> ().sprite == RunningImage) {
				GameObject.Find (itemList [60 * i].Name + "Ani").GetComponent<ShopCharacter> ().Shopani.SetBool ("IsSelect", true);
			} else {
				GameObject.Find (itemList [60 * i].Name + "Ani").GetComponent<ShopCharacter> ().Shopani.SetBool ("IsSelect", false);
			}
		}	
	}
	
	public static List<CharacterInfo> Read(string filepath)
	{
		TextAsset textxml = (TextAsset)Resources.Load (filepath, typeof(TextAsset));
		XmlDocument Document = new XmlDocument ();
		Document.LoadXml (textxml.text);

		XmlElement ItemListElement = Document ["Character"];

		List<CharacterInfo> ItemList = new List<CharacterInfo> ();

		foreach (XmlElement ItemElement in ItemListElement.ChildNodes) {
			CharacterInfo Item = new CharacterInfo ();

			Item.Level = System.Convert.ToInt32 (ItemElement.GetAttribute ("Level"));
			Item.Name =ItemElement.GetAttribute ("Name");
			Item.MaxSpeed = System.Convert.ToDouble (ItemElement.GetAttribute ("MaxSpeed"));
			Item.Jump = System.Convert.ToDouble (ItemElement.GetAttribute ("Jump"));
			Item.Booster = System.Convert.ToDouble (ItemElement.GetAttribute ("Booster"));
			Item.Shiled = System.Convert.ToDouble (ItemElement.GetAttribute ("Shiled"));
			Item.UpgradeCost = System.Convert.ToInt32 (ItemElement.GetAttribute ("UpgradeCost"));
			ItemList.Add (Item);

		}
		return ItemList;
	}


	public void ClickSelectButton1(){
		ClickButtonNunmber = 0;
		ClickChangeButton (ClickButtonNunmber);
	}

	public void ClickSelectButton2(){
		ClickButtonNunmber = 1;
		ClickChangeButton (ClickButtonNunmber);
	}
	public void ClickSelectButton3(){
		ClickButtonNunmber = 2;
		ClickChangeButton (ClickButtonNunmber);
	}
	public void ClickSelectButton4(){
		ClickButtonNunmber = 3;
		ClickChangeButton (ClickButtonNunmber);
	}

	public void ClickBuyButton(){
		ClickBuyfuntion (ClickButtonNunmber);
	}

	public void ClickBuyfuntion(int ClickButtonNunmber){
		if (CharacterBuyPrice < PlayerInfoManager.Diamond) { // 캐릭터  구매시
			PlayerInfoManager.Diamond -= CharacterBuyPrice; 
			PlayerInfoManager.PlayerCharacterinfo [1, ClickButtonNunmber] = "1";
		} else {
			//못 산다. 
		
		}
		ClickBuyCancelButton ();
	}
		
	public void ClickBuyCancelButton(){
		for(int i = 0; i < CharacterMax_Count; i++){
			SelectButton [i].GetComponent<Button>().interactable = true; //구매 버튼 누를시 나머지 버튼 활성화 
		}
		Scrollrect.GetComponent<ScrollRect> ().horizontal = true; // 구매 버튼 누를시 캐릭터 ScrollRect 고정해제 
		BuyPopup.SetActive (false);
	}


	public void ClickChangeButton(int index){

		if (SelectButton [index].GetComponent<Image>().sprite == RunningImage) {
			
		} else if (SelectButton [index].GetComponent<Image>().sprite == SelectImage) {  // 버튼이  선택버튼일 경우 
			PlayerInfoManager.SelectCharacter = itemList [60 * index].Name;
		} else {  // 버튼이 구매버튼일 경우 
			BuyPopup.SetActive (true);
			BuyCharacterText.text = CharacterNameText [index].text;
			for(int i = 0; i < CharacterMax_Count; i++){
				SelectButton [i].GetComponent<Button>().interactable = false; //구매 버튼 누를시 나머지 버튼 비활성화 
			}
				Scrollrect.GetComponent<ScrollRect> ().horizontal = false; // 구매 버튼 누를시 캐릭터 ScrollRect 고정 

				if(index == 1){  // 캐릭터 가격 표시
					CharacterBuyPrice = 100;
					BuyPriceText.text = CharacterBuyPrice.ToString ();
				}else if(index == 2){
					CharacterBuyPrice = 200;
					BuyPriceText.text = CharacterBuyPrice.ToString ();
				}else if(index == 3){
					CharacterBuyPrice = 300;
					BuyPriceText.text = CharacterBuyPrice.ToString ();
				}
			}
	}


	public void ClickUpgradeButton1(){
		ClickChangeButton (0);
	}

	public void ClickUpgradeButton2(){
		ClickChangeButton (1);
	}
	public void ClickUpgradeButton3(){
		ClickChangeButton (2);
	}
	public void ClickUpgradeButton4(){
		ClickChangeButton (3);
	}

	public void ClickUpgradeButton(int index){



	}


}
