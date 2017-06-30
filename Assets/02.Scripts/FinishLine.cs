using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour {
	public GameObject text;
	public GameObject FinishTimer;
	public GameObject RunningTimer;
	// Use this for initialization
	void Start () {
		text.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player") {
			text.SetActive (true);
			FinishTimer.SetActive (true);
			FinishTimer.GetComponent<Text>().text= Timer.timesec;
			RunningTimer.SetActive(false);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerOff> ().MOVESPD_ORIGIN = 0;
			GameObject.Find ("ButtonBooster").GetComponent<Button> ().interactable = false;  // 부스터 버튼 클릭 불가
			StartCoroutine (FinishGame());


		}
	}

	IEnumerator FinishGame(){
		yield return new WaitForSeconds (3.5f);
		GameObject.Find ("ButtonBooster").GetComponent<Button> ().interactable = true;  // 부스터 버튼 클릭 활성
		Application.LoadLevel("Result");
	}
}
