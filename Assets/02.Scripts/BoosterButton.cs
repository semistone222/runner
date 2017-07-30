using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoosterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public Image img;
	public UnityEngine.UI.Button btn;
	float coolTime = 30.0f;
	public bool disableOnStart = true;
	private bool BoosterOn = false;
	float leftTime = 30.0f;
	public float ChargeTime = 0f;
	float BoosterTime = 0;
	float BoosteringTime ;
	public float BoosterSpeed;

	private GameObject target;
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
        
		// 각 캐릭터 부스터 유지 시간
		BoosteringTime = 2 * System.Convert.ToSingle (CharacterManager.CharacterInfoList [ ((CharacterManager.SelectCharacterNumber) * 60) +  System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, CharacterManager.SelectCharacterNumber])-1].Booster);
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
		GameObject.FindGameObjectWithTag ("Player").GetComponent<TrailRenderer> ().enabled = true;
		BoosterSpeed = 45 * System.Convert.ToSingle (CharacterManager.CharacterInfoList [((CharacterManager.SelectCharacterNumber) * 60) + System.Convert.ToInt32 (PlayerInfoManager.PlayerCharacterinfo [1, CharacterManager.SelectCharacterNumber]) - 1].MaxSpeed);
		BoosterSpeed = BoosterSpeed + (BoosterSpeed * 0.2f);
		Debug.Log ("BoosterSpeed: "+BoosterSpeed);
		GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerControllerOff> ().MOVESPD_ORIGIN= BoosterSpeed;
        GetComponent<AudioSource>().Play();
        BoosterOn = true;

		if (Input.GetMouseButtonUp(0)) {
			Debug.Log("uP");
		}
	}

	public void ResetBoosterSpeed(){
		BoosterSpeed = PlayerControllerOff.DeathBeforeSpeed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerControllerOff> ().MOVESPD_ORIGIN= BoosterSpeed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<TrailRenderer> ().enabled = false;
	}

	public void OnPointerDown(PointerEventData eventData){
		check = true;
	}

	public void OnPointerUp(PointerEventData eventData){
		check = false;
	}
}
