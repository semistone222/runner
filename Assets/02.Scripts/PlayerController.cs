using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public float gravity;

	private bool isJumping;
	private Transform myTransform;
	private Rigidbody myRigidbody;
	private CharacterController myCharacterController;
	private Camera mainCamera;
	private Vector3 inputVec;
	private Vector3 moveVec;
	private float jumpVal;

	void Start () {
		isJumping = false;
		myTransform = GetComponent<Transform> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myCharacterController = GetComponent<CharacterController> ();
		mainCamera = Camera.main;
	}

	void FixedUpdate () {
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
}
