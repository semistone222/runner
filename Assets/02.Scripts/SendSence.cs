using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSence : MonoBehaviour {

	public GameObject StagePopup;
	public GameObject ExitPopup;
	private AudioSource clickSound;
    public AudioSource clickSound2;
	public static string SceneName;
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

    public void ClickExitButton(){	
		clickSound.Play();
		ExitPopup.SetActive (true);
	}

	public void ClickNoExitButton(){	
		clickSound2.Play();
		ExitPopup.SetActive (false);
	}
		
	public void ClickStagePopupExitButton(){	
		clickSound2.Play();
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
		SceneName = "Ch.1_Stage1";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}
	public void ClickStage2(){
		clickSound.Play();
		SceneName = "Ch.1_Stage2";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}
	public void ClickStage3(){
		clickSound.Play();
		SceneName = "Ch.1_Stage3";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}
	public void ClickStage4(){
		clickSound.Play();
		SceneName = "Ch.1_Stage4";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}
	public void ClickStage5(){
		clickSound.Play();
		SceneName = "Ch.1_Stage5";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}
	public void ClickStage6(){
		clickSound.Play();
		SceneName = "Ch.1_Stage6";
		Debug.Log ("SceneName = " +SceneName);
		Application.LoadLevel ("Loading");
	}

}
