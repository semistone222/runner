using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSence : MonoBehaviour {

	public GameObject StagePopup;
	public GameObject ExitPopup;
	public GameObject Shop;
	public GameObject Character;
	public GameObject Single;
	public GameObject Multi;

	private AudioSource clickSound;
    public AudioSource clickSound2;
	public static string SceneName;
	public static int StageNumber ;

	private void Awake()
	{

		clickSound = GetComponent<AudioSource>();
		clickSound.playOnAwake = false;
		Debug.Log ("SceneName = " +SceneName);

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
			Debug.Log ("ShowLoadingSceneShowLoadingSceneShowLoadingScene");
			PlayerInfoManager.RunPoint--; // Player 게임 횟수 1감소
			GameObject.Find ("StageManager").GetComponent<StageManager> ().ActiveItems ();
			Application.LoadLevel ("Loading");
		} else {
			Debug.Log ("하트가 부족합니다.");
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
		clickSound.Play();
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}


	public void ClickNextStage(){
		clickSound.Play();
		StageNumber++;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}

	public void ClickStage1(){
		clickSound.Play();
		StageNumber = 1;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage2(){
		clickSound.Play();
		StageNumber = 2;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage3(){
		clickSound.Play();
		StageNumber = 3;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage4(){
		clickSound.Play();
		StageNumber = 4;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage5(){
		clickSound.Play();
		StageNumber = 5;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}
	public void ClickStage6(){
		clickSound.Play();
		StageNumber = 6;
		SceneName = "Ch.1_Stage"+StageNumber;
		Application.LoadLevel ("StageLobby");
	}

}
