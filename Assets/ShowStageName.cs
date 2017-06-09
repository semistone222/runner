using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStageName : MonoBehaviour {
	public Text StageName;
	// Use this for initialization
	void Start () {
		StageName.text = ("Stage"+ SendSence.StageNumber);
	}
}
