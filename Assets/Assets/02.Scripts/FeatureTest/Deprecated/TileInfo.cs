using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  TileInfo , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  각 타일의 기능과 관련한 컴포넌트입니다. Inspector에서 설정 가능합니다.
 */

public class TileInfo : Tile
{
    public bool isCheckRespawnPoint = true; //RespawnPoint를 저장할 것인가?

    public bool isVanish = false; //밟으면 사라지는 타일인가?
    public bool isJump = false; //밟으면 점프되는 타일인가?
    public float jumpspdMultiplier = 1.0f;  //점프력 조절
    public float timeVanishing = 3f; //타일 소멸 시간 (사라지는 타일일 경우)
    public float timeRespawning = 2f;//타일 재생 시간 (사라지는 타일일 경우)
    private bool nowVanishing = false;

    public bool isMoveWithPlayers = false; //움직이는 타일일경우, 플레이어와 같이 이동할 것인가?

    private void Awake()
    {
        DoNotCheckRespawnIfVanish(); /*isVanish가 체크된 타일인 경우, 강제로 isCheckRespawnPoint 체크 해제*/
    }

    private void DoNotCheckRespawnIfVanish()
    {
        if(isVanish)
        {
            if(isCheckRespawnPoint)
            {
                Debug.Log("Warning: [" + transform.name + "] has been checked to set respawn point \n while it has been checked vanishing, so it has automatically removed");
                isCheckRespawnPoint = false;
            }
        }
    }
    
    protected override void EnterFunc(Collider other)
    {
        if(isJump)
        {
            /*Player면*/
            other.GetComponent<PlayerControllerOff>().JumpCheck();
            other.GetComponent<PlayerControllerOff>().jumpSpeed = other.GetComponent<PlayerControllerOff>().JUMPSPD_ORIGIN * jumpspdMultiplier;
        }
        if (isVanish && !nowVanishing)
        {
            nowVanishing = true;
            Invoke("VanishTile", timeVanishing);
            Invoke("RespawnTile", timeVanishing + timeRespawning);
        }
        if (isCheckRespawnPoint)
        {
            SetRespawnPoint(other);
        }
        if (isMoveWithPlayers)
        {
            SetPlayerParent(other, this.transform);
        }
    }
    protected override void ExitFunc(Collider other)
    {
        if (isJump)
        {
            /*Player면*/
            other.GetComponent<PlayerControllerOff>().jumpSpeed = other.GetComponent<PlayerControllerOff>().JUMPSPD_ORIGIN / jumpspdMultiplier;
        }
        if (isMoveWithPlayers)
        {
            SetPlayerParent(other, null);
        }
    }

    private void SetRespawnPoint(Collider other)
    {/*부딪힌 플레이어를 호출, respawnPoint를 현재 타일의 중앙좌표로 설정합니다.*/
        other.GetComponent<PlayerControllerOff>().respawnPoint = transform.position;
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
        //Player Tag 인경우에만 실행하게 바꿔야합니다. if(other.tag == "Player") 
        //{
        other.transform.SetParent(tf);
        //}
    }
}

/*

    protected override void EnterFunc(Collider other)
    {
        other.GetComponent<PlayerControllerOff>().moveSpeed *= MoveSpdMultiplier;
        other.GetComponent<PlayerControllerOff>().jumpSpeed *= JumpSpdMultiplier;
        if(isJumper)
        {
            other.GetComponent<PlayerControllerOff>().JumpCheck();
        }
    }
    protected override void ExitFunc(Collider other)
    {
        other.GetComponent<PlayerControllerOff>().moveSpeed /= MoveSpdMultiplier;
        other.GetComponent<PlayerControllerOff>().jumpSpeed /= JumpSpdMultiplier;
    }

*/