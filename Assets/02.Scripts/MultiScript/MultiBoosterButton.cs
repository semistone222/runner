using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MultiBoosterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public Image img;
	public UnityEngine.UI.Button btn;
	float coolTime = 30.0f;
	public bool disableOnStart = true;
	private bool BoosterOn = false;
	float leftTime = 30.0f;
	public float ChargeTime = 0f;
	float BoosterTime = 0;
	public float BoosterSpeed;


	private GameObject BoosterEffect;
	public Vector2 pos;

	public bool check;
	// Use this for initialization
	void Start ()
    {
		ChargeTime = 30;
		btn.enabled = false;

		if (img == null) {
			img = gameObject.GetComponent<Image> ();
		}
		if (btn == null) {
			btn = gameObject.GetComponent<UnityEngine.UI.Button> ();
		}
		if (disableOnStart){
		//	ResetBoostertime ();
		}
		//btn.enabled = false;
        //BoosterSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerOff>().MOVESPD_ORIGIN;
		BoosterEffect = GameObject.FindGameObjectWithTag ("Player");

	}

	// Update is called once per frame
	void Update () {	
		if (StageManager.ItemChecked [2] == true) { // Booster 구매시 
			ChargeTime = 30f;
		}

		if (check) {
			BoosterOn = true;
			if(BoosterOn){
				Debug.Log ("Click");
				if (ChargeTime > 0) {
					ChargeTime -= 6 * Time.deltaTime;
					ResetBoostertime ();
				} else {
					ChargeTime = 0f;
					BoosterOn = false;
					check = false;
				}

			//	ResetBoostertime ();

			}
		} else {
			BoosterOn = false;
			ResetBoosterSpeed ();
			ChargeTime += 3 * Time.deltaTime;
				if (ChargeTime >= 30) {
					ChargeTime = 30;
				}

				if (ChargeTime < 6) {
					if (btn)
						btn.enabled = false;
						BoosterOn = false;
				}



		}
	
		// 버튼 게이지 차는 이미지 
		float ratio = ChargeTime / 30;

		if (img)
			img.fillAmount = ratio;


	}

	public void ResetBoostertime(){  // 클릭시
		BoosterEffect.GetComponent<TrailRenderer> ().enabled = true;
		BoosterSpeed = 54;
		Debug.Log ("BoosterSpeed: "+BoosterSpeed);
		GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerController> ().moveSpeed= BoosterSpeed;
        GetComponent<AudioSource>().Play();
        BoosterOn = true;

		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("uP");
		}
	}

	public void ResetBoosterSpeed(){
		//GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerController> ().moveSpeed= 45;
		BoosterEffect.GetComponent<TrailRenderer> ().enabled = false;
	}

	public void OnPointerDown(PointerEventData eventData){
		check = true;
	}

	public void OnPointerUp(PointerEventData eventData){
		check = false;
	}
}
