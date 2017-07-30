using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStateManager : MonoBehaviour {
	const int StageMaxCount = 10;

	public GameObject[] StageButton = new GameObject[StageMaxCount];
	public GameObject[] StageMedal = new GameObject[StageMaxCount];
	public static int PlayingStage = 0;
	public static string[] StageRank = new string[StageMaxCount];

	public Sprite PlayingStageImage;
	public Sprite GoldMedal;
	public Sprite SilverMedal;
	public Sprite BrozenMedal;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < StageMaxCount; i++) {
			if (i <= PlayingStage) {
				StageMedal [i].SetActive (true);
			} else if (i > PlayingStage) {
				//StageButton [i].GetComponent<Button> ().interactable = false;
			}
			if (StageRank [i] == null) {
				StageMedal [i].GetComponent<Image> ().sprite = PlayingStageImage;
			} else if (StageRank [i] == "Gold") {
				StageMedal [i].GetComponent<Image> ().sprite = GoldMedal;
			} else if (StageRank [i] == "Silver") {
				StageMedal [i].GetComponent<Image> ().sprite = SilverMedal;
			} else if (StageRank [i] == "Brozen") {
				StageMedal [i].GetComponent<Image> ().sprite = BrozenMedal;
			}
		}
	}
}