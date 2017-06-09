using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour {
	public GameObject text;
	public GameObject FinishTimer;
	public GameObject RunningTimer;
	public Text Timetext;
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
			Debug.Log ("hi");
			text.SetActive (true);
			FinishTimer.SetActive (true);
			Timetext.text = Timer.timesec;
			RunningTimer.SetActive(false);
			StartCoroutine (FinishGame());

		}
	}

	IEnumerator FinishGame(){
		yield return new WaitForSeconds (3.5f);
		Application.LoadLevel("Result");
	}
}
