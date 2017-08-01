using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  GimmickInfo , by Jin-seok, Yu
 * 
 *  Last Update : May 14th, 2017
 * 
 *  각 특수 오브젝트의 기능과 관련한 컴포넌트입니다. Inspector에서 설정 가능합니다.
 *  
 *  May 14th, 2017 : isMoveWithPlayer 관련 이슈 발생으로, 현재 강제 해제 중.
 */	

public class GimmickInfo : Gimmick
{
	[Header("Tile Info")]
	[Tooltip("체크시, 오브젝트를 밟는 순간 체크포인트로 지정됩니다.")]
	public bool isCheckRespawnPoint = true; //RespawnPoint를 저장할 것인가?
	[Tooltip("체크시, 오브젝트를 밟았을 때, 지정 시간 후 사라지고, 지정 시간 후 리스폰 됩니다.")]
	public bool isVanish = false; //밟으면 사라지는 타일인가?
	[Tooltip("체크시, 오브젝트를 밟았을 때, 스프링 발판처럼 플레이어가 자동으로 점프하도록 합니다.")]
	public bool isJumper = false; //밟으면 점프되는 타일인가?
	[Tooltip("계수만큼 자동 점프시 점프력이 강화됩니다.")]
	public float jumperMultiplier = 1.0f;  //점프력 조절
	[Tooltip("사라지는 오브젝트의 사라지기 까지의 시간입니다.")]
	public float timeVanishing = 3f; //타일 소멸 시간 (사라지는 타일일 경우)
	[Tooltip("사라지는 오브젝트의 리스폰 까지의 시간입니다.")]
	public float timeRespawning = 2f;//타일 재생 시간 (사라지는 타일일 경우)
	private bool nowVanishing = false;
	[Tooltip("체크시, 이동/회전하는 오브젝트가 플레이어와 같이 이동/회전 합니다.")]
	public bool isMoveWithPlayers = false; //움직이는 타일일경우, 플레이어와 같이 이동할 것인가?


	CrowdControl[] ccArray;
	CrowdControl cc;

	[Header("Crowd Control Info")]
	[Tooltip("상태이상의 태그입니다.")]
	public string Tag;
	[Tooltip("상태이상의 이동속도 조정계수 입니다.")]
	public float movespeedMultiplier;
	[Tooltip("상태이상의 점프량 조정계수 입니다.")]
	public float jumpspeedMultiplier;
	[Tooltip("상태이상의 지속시간 입니다.")]
	public float durationTime;
	[Tooltip("상태이상의 파괴 여부 입니다.")]
	public bool isDestroyable;

	private Vector3 respawnPoint; //이전 리스폰 포인트를 저장할 변수
	private bool hasTag = false;


	private void Awake()
	{
		DoNotCheckRespawnIfVanish(); /*isVanish가 체크된 타일인 경우, 강제로 isCheckRespawnPoint 체크 해제*/

		isMoveWithPlayers = false; // 이슈 발생하여 강제 해제
	}

	private void DoNotCheckRespawnIfVanish()
	{
		if (isVanish)
		{
			if (isCheckRespawnPoint)
			{
				Debug.Log("Warning: [" + transform.name + "] has been checked to set respawn point \n while it has been checked vanishing, so it has automatically removed");
				isCheckRespawnPoint = false;
			}
		}
	}

