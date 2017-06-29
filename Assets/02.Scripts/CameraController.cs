using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[HideInInspector]
	public GameObject player;
	public float rotationSpeed;
	private Vector3 plus = new Vector3 (0, 7, 0);
	private Vector3 offset = new Vector3(0, -1, -8);

    void Update()
    {
        //카메라 감도값 유지.
        rotationSpeed = UIValue.camValue;
    }

void LateUpdate () {
		if (!player)
			return;

		var h = CnInputManager.GetAxis("TouchPadX");
		offset = Quaternion.AngleAxis (h * rotationSpeed, Vector3.up) * offset;
		transform.position = player.transform.position + offset + plus;
		transform.LookAt (player.transform.position - offset);
	}
}
