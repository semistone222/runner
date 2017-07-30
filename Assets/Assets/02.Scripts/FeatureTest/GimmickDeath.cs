using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CnControls;
/*
 *  GimmickDeath , by Jin-seok, Yu
 * 
 *  Last Update : Apr 29th, 2017
 * 
 *  떨어지면 사망 판정되어, 다시 Respawn 시키는 오브젝트입니다.
 */



public class GimmickDeath : Gimmick
{
	public GameObject DeathRetryPopup;
	public static int LifeCount = 2;
	private  GameObject Life1;
	private  GameObject Life2;
	private  GameObject Life3;
	public static bool Death = false;

	void Start(){
		
		Life1 = GameObject.Find ("Life1");
		Life2 = GameObject.Find ("Life2");
		Life3 = GameObject.Find ("Life3");

		if (LifeCount == 2) {
			Life3.SetActive (false);
		}
		Death = false;
	}

	public override void EnterFunc(Collider other)
	{

		LifeCount--;

		StartCoroutine ("DeadAnimarter");
		Respawn(other);


		if (LifeCount == 2) {
			Life3.SetActive (false);
		} else if (LifeCount == 1) {
			Life2.SetActive (false);
		} else {
			Life1.SetActive (false);
			if (DeathRetry.DeathRetryPopupAgain == false) {
				DeathRetryPopup.SetActive (true);
				Time.timeScale = 0;
			} else { // 리트라이 이후 죽을 경우 바로 스테이지 이동 

                SoundManager sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

                ResultManager.InitItems ();  // 아이템 목숨 초기화 
				DeathRetry.DeathRetryPopupAgain = false;

                sm.SetBGMNull();
                sm.SetBGM1();

                Application.LoadLevel ("SelectMode");
			}
		}

	//	GameObject.Find ("JoyStick").GetComponent<SimpleJoystick> ().OnPointerUp (PointerEventData);
	//	BeforeRespawn();

	}
	private void BeforeRespawn()
	{
		/*Respawn 전에 호출될 함수입니다. 애니메이션, 타이머 등이 들어갈 것으로 예상합니다.*/
	}
	private void Respawn(Collider other)
	{   /*Respawn 함수입니다. 현재는 부딪힌 플레이어에 저장된 respawnPoint로 위치를 강제 변경합니다.*/

		/*온라인일때*/
		if (other.GetComponent<PlayerController>() != null)
		{
			//other.transform.position = other.GetComponent<PlayerController>().respawnPoint;
		}
		/*오프라인일때*/
		else if (other.GetComponent<PlayerControllerOff>() != null)
		{
			other.transform.position = other.GetComponent<PlayerControllerOff>().respawnPoint;
		}
	}



	public IEnumerator DeadAnimarter(){
		GameObject.Find("ButtonJump").GetComponent<SimpleButton>().enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetBool ("IsDead", true);
		GameObject.Find (PlayerInfoManager.SelectCharacter + "Body").GetComponent<ChangeMaterial> ().ChangeLose ();  // 우는 표정으로 바꿈
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerOff> ().MOVESPD_ORIGIN = 0;
		Death = true;
		yield return new WaitForSeconds(1.5f);
		Death = false;
		GameObject.Find("ButtonJump").GetComponent<SimpleButton>().enabled = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerOff> ().MOVESPD_ORIGIN = PlayerControllerOff.DeathBeforeSpeed;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetBool ("IsDead", false);
		GameObject.Find (PlayerInfoManager.SelectCharacter + "Body").GetComponent<ChangeMaterial> ().ChangeIdle ();  // 우는 표정으로 해제

	}
}