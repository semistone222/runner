﻿using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[HideInInspector]
	public GameObject player;
	public float rotationSpeed;

	private Vector3 offset = new Vector3(0, 5, -10);

	void LateUpdate () {
		if (!player)
			return;

		var h = CnInputManager.GetAxis("TouchPadX");
		offset = Quaternion.AngleAxis (h * rotationSpeed, Vector3.up) * offset;
		transform.position = player.transform.position + offset;
		transform.LookAt (player.transform.position - offset);
	}
}
