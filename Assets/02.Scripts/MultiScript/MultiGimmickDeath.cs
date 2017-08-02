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



public class MultiGimmickDeath : Gimmick
{
	public static bool Death = false;

	void Start(){
		Death = false;
	}

	public override void EnterFunc(Collider other)
	{


		StartCoroutine ("DeadAnimarter");
		Respawn(other);

	 	SoundManager sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

		DeathRetry.DeathRetryPopupAgain = false;

        sm.SetBGMNull();
        sm.SetBGM1();


	}


	//	GameObject.Find ("JoyStick").GetComponent<SimpleJoystick> ().OnPointerUp (PointerEventData);
	//	BeforeRespawn();


	private void BeforeRespawn()
	{
		/*Respawn 전에 호출될 함수입니다. 애니메이션, 타이머 등이 들어갈 것으로 예상합니다.*/
	}
	private void Respawn(Collider other)
	{   /*Respawn 함수입니다. 현재는 부딪힌 플레이어에 저장된 respawnPoint로 위치를 강제 변경합니다.*/

		/*온라인일때*/
		if (other.GetComponent<PlayerController>() != null)
		{
			other.transform.position = other.GetComponent<PlayerController>().respawnPoint;
		}
		/*오프라인일때*/
		else if (other.GetComponent<PlayerControllerOff>() != null)
		{
			//other.transform.position = other.GetComponent<PlayerControllerOff>().respawnPoint;
		}
	}



	public IEnumerator DeadAnimarter(){
		GameObject.Find("ButtonJump").GetComponent<SimpleButton>().enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetBool ("IsDead", true);
		GameObject.Find (PlayerInfoManager.SelectCharacter + "Body").GetComponent<ChangeMaterial> ().ChangeLose ();  // 우는 표정으로 바꿈
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().moveSpeed = 0f;//!
		Death = true;
		yield return new WaitForSeconds(1.5f);
		Death = false;
		GameObject.Find("ButtonJump").GetComponent<SimpleButton>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed = 45f;//!
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetBool ("IsDead", false);
		GameObject.Find (PlayerInfoManager.SelectCharacter + "Body").GetComponent<ChangeMaterial> ().ChangeIdle ();  // 우는 표정으로 해제

	}
}