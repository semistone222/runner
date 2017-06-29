using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {
	public GameObject Background;
	public GameObject ShopPanel;
	public GameObject CharacterPanel;
	public GameObject StagePanel;
	public GameObject MapName;
	public GameObject CheckImage;

	public Toggle RunPoint;
	public Toggle Diamond;
	public Toggle Gold;

	public Sprite StageBackground;
	public Sprite ShopBackground;
	public Sprite CharacterTextImage;
	public Sprite ShopTextImage;


	void Start(){
				CharacterManager.CharacterInfoList = CharacterManager.Read ("CharacterData");  // 첫 캐릭터 XML정보 값 불러오기 
				ClickStageButton ();
		// 만약에 상점으로 이동 팝업창을 통해 메인에 들어오면 상점에 들어감
			if (SendSence.RunPointShop == true) { // 스테이지 화면에서 런포인트가 부족해서 상점으로 이동하면 런 포인트 상품 보여줌
				CharacterManager.CharacterInfoList = CharacterManager.Read ("CharacterData");  // 첫 캐릭터 XML정보 값 불러오기 
				ClickShopButton ();
				GameObject.Find ("ShopPopup").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-88, 0);
				SendSence.RunPointShop = false;
			}else if(ShopMove.StageToStore == true){
				CharacterManager.CharacterInfoList = CharacterManager.Read ("CharacterData");  // 첫 캐릭터 XML정보 값 불러오기 
				ClickShopButton ();
				GameObject.Find ("ShopPopup").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-510, 0);  // 스테이지 화면에서 골드가 부족해서 상점으로 이동하면 골드 상품 보여줌
				ShopMove.StageToStore = false;
			}else if (ShopMove.DeathRetryToStore == true){
				CharacterManager.CharacterInfoList = CharacterManager.Read ("CharacterData");  // 첫 캐릭터 XML정보 값 불러오기 
				ClickShopButton ();
				ShopMove.DeathRetryToStore = false;
			}
		
	}

	public void ClickShopButton(){
		Background.GetComponent<SpriteRenderer> ().sprite = ShopBackground;
		ShopPanel.SetActive (true);
		CharacterPanel.SetActive (false);
		StagePanel.SetActive (false);
		MapName.SetActive (false);
		CheckImage.SetActive (true);
		CheckImage.GetComponent<Image>().sprite = ShopTextImage;
		RunPoint.isOn = false;
		Diamond.isOn = false;
		Gold.isOn = false;
	}
	public void ClickCharacterPButton(){
		Background.GetComponent<SpriteRenderer> ().sprite = ShopBackground;
		ShopPanel.SetActive (false);
		CharacterPanel.SetActive (true);
		StagePanel.SetActive (false);
		MapName.SetActive (false);
		CheckImage.SetActive (true);
		CheckImage.GetComponent<Image>().sprite = CharacterTextImage;
		RunPoint.isOn = false;
		Diamond.isOn = false;
		Gold.isOn = false;
	}
	public void ClickStageButton(){
		Background.GetComponent<SpriteRenderer> ().sprite = StageBackground;
		ShopPanel.SetActive (false);
		CharacterPanel.SetActive (false);
		StagePanel.SetActive (true);
		MapName.SetActive (true);
		CheckImage.SetActive (false);
		RunPoint.isOn = false;
		Diamond.isOn = false;
		Gold.isOn = false;

	}
	public void ClickMultiButton(){
		
	}
	public void ClickRunPountDetail(){
		if (RunPoint.isOn == true) {
			Diamond.isOn = false;
			Gold.isOn = false;
		} else {
			RunPoint.isOn = false;
		}
	}
	public void ClickDiamondDetail(){
		if (Diamond.isOn == true) {
			RunPoint.isOn = false;
			Gold.isOn = false;
		} else {
			Diamond.isOn = false;
		}
	}
	public void ClickdGoldtDetail(){
		if (Gold.isOn == true) {
			RunPoint.isOn = false;
			Diamond.isOn = false;
		} else {
			Gold.isOn = false;
		}
	}
}
