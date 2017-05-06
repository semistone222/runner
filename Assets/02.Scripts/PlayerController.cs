using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

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

	private Vector3 currPos = Vector3.zero;
	private Quaternion currRot = Quaternion.identity;

	void Awake () {
		myTransform = GetComponent<Transform> ();
		myCharacterController = GetComponent<CharacterController> ();
		myPhotonView = GetComponent<PhotonView> ();
		mainCamera = Camera.main;

		if (myPhotonView.isMine) {
			Camera.main.GetComponent<CameraController> ().player = this.gameObject;
		}

		currPos = myTransform.position;
		currRot = myTransform.rotation;
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
		 
		if (myCharacterController.isGrounded) {
			if (CnInputManager.GetButtonDown ("Jump")) {
				jumpVal = jumpSpeed;
			}
		}

		jumpVal -= gravity * Time.deltaTime;
		moveVec.y = jumpVal;
		myCharacterController.Move (moveVec * Time.deltaTime);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		if (stream.isWriting) {
			stream.SendNext (myTransform.position);
			stream.SendNext (myTransform.rotation);
		} else {
			currPos = (Vector3) stream.ReceiveNext ();
			currRot = (Quaternion) stream.ReceiveNext ();
		}
	}

	void Update() {

		if (myPhotonView.isMine) {

		} else {
			myTransform.position = Vector3.Lerp (myTransform.position, currPos, Time.deltaTime * 3.0f);
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, currRot, Time.deltaTime * 3.0f);
		}
	}
}
