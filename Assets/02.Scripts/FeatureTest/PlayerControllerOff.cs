using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

/*
 *  PlayerControllerOff , by Jin-seok, Yu
 * 
 *  Last Update : May 19th, 2017
 * 
 *  Offline에서 개발 기능 테스트를 위해 온라인 연결 필요 없이 동작할 수 있도록 한 플레이어 컨트롤러 컴포넌트 입니다.
 
    !현재 점퍼 안 됨, 우연히 데스를 두 번 밟으면 리스폰이 안 됨
*/

public class PlayerControllerOff : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;

    /*추가한 내용*/
    [HideInInspector]
    public float MOVESPD_ORIGIN;    //플레이어의 최초 이동 속도
    [HideInInspector]
    public float JUMPSPD_ORIGIN;    //플레이어의 최초 이동 속도


    private CrowdControl[] ccArray; //플레이어에게 부착된 상태이상을 체크할 배열
                                    /// ///////////////


    private Transform myTransform;
    private CharacterController myCharacterController;
    //private PhotonView myPhotonView;
    private Camera mainCamera;
    private Vector3 inputVec;
    private Vector3 moveVec;
    private float jumpVal;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    /*추가한 내용*/
    //[HideInInspector]
    public Vector3 respawnPoint; /*플레이어가 죽었을때 리스폰 할 위치입니다. 디버깅 용으로 HideInInspector 해제해놨습니다.*/
    private bool isMoveAccel = false;
    private bool isJumpAccel = false;
    /// ///////////////


    public Animator ani;

    private string formerColName;


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
        /*추가한 내용*/
        MOVESPD_ORIGIN = moveSpeed;
        JUMPSPD_ORIGIN = jumpSpeed;
        ///////////////
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

        // Animation 동작 부분
        if (inputVec.x == 0 && inputVec.y == 0)
        {
            ani.SetBool("IsRun", false);

        }
        else
        {
            ani.SetBool("IsRun", true);
        }



        if (inputVec.sqrMagnitude > 0.001f)
        {
            moveVec = mainCamera.transform.TransformDirection(inputVec);
            moveVec.y = 0f;
            moveVec.Normalize();
            myTransform.forward = moveVec;

            if (isMoveAccel)
            {
                moveVec *= moveSpeed * inputVec.sqrMagnitude;
            }
            else
            {
                moveVec *= moveSpeed;
            }
        }

        if (myCharacterController.isGrounded)
        {
            if (CnInputManager.GetButtonDown("Jump"))
            {
                jumpVal = jumpSpeed;
            }
            else
            {
                jumpVal = 0;
            }
        }

        jumpVal -= gravity * Time.deltaTime;
        moveVec.y = jumpVal;
        myCharacterController.Move(moveVec * Time.deltaTime);
    }

    /*테스트 용으로 넣은 함수로, 온라인에선 사용할 수 없습니다.*/
    public void MoveAccelCheck()
    {
        isMoveAccel = !isMoveAccel;
    }
    public void JumpAccelCheck()
    {
        isJumpAccel = !isJumpAccel;
    }
    /// ////////////////////////////////////////////////////////////

    public void JumpCheck()
    {
        if (myCharacterController.isGrounded)
        {

            jumpVal = jumpSpeed;

        }

        jumpVal -= gravity * Time.deltaTime;
        moveVec.y = jumpVal;
        myCharacterController.Move(moveVec * Time.deltaTime);
    }

    void Update()
    {

        CrowdControlCheck();

        //For Debug
        Debug.Log(myCharacterController.isGrounded);
    }

    private void CrowdControlCheck()
    {
        ccArray = GetComponents<CrowdControl>();
        if (ccArray.Length != 0)
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider col = hit.collider;
        if (col.tag == "Tile")
        {
            if (col.GetComponent<Gimmick>() != null && formerColName != col.name && hit.controller.isGrounded == true)
            {
                formerColName = col.name;

                Debug.Log(col.name);
                col.GetComponent<Gimmick>().EnterFunc(hit.controller);
            }
        }
        else if (col.tag == "DeathTile")
        {
            formerColName = col.name;

            Debug.Log(col.name);
            col.GetComponent<Gimmick>().EnterFunc(hit.controller);
        }        
        else if (col.tag == "FinishLine")
        {
            //현재 FinishLine에선 충돌이슈가 발생하지 않으므로 무시.
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
