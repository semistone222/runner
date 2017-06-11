using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour {

	public GameObject ShopPanel;
	public GameObject CharacterPanel;
	public GameObject StagePanel;

	void Start(){
		ClickStageButton ();
	}

	public void ClickShopButton(){
		ShopPanel.SetActive (true);
		CharacterPanel.SetActive (false);
		StagePanel.SetActive (false);

	}
	public void ClickCharacterPButton(){
		ShopPanel.SetActive (false);
		CharacterPanel.SetActive (true);
		StagePanel.SetActive (false);
	}
	public void ClickStageButton(){
		ShopPanel.SetActive (false);
		CharacterPanel.SetActive (false);
		StagePanel.SetActive (true);

	}
	public void ClickMultiButton(){
		
	}
}
