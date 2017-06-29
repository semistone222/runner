using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {
	public  Material[] material;
	 Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
	//	rend.sharedMaterial = material [1];
	}
	public void ChangeIdle(){
		rend.sharedMaterial = material [0];

	}

	public void ChangeVictory(){
		rend.sharedMaterial = material [1];

	}
	public void ChangeLose(){

		rend.sharedMaterial = material [2];
	}
}