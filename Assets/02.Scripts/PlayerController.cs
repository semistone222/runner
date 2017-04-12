using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;

	private Transform tr;
	private Rigidbody rb;

	void Start () {
		tr = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		Move ();

		if (Input.GetKeyDown ("space")) {
			Jump ();
		}
	}

	void Move() {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Debug.Log ("H = " + h.ToString ());
		Debug.Log ("V = " + v.ToString ());

		Vector3 moveDir = new Vector3(h, 0, v);
		moveDir *= moveSpeed;

		tr.Translate(moveDir * Time.deltaTime, Space.Self);
	}

	void Jump() {
		rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
