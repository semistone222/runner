using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;


public class ResultInfo{
	public int Stage;
	public int BrozenMoney;
	public int SilverTime;
	public int SilverMoney;
	public int GoldTime;
	public int GoldMoney;
}
public class ResultManager : MonoBehaviour {
	public Text StageText;
	public Text FinishTimetext;
	public Text Goldtext;
	public string ClearRank; //현재 클리어한 랭크 
	public int RewardMoney;


	public Text RewardGoldText;
	public Text RewardSilverText;
	public Text RewardBrozenText;

	public Sprite Gold;
	public Sprite Silver;
	public Sprite Brozen;

	public GameObject Medal;
	public GameObject RewardGold;
	public GameObject RewardSilver;
	public GameObject RewardBrozen;


	public GameObject PlayerPosition;
	private GameObject Character;


	public List<ResultInfo> itemList = new List<ResultInfo>(); 

	// Use this for initialization
	void Start () {
		RewardMoney = 0;
		itemList = Read("ResultData"); 
		StageText.text =  ("Stage"+ SendSence.StageNumber);
		FinishTimetext.text = Timer.timesec;
		InitItems ();// 구매한 아이템 및 목숨 초기화
		ClearRankCheckingRank ();
		if (StageStateManager.StageRank [SendSence.StageNumber - 1] == null) {
			NoRankCheckingRank ();
			StageStateManager.PlayingStage++;  // 현재 스테이지 수정 
		}else if(StageStateManager.StageRank [SendSence.StageNumber - 1] == "Gold"){
			GoldRankCheckingRank ();
		}else if(StageStateManager.StageRank [SendSence.StageNumber - 1] == "Silver"){
			SilverRankCheckingRank ();
		}else if(StageStateManager.StageRank [SendSence.StageNumber - 1] == "Brozen"){
			BrozenRankCheckingRank ();
		}
		PlayerInfoManager.Gold += RewardMoney;


		// 플레이어 캐릭터 애니메이션 표기 
		Character = Resources.Load ("Character/"+PlayerInfoManager.SelectCharacter+"StageAni") as GameObject;
		Instantiate (Character, PlayerPosition.transform.position, PlayerPosition.transform.rotation).transform.SetParent(PlayerPosition.transform);
	}
	void Update(){	
		
		GameObject.Find (PlayerInfoManager.SelectCharacter+"StageAni(Clone)").GetComponent<ShopCharacter>().Shopani.SetBool ("IsWin", true);
	
	}

