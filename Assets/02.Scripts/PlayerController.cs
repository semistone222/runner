using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float gravity;



	private Transform myTransform;
	private CharacterController myCharacterController;
	private PhotonView myPhotonView;
	private Camera mainCamera;
	private Vector3 inputVec;
	private Vector3 moveVec;
	private float jumpVal;

	private int readyPlayerNum;

	private Vector3 currPos = Vector3.zero;
	private Quaternion currRot = Quaternion.identity;
	public Animator ani;

	private GameObject StartText;
	private GameObject StartBoundary;
	private GameObject ReadyLine;

	Coroutine Starttimer;
	Coroutine Stoptimer;


	void Awake () {

		StartText = GameObject.FindGameObjectWithTag ("StartText");
		StartBoundary = GameObject.FindGameObjectWithTag ("StartBoundary");
		ReadyLine = GameObject.FindGameObjectWithTag ("StartZone");

		myTransform = GetComponent<Transform> ();
		myCharacterController = GetComponent<CharacterController> ();
		myPhotonView = GetComponent<PhotonView> ();
		mainCamera = Camera.main;

		if (myPhotonView.isMine) {
			Camera.main.GetComponent<CameraController> ().player = this.gameObject;
		}

		currPos = myTransform.position;
		currRot = myTransform.rotation;


//		StartText = GetComponent<Text> ();
	}

	void FixedUpdate () {

		if (!myPhotonView.isMine)
			return;

		inputVec = new Vector3 (CnInputManager.GetAxis ("JoyStickX"), CnInputManager.GetAxis ("JoyStickY"));
		moveVec = Vector3.zero;

		if (inputVec.sqrMagnitude > 0.001f) {
			moveVec = mainCamera.transform.TransformDirection (inputVec);
			moveVec.y = 0f;
			moveVec.Normalize ();
			myTransform.forward = moveVec;
			moveVec *= moveSpeed;
		}

		// Animation 동작 부분
		if (inputVec.x == 0 && inputVec.y == 0) {
			ani.SetBool ("IsRun", false);

		} else {
			ani.SetBool ("IsRun", true);
		}


		 
		if (myCharacterController.isGrounded) {
			if (CnInputManager.GetButtonDown ("Jump")) {
				jumpVal = jumpSpeed;
			} else {
				jumpVal = 0;
			}
		}

		JumpCheck ();
		jumpVal -= gravity * Time.deltaTime;
		moveVec.y = jumpVal;
		myCharacterController.Move (moveVec * Time.deltaTime);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

	//	Debug.Log("OnPhontonSerializeView");

		if (stream.isWriting) {
			stream.SendNext (myTransform.position);
			stream.SendNext (myTransform.rotation);
		} else {
			currPos = (Vector3) stream.ReceiveNext ();
			currRot = (Quaternion) stream.ReceiveNext ();
		}
	}

	public void JumpCheck()
	{
		if (myCharacterController.isGrounded) {
			if (CnInputManager.GetButtonDown ("Jump")) {
				jumpVal = jumpSpeed;
			} else {
				jumpVal = 0;
			}
		}
	}


	void Update() {

		if (myPhotonView.isMine) {

		} else {
			myTransform.position = Vector3.Lerp (myTransform.position, currPos, Time.deltaTime * 10.0f);
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, currRot, Time.deltaTime * 10.0f);
		}
	}

	IEnumerator StartGame(){
		int time = 0;
		while(true){
			if(time <=3)
			time++; 
			StartText.GetComponent<Text> ().text = ""+time;
			if (time >3 && time <= 5) {
				time++; 
				StartText.GetComponent<Text> ().text = "Start!!";
			}

			if (time > 5) {
				StartText.GetComponent<Text> ().text = "";
				StartBoundary.SetActive (false);
				StartText.SetActive (false);
				ReadyLine.SetActive (false);
				break;
			}
			yield return new WaitForSeconds(1.0f);
		}
	}



	private void OnTriggerEnter(Collider other)
	{

		Debug.Log("ininiininininininininininininininin");
		switch (other.transform.tag)
		{
		case "StartZone":
			myPhotonView.RPC("OnGameReady", PhotonTargets.MasterClient, null);
			break;
		case "EndZone":
			myPhotonView.RPC("OnGameEnd", PhotonTargets.MasterClient, myTransform.name);
			break;
		default:
			break;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		switch (other.transform.tag)
		{
		case "StartZone":
			myPhotonView.RPC("OnReadyCancel", PhotonTargets.MasterClient, null);
			break;
		default:
			break;
		}
	}

	[PunRPC]
	void OnGameReady()
	{
		readyPlayerNum++;
		if (PhotonNetwork.room.PlayerCount >= 2 && readyPlayerNum > PhotonNetwork.room.PlayerCount / 2)
		{
			myPhotonView.RPC("OnGameStart", PhotonTargets.AllViaServer, null);
		}
		Debug.Log("ReadyNum : " + readyPlayerNum + "    PhotonNetwork.room.PlayerCount :"+PhotonNetwork.room.PlayerCount);
	}

	[PunRPC]
	void OnReadyCancel()
	{
		readyPlayerNum--;
	//	Starttimer = StartCoroutine (StartGame());
		Debug.Log("ReadyNum : " + readyPlayerNum + "    PhotonNetwork.room.PlayerCount :"+PhotonNetwork.room.PlayerCount);
		if (PhotonNetwork.room.PlayerCount >= 2 && readyPlayerNum <= PhotonNetwork.room.PlayerCount / 2) {		
			myPhotonView.RPC("StopStartgame", PhotonTargets.AllViaServer, null);
		}
		Debug.Log("ReadyNum : " + readyPlayerNum);
	}

	[PunRPC]
	void StopStartgame()
	{
		StopCoroutine (Starttimer);
		StartText.GetComponent<Text> ().text = "";
	}

	[PunRPC]
	void OnGameStart()
	{
		Debug.Log("OnGameStart");
		Starttimer = StartCoroutine (StartGame());
	}

	[PunRPC]
	void OnGameEnd(string name)
	{
		Debug.Log("OnGameEnd : " + name);
	}


}