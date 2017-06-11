using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 *  GimmickDeath , by Jin-seok, Yu
 * 
 *  Last Update : Apr 29th, 2017
 * 
 *  떨어지면 사망 판정되어, 다시 Respawn 시키는 오브젝트입니다.
 */

public class GimmickDeath : Gimmick
{
	public static int LifeCount = 2;
	private  GameObject Life1;
	private  GameObject Life2;
	private  GameObject Life3;


	void Start(){

	//	life1 = GameObject.FindGameObjectsWithTag ("LifeImage");
		Life1 = GameObject.Find ("Life1");
		Life2 = GameObject.Find ("Life2");
		Life3 = GameObject.Find ("Life3");

		if (LifeCount == 2) {
			Life3.SetActive (false);
		}
	}

	public override void EnterFunc(Collider other)
	{

		LifeCount--;
		if (LifeCount == 2) {
			Life3.SetActive (false);
		} else if (LifeCount == 1) {
			Life2.SetActive (false);
		} else {
			Life1.SetActive (false);
		}
	
		BeforeRespawn();
		Respawn(other);
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
}