using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLaoding : MonoBehaviour {

	public Text StageText;
	public GameObject InitTimeImage;
	public GameObject TimeImage;
	public GameObject StartBackGround;
	public GameObject Boundary;
	public GameObject Booster;
	public GameObject Sound;
	public GameObject number;
	public GameObject StartPosition;

	private GameObject Character;


	public Sprite number1;
	public Sprite number2;
	public Sprite number3;



	float CheckTime = 0;

	Coroutine StartLoading;

	// Use this for initialization
	void Start () {
		Character = Resources.Load ("Character/"+PlayerInfoManager.SelectCharacter) as GameObject;
		Instantiate (Character, StartPosition.transform.position, StartPosition.transform.rotation);

		StageText.text = "" + SendSence.StageNumber	;
		InitTimeImage.SetActive (true);
		StartBackGround.SetActive (true);
		Sound.SetActive (false);
		DeathRetry.DeathRetryPopupAgain = false; // 리트라이 값  초기화 
		StartLoading = StartCoroutine (startLoadingControl());
	}

	IEnumerator startLoadingControl(){
		yield return new WaitForSeconds (2.0f);
		StartBackGround.SetActive (false);
		number.SetActive (true);
		number.GetComponent<Image> ().sprite = number3;
		Sound.SetActive (true);
		yield return new WaitForSeconds (1.0f);
		number.GetComponent<Image> ().sprite = number2;
		yield return new WaitForSeconds (1.0f);
		number.GetComponent<Image> ().sprite = number1;
		yield return new WaitForSeconds (1.0f);
		Boundary.SetActive(false);
		number.SetActive (false);
		Booster.SetActive (true);
		InitTimeImage.SetActive (false);
		TimeImage.SetActive (true);
		StopCoroutine (StartLoading);
	}
}
