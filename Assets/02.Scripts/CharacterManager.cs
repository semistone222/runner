using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.UI;

public class CharacterInfo{
	public int Level;
	public string Name;
	public float MaxSpeed;
	public float Jump;
	public float Booster;
	public float Shiled;
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
	public GameObject UpgradePopup;

	public Text BuyPriceText;
	public Text BuyCharacterText;

    public AudioSource SceneManagerSE;
    public AudioSource UseGemSE;

	private Text PriceText;


	private GameObject CharacterPosition;
	private GameObject Character;
	private GameObject CharacterComponent; 

	public Text UpgradeChacaterNameText;
	public Text TopLevelText;
	public Text TopLevelDetail;
	public Text BottomLevelText;
	public Text BottomLevelDetail;
	public Text NeedUpgradeGold;
	private int UpgradeNumber ;



	public GameObject ShopMovePopup;
	public static int ShopMovePopupNumber;

	public static int SelectCharacterNumber = 0;
	public static List<CharacterInfo> CharacterInfoList = new List<CharacterInfo>(); 


	// Use this for initialization
	void Start () {
		CharacterInfoList = Read("CharacterData"); 
		for (int i = 0; i < CharacterMax_Count; i++) {   	// 이름 표기 
			if(CharacterInfoList [i * 60].Name == "rabbit"){
				CharacterNameText [i].text = "토순이";
			}else if(CharacterInfoList [i * 60].Name == "turtle"){
				CharacterNameText [i].text = "거북이";
			}else if(CharacterInfoList [i * 60].Name == "cat"){
				CharacterNameText [i].text = "냥냥이";
			}else if(CharacterInfoList [i * 60].Name == "tiger"){
				CharacterNameText [i].text = "호돌이";
			}
		}
		for (int i = 0; i < CharacterMax_Count; i++) { // 캐릭터선택 각 위치에 캐릭터 프리팹 생성
			Character = Resources.Load ("Character/" + CharacterInfoList [i * 60].Name + "Ani") as GameObject;
			CharacterPosition = GameObject.Find ("Character"+(i+1));
			Instantiate (Character, CharacterPosition.transform.position, CharacterPosition.transform.rotation).transform.SetParent (CharacterPosition.transform);
			CharacterComponent = GameObject.Find (CharacterInfoList [i * 60].Name + "Ani(Clone)");
			CharacterComponent.transform.localScale = new Vector3 (150, 150, 150);
			CharacterComponent.transform.localPosition = new Vector3 (0f, -49.7f, -45.4f);
			CharacterComponent.transform.localRotation = Quaternion.Euler (0, -180, 0);
		}
	}

	void FixedUpdate(){
		for (int i = 0; i < CharacterMax_Count; i++) {
			UpgradeLevelText[i].text = "LV." +PlayerInfoManager.PlayerCharacterinfo [1, i];
			if (CharacterInfoList [i * 60].Name == PlayerInfoManager.SelectCharacter) {// 현재 선택한 캐릭터 표기 
				SelectButton [i].GetComponent<Image>().sprite = RunningImage;	

				if(i != 0){  // 살 캐릭터가 아닌 곳은 가격 텍스트 공백
					GameObject.Find ("Price" + i).GetComponent<Text> ().text = "";
				}
			}
			if (System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, i]) == 0) { // 선택하지 않은 캐릭터 상태 구매하지 않은 상태 표기  
				SelectButton [i].GetComponent<Image>().sprite = BuyImage;	
				 GameObject.Find("UpgradeButton"+(i+1)).GetComponent<Button>().interactable = false ;
				UpgradeLevelText [i].text = " LV.1"; //구매하지 않을 경우 lv1로 표기   
				if (i == 1) { // 살 수 있는 캐릭터는 가격 표시 (후에 데이터 테이블로 수정)
					GameObject.Find ("Price" + i).GetComponent<Text> ().text = "100";
				} else if (i == 2) {
					GameObject.Find ("Price" + i).GetComponent<Text> ().text = "200";
				} else if (i == 3) {
					GameObject.Find ("Price" + i).GetComponent<Text> ().text = "300";
				}
			}
			else if (PlayerInfoManager.SelectCharacter != PlayerInfoManager.PlayerCharacterinfo[0,i]){ // 현재 선택 가능한 캐릭터 표기
				SelectButton [i].GetComponent<Image>().sprite = SelectImage;	
				if(i != 0){ // 살 캐릭터가 아닌 곳은 가격 텍스트 공백
					GameObject.Find ("Price" + i).GetComponent<Text> ().text = "";
				}
			}


