using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float rotationSpeed;

	private Transform myTransform;
	private Vector3 offset;

	void Start () {
		myTransform = GetComponent<Transform> ();
		offset = myTransform.position - player.transform.position;
	}

	void Update () {
		var h = CnInputManager.GetAxis("TouchPadX");
		offset = Quaternion.AngleAxis (h * rotationSpeed, Vector3.up) * offset;
		myTransform.position = player.transform.position + offset;
		myTransform.LookAt (player.transform.position);
	}
}
