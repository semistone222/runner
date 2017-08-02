using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SendSence : MonoBehaviour {

	public GameObject StagePopup;
	public GameObject ExitPopup;
	public GameObject Shop;
	public GameObject Character;
	public GameObject Single;
	public GameObject Multi;
	public GameObject ShowMovePopup;

	private GameObject SoundManager;

	public static string SceneName;
	public static int StageNumber ;
	public static bool RunPointShop = false; 

	private void Awake()
	{
		SoundManager = GameObject.Find ("SoundManager");
	}


	public void ClickSingleButton(){
		StagePopup.SetActive (true);
	}
		
    public void ClickExitButton(){	
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		ExitPopup.SetActive (true);
	}

	public void ClickNoExitButton(){	
		SoundManager.GetComponent<SoundManager> ().PlayClickSound2 ();
		ExitPopup.SetActive (false);
	}
		
	public void ClickStagePopupExitButton(){	
		for (int i = 0; i < 3; i++) {
			StageManager.ItemChecked [i] = false;
		}
		SoundManager.GetComponent<SoundManager> ().PlayClickSound2 ();
		StagePopup.SetActive (false);
	}

	public void ShowSelectMode(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		ShopMove.StageToStore = false;
		Application.LoadLevel ("SelectMode");
	}
		
	public void ShowSingleMode(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		StagePopup.SetActive (true);
	}

	public void ShowMultiMode(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		Application.LoadLevel ("Lobby");
	}

    public void ShowMultiMode1()
    {
        SoundManager.GetComponent<SoundManager>().PlayClickSound3();
        Application.LoadLevel("Lobby 1");
    }

    public void ShowMultiMode2()
    {
        SoundManager.GetComponent<SoundManager>().PlayClickSound3();
        Application.LoadLevel("Lobby 2");
    }

    public void ShowLoadingScene(){
		SoundManager.GetComponent<SoundManager> ().PlayStageSelectSE ();
		if (PlayerInfoManager.RunPoint > 0) {
			PlayerInfoManager.RunPoint--; // Player 게임 횟수 1감소
			GameObject.Find ("StageManager").GetComponent<StageManager> ().ActiveItems ();
			StartCoroutine ("MoveLoadingScene");
		} else {  // 런 포인트 부족시 상점이동 팝업창 활성
			ShowMovePopup.SetActive(true);
			RunPointShop = true;
			GameObject.Find ("ShowDetailText").GetComponent<Text> ().text = "런 포인트가 부족합니다";
		}
	}

	public void ClickBackButton(){
		SoundManager.GetComponent<SoundManager> ().PlayClickSound3 ();
		for(int i = 0 ; i < 3 ; i++){
			if(StageManager.ItemChecked[i]  == true){
				PlayerInfoManager.Gold += 1000; 
				StageManager.ItemChecked [i] = false;
			}
		}
		Application.LoadLevel ("SelectMode");
	}
	public void ClickAgainStage(){
		SoundManager.GetComponent<SoundManager> ().PlayStageSelectSE ();
		SceneName = "Ch.1_Stage"+StageNumber;
	//	StartCoroutine ("MoveStageScene");
		Application.LoadLevel ("StageLobby");
	}


	public void ClickNextStage(){
		SoundManager.GetComponent<SoundManager> ().PlayStageSelectSE ();
		StageNumber++;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}

	public void ClickStage(int index){
		SoundManager.GetComponent<SoundManager> ().PlayStageSelectSE ();
		StageNumber = index;
		SceneName = "Ch.1_Stage"+StageNumber;
		StartCoroutine ("MoveStageScene");
	}

	IEnumerator MoveStageScene(){
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel ("StageLobby");
	}

	IEnumerator MoveLoadingScene(){
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel ("Loading");
	}
}
