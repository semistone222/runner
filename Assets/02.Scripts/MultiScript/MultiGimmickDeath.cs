using System.Collections;
using UnityEngine;
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
    SimpleButton simpleButton;
    GameObject player;
    Animator playerAnimator;
    ChangeMaterial playerCm;
    PlayerController playerController;
    public static bool Death;

    void Start()
    {
        simpleButton = GameObject.Find("ButtonJump").GetComponent<SimpleButton>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerCm = player.GetComponentInChildren<ChangeMaterial>();
        playerController = player.GetComponent<PlayerController>();
        Death = false;
    }

    public override void EnterFunc(Collider other)
    {
        StartCoroutine("DeadAnimarter");
        Respawn(other);

        PhotonView photonView = other.GetComponent<PhotonView>();
        photonView.RPC("OnCryAnim", PhotonTargets.Others);

        SoundManager sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        DeathRetry.DeathRetryPopupAgain = false;

        sm.SetBGMNull();
        sm.SetBGM1();
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
            other.transform.position = other.GetComponent<PlayerController>().respawnPoint;
        }
        /*오프라인일때*/
        else if (other.GetComponent<PlayerControllerOff>() != null)
        {
            //other.transform.position = other.GetComponent<PlayerControllerOff>().respawnPoint;
        }
    }

    public IEnumerator DeadAnimarter()
    {
        simpleButton.enabled = false;
        playerAnimator.SetBool("IsDead", true);
        playerCm.ChangeLose();
        playerController.moveSpeed = 0f;//!

        Death = true;

        yield return new WaitForSeconds(1.5f);

        Death = false;

        simpleButton.enabled = true;
        playerAnimator.SetBool("IsDead", false);
        //playerController.moveSpeed = 45f;//!
        playerCm.ChangeIdle();
    }
}