using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour {
	const int MAX_ItemCount = 3;

	public Text Stagetext;
	public Text ItemText;
	public AudioSource clickSound2;
	public Toggle[] ItemToggles = new Toggle[MAX_ItemCount];

	public GameObject PlayerPosition;
	private GameObject Character;
	public GameObject ShopMovePopup;

	enum Item:int{
		Heart, Shield, Booster
	}


	[HideInInspector]
	public static bool[] ItemChecked = new bool[MAX_ItemCount];
	private string[] ItemString = new string[MAX_ItemCount]; 

	// Use this for initialization
	void Start () {
		Timer.CompareTime = 0;  // 타이머 리셋 	
		Stagetext.text = "Stage "+SendSence.StageNumber;
		ItemText.text = "";
		ItemString [0] = "하트 아이템을 사용하면 추가 생명력을 얻을 수 있어요";
		ItemString [1] = "보호막 아이템을 사용하면 장애물에 부딪혔을 때 한 번 무효화해줘요";
		ItemString [2] = "부스터 아이템을 사용하면 시작할 때 부스터 게이지가 가득 찬 상태로 시작해요";
		for (int i = 0; i < MAX_ItemCount; i++) {
			ItemToggles [i].isOn = ItemChecked [i];
		}
		// 플레이어 캐릭터 애니메이션 표기 
		Character = Resources.Load ("Character/"+PlayerInfoManager.SelectCharacter+"StageAni") as GameObject;
		Instantiate (Character, PlayerPosition.transform.position, PlayerPosition.transform.rotation).transform.SetParent (PlayerPosition.transform);
		GameObject.Find (PlayerInfoManager.SelectCharacter+"StageAni(Clone)").GetComponent<ShopCharacter>().Shopani.SetBool ("IsRun", true);

	}
	public void ShowLoadingScene(){
		clickSound2.Play();
		ActiveItems ();
	}
	public void ItemToggle(int index)
	{
		if (PlayerInfoManager.Gold >= 1000 && ItemChecked [index] == false) {
			ItemChecked[index] = !ItemChecked[index];
			ItemText.text = ItemString[index];
			PlayerInfoManager.Gold -= 1000; 
			ItemChecked [index] = true;
		} else if(ItemChecked [index] == true){
			PlayerInfoManager.Gold += 1000; 
			ItemChecked [index] = false;
			ItemText.text = "";
		} else if(PlayerInfoManager.Gold < 1000 && ItemChecked [index] == false){
			ShopMovePopup.SetActive (true);
			ItemChecked [index] = false;
			ItemToggles [index].isOn = false;
		}
	}

	public void ActiveItems()
	{
		if (ItemChecked [(int)Item.Heart]) {
			GimmickDeath.LifeCount++;
		} else if (ItemChecked [(int)Item.Shield]) {
			ItemChecked [(int)Item.Shield] = true;
		} else if (ItemChecked [(int)Item.Booster]) {
			ItemChecked [(int)Item.Booster] = true;
		}
	}
}