	private bool CheckPlayer(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			return true;
		}
		return false;
	}

	public override void EnterFunc(Collider other)
	{
		/*사용자에게 상태이상을 거는 장애물*/
		hasTag = false;
		ccArray = other.GetComponents<CrowdControl>();
		foreach (CrowdControl c in ccArray)
		{/*현재 사용자에게 걸려있는 상태이상을 불러옴*/
			if (string.Equals(c.Tag, Tag))
			{
                cc = c;
				hasTag = true;
				break;
			}
		}

		if (hasTag)
		{/*만약 같은 상태이상을 또 걸 경우, 남은 시간을 초기화*/
			cc.currentTime = 0f;
		}
		else
		{/*만약 걸리지 않은 상태이상일 경우, 상태이상을 건다*/
			if (Tag != "")
			{
				cc = other.gameObject.AddComponent<CrowdControl>();
				cc.Tag = Tag;
				cc.movespdMultiplier = movespeedMultiplier;
				cc.jumpspdMultiplier = jumpspeedMultiplier;
				cc.durationTime = durationTime;
			}
		}
		/////////////////////////////////////////////////

		if (isDestroyable)
		{/*1회용 오브젝트인 경우, 상호작용 후 파괴*/
			Destroy(this.gameObject);
		}

		if (isVanish && !nowVanishing)
		{/*사라졌다 생성되는 오브젝트 인 경우*/
			nowVanishing = true;
			Invoke("VanishTile", timeVanishing);
			Invoke("RespawnTile", timeVanishing + timeRespawning);
		}


		if (isCheckRespawnPoint)
		{//리스폰 포인트인 오브젝트 인 경우
			SetRespawnPoint(other);
		}



		if (isJumper)
		{
			/*Player면*/
			if (CheckPlayer(other))
			{
				/*온라인일때*/
				if (other.GetComponent<PlayerController>() != null)
				{
					//other.GetComponent<PlayerController>().JumpCheck();
					//other.GetComponent<PlayerController>().jumpSpeed = other.GetComponent<PlayerController>().JUMPSPD_ORIGIN * jumperMultiplier;
				}
				/*오프라인일때*/
				else if (other.GetComponent<PlayerControllerOff>() != null)
				{
					other.GetComponent<PlayerControllerOff>().jumpSpeed = other.GetComponent<PlayerControllerOff>().JUMPSPD_ORIGIN * jumperMultiplier;
					other.GetComponent<PlayerControllerOff>().JumpCheck();
				}
			}
		}

		if (isMoveWithPlayers)
		{/*플레이어와 함께 움직이는 오브젝트 인경우*/
			SetPlayerParent(other, this.gameObject.transform);
		}

	}

	public override void StayFunc(Collider other)
	{
		// if (isCheckRespawnPoint)
		//{//리스폰 포인트인 오브젝트 인 경우
		//     SetRespawnPoint(other);
		// }
	}

	public override void ExitFunc(Collider other)
	{
		if (isJumper)
		{
			/*Player면*/
			if (CheckPlayer(other))
			{
				/*온라인일때*/
				if (other.GetComponent<PlayerController>() != null)
				{
					//other.GetComponent<PlayerController>().jumpSpeed = other.GetComponent<PlayerController>().JUMPSPD_ORIGIN / jumperMultiplier;
				}
				/*오프라인일때*/
				else if (other.GetComponent<PlayerControllerOff>() != null)
				{
					other.GetComponent<PlayerControllerOff>().jumpSpeed = other.GetComponent<PlayerControllerOff>().JUMPSPD_ORIGIN / jumperMultiplier;
				}
			}
		}
		if (isMoveWithPlayers)
		{/*플레이어와 함께 움직이는 오브젝트 인경우*/
			SetPlayerParent(other, null);
		}
	}

	private void SetRespawnPoint(Collider other)
	{/*부딪힌 플레이어를 호출, respawnPoint를 현재 타일의 중앙좌표로 설정합니다.*/

		/*Player면*/
		if (CheckPlayer(other))
		{
			if (other.GetComponent<CharacterController>().isGrounded == false)
			{/*땅을 밟고 있을 때만 저장*/
				return;
			}

			float xSin = Mathf.Sin(transform.rotation.x  * 4); //왜 *4인지 1도 모르겠음...
			float yFactor = Mathf.Pow((transform.lossyScale.y / 2), 2);
			float zFactor = Mathf.Pow((transform.lossyScale.z / 2) * xSin, 2);


			//밟은 오브젝트의 중앙좌표의 높이와, 밟은 순간 플레이어의 y 위치중에 큰 것 + 3f
			float xPos = transform.position.x;
			float yPos = Mathf.Max(transform.position.y + Mathf.Sqrt(yFactor + zFactor), other.transform.position.y) + 0.5f;
			float zPos = transform.position.z;

			respawnPoint = new Vector3(xPos, yPos, zPos);

			/*온라인일때*/
			if (other.GetComponent<PlayerController>() != null)
			{
				Vector3 formerRespawnPoint = other.GetComponent<PlayerController>().respawnPoint;

				//  if (xPos != formerRespawnPoint.x
				//      && zPos != formerRespawnPoint.z)
				// {
				other.GetComponent<PlayerController>().respawnPoint = respawnPoint;
				//  }
			}
			/*오프라인일때*/
			else if (other.GetComponent<PlayerControllerOff>() != null)
			{
				Vector3 formerRespawnPoint = other.GetComponent<PlayerControllerOff>().respawnPoint;

				//  if (xPos != formerRespawnPoint.x
				//     && zPos != formerRespawnPoint.z)
				//   {
				other.GetComponent<PlayerControllerOff>().respawnPoint = respawnPoint;
                //   }
			}

		}
	}


	private void VanishTile()
	{
		this.gameObject.SetActive(false);
		nowVanishing = false;
	}
	private void RespawnTile()
	{
		this.gameObject.SetActive(true);
	}

	private void SetPlayerParent(Collider other, Transform tf)
	{
		/*Player면*/
		if (CheckPlayer(other))
		{
			other.transform.SetParent(tf);
		}
	}
}
