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

	private AudioSource clickSound;
    public AudioSource clickSound2;
    public AudioSource StageSelectSE;
	public static string SceneName;
	public static int StageNumber ;
	public static bool RunPointShop = false; 

	private void Awake()
	{

		clickSound = GetComponent<AudioSource>();
		clickSound.playOnAwake = false;
	}

	private void ClickSound()
	{
		if (clickSound.isPlaying == false)
		{
			clickSound.Play();
		}
	}

    private void ClickSound2()
    {
        if (clickSound2.isPlaying == false)
        {
            clickSound2.Play();
        }
    }

	public void ClickSingleButton(){
		StagePopup.SetActive (true);
	}
		
    public void ClickExitButton(){	
		clickSound.Play();
		ExitPopup.SetActive (true);
	}

	public void ClickNoExitButton(){	
		clickSound2.Play();
		ExitPopup.SetActive (false);
	}
		
	public void ClickStagePopupExitButton(){	
		for (int i = 0; i < 3; i++) {
			StageManager.ItemChecked [i] = false;
		}
		clickSound2.Play();
		StagePopup.SetActive (false);
	}

	public void ShowSelectMode(){
		clickSound.Play();
		Application.LoadLevel ("SelectMode");
	}
		
	public void ShowSingleMode(){
		clickSound.Play();
		StagePopup.SetActive (true);
	}

	public void ShowMultiMode(){
		clickSound.Play();
		Application.LoadLevel ("Lobby");
	}

	public void ShowLoadingScene(){
		clickSound.Play();
		if (PlayerInfoManager.RunPoint > 0) {
			PlayerInfoManager.RunPoint--; // Player 게임 횟수 1감소
			GameObject.Find ("StageManager").GetComponent<StageManager> ().ActiveItems ();
			Application.LoadLevel ("Loading");
		} else {  // 런 포인트 부족시 상점이동 팝업창 활성
			ShowMovePopup.SetActive(true);
			RunPointShop = true;
			GameObject.Find ("ShowDetailText").GetComponent<Text> ().text = "런 포인트가 부족합니다";
		}
	}

	public void ClickBackButton(){
		for(int i = 0 ; i < 3 ; i++){
			if(StageManager.ItemChecked[i]  == true){
				PlayerInfoManager.Gold += 1000; 
				StageManager.ItemChecked [i] = false;
			}
		}
		Application.LoadLevel ("SelectMode");
	}
	public void ClickAgainStage(){
		StageSelectSE.Play();
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}


	public void ClickNextStage(){
		StageSelectSE.Play();
		StageNumber++;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}

	public void ClickStage1(){
		StageSelectSE.Play();
		StageNumber = 1;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage2(){
		StageSelectSE.Play();
		StageNumber = 2;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage3(){
		StageSelectSE.Play();
		StageNumber = 3;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage4(){
		StageSelectSE.Play();
		StageNumber = 4;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage5(){
		StageSelectSE.Play();
		StageNumber = 5;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage6(){
		StageSelectSE.Play();
		StageNumber = 6;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}

    public void ClickStage7()
    {
        StageSelectSE.Play();
        StageNumber = 7;
        SceneName = "Ch.1_Stage" + StageNumber;
        Application.LoadLevel("StageLobby");
    }

    public void ClickStage8()
    {
        StageSelectSE.Play();
        StageNumber = 8;
        SceneName = "Ch.1_Stage" + StageNumber;
        Application.LoadLevel("StageLobby");
    }
}