			if (SelectButton [i].GetComponent<Image> ().sprite == RunningImage) {
				GameObject.Find (CharacterInfoList [60 * i].Name + "Ani(Clone)").GetComponent<ShopCharacter> ().Shopani.SetBool ("IsSelect", true);
			} else {
				GameObject.Find (CharacterInfoList [60 * i].Name + "Ani(Clone)").GetComponent<ShopCharacter> ().Shopani.SetBool ("IsSelect", false);
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
			Item.MaxSpeed = System.Convert.ToSingle(ItemElement.GetAttribute ("MaxSpeed"));
			Item.Jump = System.Convert.ToSingle (ItemElement.GetAttribute ("Jump"));
			Item.Booster = System.Convert.ToSingle (ItemElement.GetAttribute ("Booster"));
			Item.Shiled = System.Convert.ToSingle (ItemElement.GetAttribute ("Shiled"));
			Item.UpgradeCost = System.Convert.ToInt32 (ItemElement.GetAttribute ("UpgradeCost"));
			ItemList.Add (Item);

		}
		return ItemList;
	}



	public void ClickBuyfuntion(int ClickButtonNunmber){
		if (CharacterBuyPrice <= PlayerInfoManager.Diamond) { // 캐릭터  구매시
			PlayerInfoManager.Diamond -= CharacterBuyPrice; 
			PlayerInfoManager.PlayerCharacterinfo [1, ClickButtonNunmber] = "1";
            UseGemSE.Play();   //play UseGem.mp3
            ClickBuyCancelButton ();
		} else {   // 캐릭터 구매시 보석이 부족하면 상점이동 팝업창 
			ShopMovePopupNumber = 1;
			BuyPopup.SetActive (false);
			ShopMovePopup.SetActive (true);
			GameObject.Find ("ShowDetailText").GetComponent<Text> ().text = "보석이 부족합니다";
		}
	}
		
	public void ClickBuyCancelButton(){
		DoTouchButton ();
		Scrollrect.GetComponent<ScrollRect> ().horizontal = true; // 구매 버튼 누를시 캐릭터 ScrollRect 고정해제 
		BuyPopup.SetActive (false);
	}
		
	public void ClickBuyButton()
    {
        ClickBuyfuntion (ClickButtonNunmber);
	}


	public void ClickSelectButton(int index){
		index--; // 버튼과 주소 일치시킴
		ClickButtonNunmber = index; //구매 버튼의 번호 값을 저장 
		if (SelectButton [index].GetComponent<Image>().sprite == RunningImage) {
			
		} else if (SelectButton [index].GetComponent<Image>().sprite == SelectImage) {  // 버튼이  선택버튼일 경우
			PlayerInfoManager.SelectCharacter = CharacterInfoList [60 * index].Name;
			SelectCharacterNumber = index;

            GetComponent<AudioSource>().Play(); //play CharChoi.mp3
        }
        else {  // 버튼이 구매버튼일 경우 
			NotTouchButton ();
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
		


	public void ClickPopupUpgradeButton(int index){  // 업그레이드 팝업창 종료시
		index--; // 주소 값 일치 

		NotTouchButton ();
		Scrollrect.GetComponent<ScrollRect> ().horizontal = false; // 업그레이드버튼 누를시 캐릭터 ScrollRect 고정 
		UpgradePopup.SetActive(true);

		// 캐릭터 업그레이드 캐릭터 프리팹 생성
		Character = Resources.Load ("Character/" + CharacterInfoList [index * 60].Name + "StageAni") as GameObject;
		CharacterPosition = GameObject.Find ("UpgradePosition");
		Instantiate (Character, CharacterPosition.transform.position, CharacterPosition.transform.rotation).transform.SetParent (CharacterPosition.transform);
		CharacterComponent = GameObject.Find(CharacterInfoList [index * 60].Name + "StageAni(Clone)");
		CharacterComponent.transform.localScale = new Vector3 (150, 150, 150);
		CharacterComponent.transform.localPosition = new Vector3 (18.5f, -66.6f, -45.4f);
		CharacterComponent.transform.localRotation = Quaternion.Euler (0, -180, 0);	

		ResetUpgradeDetail (index);  // 캐릭터 업그레이드 표기 
		UpgradeNumber = index;

	}

	public void ClickUpgradeCancelButton(){
		DoTouchButton ();
		Scrollrect.GetComponent<ScrollRect> ().horizontal = true; // 업그레이드팝업 취소버튼 누를시 캐릭터 ScrollRect 고정해제 
		UpgradePopup.SetActive (false);
		Destroy (CharacterComponent);
	}


	public void ClickUpgradeButton(){
		if(PlayerInfoManager.Gold > CharacterInfoList [System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, UpgradeNumber])].UpgradeCost ){
			PlayerInfoManager.Gold -= CharacterInfoList [System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, UpgradeNumber])].UpgradeCost;
			PlayerInfoManager.PlayerCharacterinfo [1, UpgradeNumber] = (System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, UpgradeNumber])+1).ToString();
			ResetUpgradeDetail (UpgradeNumber); // 캐릭터 업그레이드 표기 
		}else{	// 캐릭터 업그레이드시 골드가 부족하면 상점이동 팝업창 
			ShopMovePopupNumber = 2;
			UpgradePopup.SetActive (false);
			Destroy (CharacterComponent);
			ShopMovePopup.SetActive (true);
			GameObject.Find ("ShowDetailText").GetComponent<Text> ().text = "골드가 부족합니다";
			Debug.Log ("can't buy");
		}
	
	
	}

	public void ResetUpgradeDetail(int index){  // 캐릭터 업그레이드 표기 함수 
		int IntConvert;
		float floatConvert;
		float floatConvert2;
		String StringConvert;

		IntConvert = System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index]);  //선택한 캐릭터 레벨값 
		//캐릭터 업그레이드 능력치 표기 
		if(CharacterInfoList [index * 60].Name == "rabbit"){
			UpgradeChacaterNameText.text = "토순이";

			if (IntConvert < 60) {
				floatConvert = 20 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Jump;
				TopLevelDetail.text = "점프력 : " + floatConvert.ToString ();
				floatConvert = 20 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].Jump;
				BottomLevelDetail.text = "점프력 : " + floatConvert.ToString ();
			} else{	
				floatConvert = 20 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Jump;
				TopLevelDetail.text = "점프력 : " + floatConvert.ToString ();
				floatConvert = 20 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Jump;
				BottomLevelDetail.text = "점프력 : " + floatConvert.ToString ();
			}



		}else if(CharacterInfoList [index * 60].Name == "turtle"){
			UpgradeChacaterNameText.text = "거북이";

			if (IntConvert < 60) {
				floatConvert = CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])- 1].Shiled;
				TopLevelDetail.text = floatConvert.ToString () + " 초마다 쉴드 생성";
				floatConvert = CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].Shiled;
				BottomLevelDetail.text = floatConvert.ToString () + " 초마다 쉴드 생성";
			} else {
				floatConvert = CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Shiled;
				TopLevelDetail.text = floatConvert.ToString () + " 초마다 쉴드 생성";
				floatConvert = CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Shiled;
				BottomLevelDetail.text = floatConvert.ToString () + " 초마다 쉴드 생성";		
			}

		}else if(CharacterInfoList [index * 60].Name == "cat"){
			UpgradeChacaterNameText.text = "냥냥이";

			if(IntConvert < 60){
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				TopLevelDetail.text = "최대속력 : " +floatConvert.ToString ();
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].MaxSpeed;
				BottomLevelDetail.text = "최대속력 : "+floatConvert.ToString ();
			}else{
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				TopLevelDetail.text = "최대속력 : " +floatConvert.ToString ();
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				BottomLevelDetail.text = "최대속력 : "+floatConvert.ToString ();
			}


		}else if(CharacterInfoList [index * 60].Name == "tiger"){
			UpgradeChacaterNameText.text = "호돌이";


			if (IntConvert < 60) {
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				floatConvert2 = 2 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Booster;
				TopLevelDetail.text = "최대속도 : " + floatConvert.ToString () + "\n부스터시간 : " + floatConvert2.ToString () + "초";

				floatConvert = 45 * CharacterInfoList [(index) * 60 + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].MaxSpeed;
				floatConvert2 = 2 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].Booster;
				BottomLevelDetail.text = "최대속도 : " + floatConvert.ToString () + "\n부스터시간 : " + floatConvert2.ToString () + "초";

			} else {
				floatConvert = 45 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				floatConvert2 = 2 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Booster;
				TopLevelDetail.text = "최대속도 : " + floatConvert.ToString () + "\n부스터시간 : " + floatConvert2.ToString () + "초";

				floatConvert = 45 * CharacterInfoList [(index) * 60 + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].MaxSpeed;
				floatConvert2 = 2 * CharacterInfoList [((index) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])-1].Booster;
				BottomLevelDetail.text = "최대속도 : " + floatConvert.ToString () + "\n부스터시간 : " + floatConvert2.ToString () + "초";
			}
		}


		if (IntConvert < 60) {
			TopLevelText.text = "LV. " + PlayerInfoManager.PlayerCharacterinfo [1, index];
			IntConvert = (System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index]) + 1);
			BottomLevelText.text = "LV. " + IntConvert.ToString ();

			IntConvert = CharacterInfoList [System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, index])].UpgradeCost;
			StringConvert = String.Format ("{0:#,###}", IntConvert);   // 돈 단위를 천 단위에 , 표시 
			NeedUpgradeGold.text = StringConvert;
			GameObject.Find ("UpgradeBuyButton").GetComponent<Button> ().interactable = true;
		} else {

			TopLevelText.text = "LV. 60";
			BottomLevelText.text = "LV. 60";

			NeedUpgradeGold.text = "Level Max";
			GameObject.Find ("UpgradeBuyButton").GetComponent<Button>().interactable = false;
		}	

	}
	public void NotTouchButton(){  // 팝업창 활성화 시 나머지 버튼 비활성화
		for(int i = 0; i < CharacterMax_Count; i++){
			SelectButton [i].GetComponent<Button>().interactable = false; //구매 버튼 누를시 나머지 버튼 비활성화 
			GameObject.Find("UpgradeButton"+(i+1)).GetComponent<Button>().interactable = false ;
			GameObject.Find("ShopButton").GetComponent<Button>().interactable = false ;
			GameObject.Find("CharacterButton").GetComponent<Button>().interactable = false ;
			GameObject.Find("SingleModeButton").GetComponent<Button>().interactable = false ;
			GameObject.Find("MultiModeButton").GetComponent<Button>().interactable = false ;
		}

	}
	public void DoTouchButton(){  // 팝업창 비활성화 시 나머지 버튼 활성화
		for(int i = 0; i < CharacterMax_Count; i++){
			SelectButton [i].GetComponent<Button>().interactable = true; //구매 버튼 누를시 나머지 버튼 활성화 
			GameObject.Find("UpgradeButton"+(i+1)).GetComponent<Button>().interactable = true ;
			GameObject.Find("ShopButton").GetComponent<Button>().interactable = true ;
			GameObject.Find("CharacterButton").GetComponent<Button>().interactable = true ;
			GameObject.Find("SingleModeButton").GetComponent<Button>().interactable = true ;
			GameObject.Find("MultiModeButton").GetComponent<Button>().interactable = true ;
		}

	}
}
