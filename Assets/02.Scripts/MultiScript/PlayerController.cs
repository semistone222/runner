using System.Collections;
using CnControls;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public const float maxMovSpd = 60f;
    public const string poisonTag = "a";

    public float moveSpeed, jumpSpeed, gravity, jumpVal, receivedSpeed, NowSpeed;
    public GameObject moveDustParticle;
        
    private float yRotate = 0.0f;
    private CharacterController myCharacterController;
    private PhotonView myPhotonView;
    private Camera mainCamera;
    private Vector3 inputVec, moveVec, precurrPos = Vector3.zero,
    currPos = Vector3.zero, tileNormal, slidingDirection;

    private Quaternion currRot = Quaternion.identity;
    private Animator ani;
    private ChangeMaterial cm;

    private CrowdControl[] ccArray; //플레이어에게 부착된 상태이상을 체크할 배열

    private float sustainTime = 0f;   // 초기 가속 시의 경과시간.
    private const float runAniSpeed = 3.0f;   // 달리기 시 애니메이션 재생 속도.
    private const float walkAniSpeed = 1.0f;  // 걷기 시 애니메이션 재생 속도.
    private const float sustainStandardTime = 1.5f; // 초기 가속 시간
    private const float sustainStandard = 0.3f; // 초기 가속시, 초기 가속 계수.
    private const float scaleStandard = 0.5f;  // 걷기와 달리기의 구분 상수
    private const string runAniSpeedStr = "RunAniSpeed";
    private bool doingSustain = false; // 초기 가속도 bool
    private float scaleFactor = 1.0f;   //moveVec 보정치

    private string formerColName;   // 중복 충돌 막는 변수

    public Vector3 respawnPoint; /*플레이어가 죽었을때 리스폰 할 위치입니다. 디버깅 용으로 HideInInspector 해제해놨습니다.*/

    void Awake()
    {        
        myCharacterController = GetComponent<CharacterController>();
        myPhotonView = GetComponent<PhotonView>();
        ani = GetComponent<Animator>();
        cm = GetComponentInChildren<ChangeMaterial>();
        mainCamera = Camera.main;

        if (myPhotonView.isMine)
        {
            Camera.main.GetComponent<CameraController>().player = gameObject;
        }

        currPos = transform.position;
        currRot = transform.rotation;
    }
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }

    }
    
    public void JumpCheck()
    {
        if (CnInputManager.GetButtonDown("Jump"))
        {
            if (myCharacterController.isGrounded)
            {
                JumpSound();
                jumpVal = jumpSpeed;
            }
        }
        else
        {
            if (myCharacterController.isGrounded)
            {
                jumpVal = 0;
            }
        }
    }

    void JumpSound()
    {
        GetComponent<AudioSource>().Play();
    }

    void ParticleCheck(Vector3 inputVec)
    {
        // Animation 동작 부분
        if (inputVec.x == 0 && inputVec.y == 0)
        {
            moveSpeed = 0;
            ani.SetBool("IsRun", false);
            moveDustParticle.SetActive(false);
        }
        else
        {
            ani.SetBool("IsRun", true);

            if (myCharacterController.isGrounded)
            {
                moveDustParticle.SetActive(true);
            }
            else
            {
                moveDustParticle.SetActive(false);
            }
        }
    }

    IEnumerator SustainCounter()
    {
        float sustainFactor = (1f - sustainStandard) / (sustainStandardTime * 10);
        sustainTime = sustainStandard;

        while (true)
        {
            sustainTime += sustainFactor;
            yield return new WaitForSeconds(0.1f);

            if (sustainTime > 1f)
            {
                sustainTime = 1f;
                break;
            }
        }
    }

    void Update()
    {
        if (myPhotonView.isMine)
        {
            JumpCheck();
            inputVec = new Vector3(CnInputManager.GetAxis("JoyStickX"), CnInputManager.GetAxis("JoyStickY"));
            moveVec = Vector3.zero;
            ParticleCheck(inputVec);

            if (inputVec.sqrMagnitude > 0.001f)
            {
                moveVec = mainCamera.transform.TransformDirection(inputVec);
                moveVec.y = 0f;
                moveVec.Normalize();
                transform.forward = moveVec;

                if (doingSustain == false && sustainTime < 1f)
                {
                    StartCoroutine("SustainCounter");
                    doingSustain = true;
                }

                scaleFactor = inputVec.sqrMagnitude * sustainTime;

                if (scaleFactor >= scaleStandard)
                {
                    ani.SetFloat(runAniSpeedStr, runAniSpeed);
                }
                else if (scaleFactor > 0f && scaleFactor < scaleStandard)
                {
                    ani.SetFloat(runAniSpeedStr, walkAniSpeed);
                }

                NowSpeed = maxMovSpd * scaleFactor; //constant is MaxMovSpeed

                CrowdControlCheck(); // 상태 이상 체크 함수.

                moveVec *= NowSpeed;
            }
            else if (inputVec.sqrMagnitude == 0f && doingSustain)
            {
                StopCoroutine("SustainCounter");
                doingSustain = false;
                sustainTime = 0f;
            }
            // 중력과 점프
            jumpVal -= gravity * Time.deltaTime;
            moveVec.y += jumpVal;

            // 적용
            myCharacterController.Move(moveVec * Time.deltaTime);
            this.GetComponent<PhotonTransformView>().SetSynchronizedValues(speed : new Vector3(moveVec.x, 0, moveVec.z), turnSpeed : 0.0f);
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, currPos, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, currRot, Time.deltaTime * 5f);
        }
    }

    [PunRPC]
    void OnGameReady()
    {

    }

    private void CrowdControlCheck()
    {
        bool isPoison = false;
        ccArray = GetComponents<CrowdControl>();

        if (ccArray.Length != 0)
        {
            float movespdFactor = 1f;
            float jumpspdFactor = 1f;

            foreach (CrowdControl cc in ccArray)
            {
                if (cc.Tag == poisonTag)
                {
                    isPoison = true;
                }
                movespdFactor *= cc.movespdMultiplier;
                jumpspdFactor *= cc.jumpspdMultiplier;
            }
            NowSpeed = NowSpeed * movespdFactor;
            jumpSpeed = jumpSpeed * jumpspdFactor;
        }
        if (isPoison)
        {
            cm.ChangeLose();
        }
        else
        {
            cm.ChangeIdle();
        }
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*충돌 처리 함수*/
        /* for determing down or up slope */
        tileNormal = hit.normal;

        Collider col = hit.collider;

        if (col.tag == "Tile")
        {
            //중복체크
            if (col.GetComponent<Gimmick>() != null && formerColName != col.name && hit.controller.isGrounded == true)
            {
                formerColName = col.name;
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
}

