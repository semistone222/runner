using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
	public GameObject text;

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
			StartCoroutine (FinishGame());

		}
	}

	IEnumerator FinishGame(){
		yield return new WaitForSeconds (3.5f);
		Application.LoadLevel("SelectMode");
	}
}
