using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

/*
 *  PlayerControllerOff , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  Offline에서 개발 기능 테스트를 위해 온라인 연결 필요 없이 동작할 수 있도록 한 플레이어 컨트롤러 컴포넌트 입니다.
 */

public class PlayerControllerOff : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;

    [HideInInspector]
    public float MOVESPD_ORIGIN;    //플레이어의 최초 이동 속도
    [HideInInspector]
    public float JUMPSPD_ORIGIN;    //플레이어의 최초 이동 속도

    private CrowdControl[] ccArray; //플레이어에게 부착된 상태이상을 체크할 배열

    private Transform myTransform;
    private CharacterController myCharacterController;
    //private PhotonView myPhotonView;
    private Camera mainCamera;
    private Vector3 inputVec;
    private Vector3 moveVec;
    private float jumpVal;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    //[HideInInspector]
    public Vector3 respawnPoint; /*플레이어가 죽었을때 리스폰 할 위치입니다. 디버깅 용으로 HideInInspector 해제해놨습니다.*/

    void Awake()
    {
        myTransform = GetComponent<Transform>();
        myCharacterController = GetComponent<CharacterController>();
        //myPhotonView = GetComponent<PhotonView>();
        mainCamera = Camera.main;

        //if (myPhotonView.isMine)
        //{
        Camera.main.GetComponent<CameraController>().player = this.gameObject;
        //}

        MOVESPD_ORIGIN = moveSpeed;
        JUMPSPD_ORIGIN = jumpSpeed;

        currPos = myTransform.position;
        currRot = myTransform.rotation;

        respawnPoint = currPos;
    }

    void FixedUpdate()
    {
        /*
            if (!myPhotonView.isMine)
                return;
                */
        inputVec = new Vector3(CnInputManager.GetAxis("JoyStickX"), CnInputManager.GetAxis("JoyStickY"));
        moveVec = Vector3.zero;

        if (inputVec.sqrMagnitude > 0.001f)
        {
            moveVec = mainCamera.transform.TransformDirection(inputVec);
            moveVec.y = 0f;
            moveVec.Normalize();
            myTransform.forward = moveVec;
            moveVec *= moveSpeed;
        }

        if (CnInputManager.GetButtonDown("Jump"))
        {
            JumpCheck();
        }

        jumpVal -= gravity * Time.deltaTime;
        moveVec.y = jumpVal;
        myCharacterController.Move(moveVec * Time.deltaTime);
    }

    public void JumpCheck()
    {
        if (myCharacterController.isGrounded)
        {
            jumpVal = jumpSpeed;
        }
    }

    void Update()
    {
        ccArray = GetComponents<CrowdControl>();
        if(ccArray.Length != 0)
        {
            float movespdFactor = 1f;
            float jumpspdFactor = 1f;
            foreach (CrowdControl cc in ccArray)
            {
                movespdFactor *= cc.movespdMultiplier;
                jumpspdFactor *= cc.jumpspdMultiplier;
            }
            moveSpeed = MOVESPD_ORIGIN * movespdFactor;
            jumpSpeed = JUMPSPD_ORIGIN * jumpspdFactor;
        }
        else
        {
            moveSpeed = MOVESPD_ORIGIN;
            jumpSpeed = JUMPSPD_ORIGIN;
        }
    }
    /*
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(myTransform.position);
            stream.SendNext(myTransform.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
    */
    /*
    void Update()
    {
        
        if (myPhotonView.isMine)
        {

        }
        
        else
        {
        myTransform.position = Vector3.Lerp(myTransform.position, currPos, Time.deltaTime * 3.0f);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, currRot, Time.deltaTime * 3.0f);
        }
    }
    */
}
