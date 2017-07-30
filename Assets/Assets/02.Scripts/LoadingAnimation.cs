using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour {
	public Animator ani;
	// Use this for initialization
	void Start () {
		ani.SetBool ("IsRun", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