	public static List<ResultInfo> Read(string filepath)
	{
		TextAsset textxml = (TextAsset)Resources.Load (filepath, typeof(TextAsset));
		XmlDocument Document = new XmlDocument ();
		Document.LoadXml (textxml.text);

		XmlElement ItemListElement = Document ["Result"];

		List<ResultInfo> ItemList = new List<ResultInfo> ();

		foreach (XmlElement ItemElement in ItemListElement.ChildNodes) {
			ResultInfo Item = new ResultInfo ();
			Item.Stage = System.Convert.ToInt32 (ItemElement.GetAttribute ("Stage"));
			Item.BrozenMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("BrozenMoney"));
			Item.SilverTime = System.Convert.ToInt32 (ItemElement.GetAttribute ("SilverTime"));
			Item.SilverMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("SilverMoney"));
			Item.GoldTime = System.Convert.ToInt32 (ItemElement.GetAttribute ("GoldTime"));
			Item.GoldMoney = System.Convert.ToInt32 (ItemElement.GetAttribute ("GoldMoney"));
			ItemList.Add (Item);
		}
		return ItemList;
	}

	public void ClearRankCheckingRank ()
	{
		if (Timer.CompareTime <= itemList [SendSence.StageNumber - 1].GoldTime) {
			ClearRank = "Gold";
			Medal.GetComponent<Image> ().sprite = Gold;
		} else if (itemList [SendSence.StageNumber - 1].GoldTime < Timer.CompareTime && Timer.CompareTime <= itemList [SendSence.StageNumber - 1].SilverTime) {
			ClearRank = "Silver";
			Medal.GetComponent<Image> ().sprite = Silver;
		} else if (Timer.CompareTime > itemList [SendSence.StageNumber - 1].SilverTime) {
			ClearRank = "Brozen";
		}

	}
	public void NoRankCheckingRank ()
	{
		if (Timer.CompareTime <= itemList [SendSence.StageNumber - 1].GoldTime) {
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Gold";
			Medal.GetComponent<Image> ().sprite = Gold;
			RewardGold.GetComponent<Image> ().sprite = Gold;
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardMoney = itemList [SendSence.StageNumber - 1].GoldMoney + itemList [SendSence.StageNumber - 1].SilverMoney + itemList [SendSence.StageNumber - 1].BrozenMoney;
			RewardGoldText.text = itemList [SendSence.StageNumber - 1].GoldMoney.ToString() ;
			RewardSilverText.text = itemList [SendSence.StageNumber - 1].SilverMoney.ToString() ;
			RewardBrozenText.text = itemList [SendSence.StageNumber - 1].BrozenMoney.ToString() ;
			Goldtext.text = RewardMoney.ToString ();
		} else if (itemList [SendSence.StageNumber - 1].GoldTime < Timer.CompareTime && Timer.CompareTime <= itemList [SendSence.StageNumber - 1].SilverTime) {
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Silver";
			Medal.GetComponent<Image> ().sprite = Silver;
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardMoney = itemList [SendSence.StageNumber - 1].SilverMoney + itemList [SendSence.StageNumber - 1].BrozenMoney;
			RewardSilverText.text = itemList [SendSence.StageNumber - 1].SilverMoney.ToString() ;
			RewardBrozenText.text = itemList [SendSence.StageNumber - 1].BrozenMoney.ToString() ;
			Goldtext.text = RewardMoney.ToString ();
		} else if (Timer.CompareTime > itemList [SendSence.StageNumber - 1].SilverTime) {
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Brozen";
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardMoney = itemList [SendSence.StageNumber - 1].BrozenMoney;
			RewardBrozenText.text = itemList [SendSence.StageNumber - 1].BrozenMoney.ToString() ;
			Goldtext.text = RewardMoney.ToString ();


		}
	}

	public void GoldRankCheckingRank(){
		RewardMoney = 0;
		RewardGold.GetComponent<Image> ().sprite = Gold;
		RewardSilver.GetComponent<Image> ().sprite = Silver;
		RewardBrozen.GetComponent<Image> ().sprite = Brozen;
		RewardGoldText.text = "0";
		RewardSilverText.text = "0";
		RewardBrozenText.text = "0";
		Goldtext.text = RewardMoney.ToString ();
		
	}
	public void SilverRankCheckingRank(){
		if (ClearRank == "Gold") {
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Gold";
			RewardMoney = itemList [SendSence.StageNumber - 1].GoldMoney;
			RewardGold.GetComponent<Image> ().sprite = Gold;
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardGoldText.text = itemList [SendSence.StageNumber - 1].GoldMoney.ToString() ;
			RewardSilverText.text = "0";
			RewardBrozenText.text = "0";
			Goldtext.text = RewardMoney.ToString ();
		}else{
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardSilverText.text = "0";
			RewardBrozenText.text = "0";
			RewardMoney = 0;
			Goldtext.text = RewardMoney.ToString ();
		}
	}
	public void BrozenRankCheckingRank(){
		if (ClearRank == "Gold") {
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Gold";
			RewardMoney = itemList [SendSence.StageNumber - 1].GoldMoney + itemList [SendSence.StageNumber - 1].SilverMoney;
			RewardGold.GetComponent<Image> ().sprite = Gold;
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardGoldText.text = itemList [SendSence.StageNumber - 1].GoldMoney.ToString() ;
			RewardSilverText.text = itemList [SendSence.StageNumber - 1].SilverMoney.ToString() ;
			RewardBrozenText.text = "0";
			Goldtext.text = RewardMoney.ToString ();
		}else if(ClearRank == "Silver" ){ 
			StageStateManager.StageRank [SendSence.StageNumber - 1] = "Silver";
			RewardMoney = itemList [SendSence.StageNumber - 1].SilverMoney;
			RewardSilver.GetComponent<Image> ().sprite = Silver;
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardSilverText.text = itemList [SendSence.StageNumber - 1].SilverMoney.ToString() ;
			RewardBrozenText.text = "0";
			Goldtext.text = RewardMoney.ToString ();
		}else{
			RewardBrozen.GetComponent<Image> ().sprite = Brozen;
			RewardGoldText.text = "0";
			RewardSilverText.text = "0";
			RewardBrozenText.text = "0";
			RewardMoney = 0;
			Goldtext.text = RewardMoney.ToString ();
		}
	}

	public static void InitItems(){  // 하트, 쉴드, 부스터 아이템 초기화  
		GimmickDeath.LifeCount = 2; // 생명 갯수 초기화
		StageManager.ItemChecked [0] = false;
		StageManager.ItemChecked [1] = false;
		StageManager.ItemChecked [2] = false;
	}
}