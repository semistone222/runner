using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSence : MonoBehaviour {

	public GameObject StagePopup;
	public GameObject ExitPopup;
	private AudioSource clickSound;

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

	public void ClickExitButton(){	
		clickSound.Play();
		ExitPopup.SetActive (true);
	}

	public void ClickNoExitButton(){	
		clickSound.Play();
		ExitPopup.SetActive (false);
	}
		
	public void ClickStagePopupExitButton(){	
		clickSound.Play();
		StagePopup.SetActive (false);
	}
	public void ClickYesExitButton(){
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

	public void ClickStage1(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage1");
	}
	public void ClickStage2(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage2");
	}
	public void ClickStage3(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage3");
	}
	public void ClickStage4(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage4");
	}
	public void ClickStage5(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage5");
	}
	public void ClickStage6(){
		clickSound.Play();
		Application.LoadLevel ("Ch.1_Stage6");
	}

}
