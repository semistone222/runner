using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  TileDeath , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  떨어지면 사망 판정되어, 다시 Respawn 시키는 타일입니다.
 */

public class TileDeath : Tile
{
    protected override void EnterFunc(Collider other)
    {
        BeforeRespawn();
        Respawn(other);
    }
    private void BeforeRespawn()
    {
        /*Respawn 전에 호출될 함수입니다. 애니메이션, 타이머 등이 들어갈 것으로 예상합니다.*/
    }
    private void Respawn(Collider other)
    {   /*Respawn 함수입니다. 현재는 부딪힌 플레이어에 저장된 respawnPoint로 위치를 강제 변경합니다.*/
        other.transform.position = other.GetComponent<PlayerControllerOff>().respawnPoint;
    }

}
