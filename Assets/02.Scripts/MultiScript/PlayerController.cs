using System.Collections;
using CnControls;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpSpeed, gravity, jumpVal, receivedSpeed, NowSpeed;
    public GameObject moveDustParticle;
    public TrailRenderer Booster;

    private GameObject ButtonBooster;
    private float BoosterCheckSped = 0;
    private float receivedBoosterSpeed;

    private Transform myTransform;
    private CharacterController myCharacterController;
    private PhotonView myPhotonView;
    private Camera mainCamera;
    private Vector3 inputVec, moveVec, precurrPos = Vector3.zero,
    currPos = Vector3.zero, tileNormal, slidingDirection;

    private Quaternion currRot = Quaternion.identity;
    private Animator ani;
    private ChangeMaterial cm;

    private GameObject StartText, StartBoundary, ReadyLine;

    Coroutine Starttimer, Stoptimer;

    private CrowdControl[] ccArray; //플레이어에게 부착된 상태이상을 체크할 배열
    private bool isSliding, isMoveAccel = false, isJumpAccel = false, isGround = false;

    private float sustainTime = 0f;   // 초기 가속 시의 경과시간.
    private const float runAniSpeed = 3.0f;   // 달리기 시 애니메이션 재생 속도.
    private const float walkAniSpeed = 1.0f;  // 걷기 시 애니메이션 재생 속도.
    private const float sustainStandardTime = 1.5f; // 초기 가속 시간
    private const float sustainStandard = 0.3f; // 초기 가속시, 초기 가속 계수.
    private const float scaleStandard = 0.5f;  // 걷기와 달리기의 구분 상수
    private const string runAniSpeedStr = "RunAniSpeed";
    private bool doingSustain = false; // 초기 가속도 bool

    private string formerColName;   // 중복 충돌 막는 변수

    public Vector3 respawnPoint; /*플레이어가 죽었을때 리스폰 할 위치입니다. 디버깅 용으로 HideInInspector 해제해놨습니다.*/

    void Awake()
    {
        StartText = GameObject.FindGameObjectWithTag("StartText");
        StartBoundary = GameObject.FindGameObjectWithTag("StartBoundary");
        ReadyLine = GameObject.FindGameObjectWithTag("StartZone");

        myTransform = GetComponent<Transform>();
        myCharacterController = GetComponent<CharacterController>();
        myPhotonView = GetComponent<PhotonView>();
        ani = GetComponent<Animator>();
        cm = GetComponent<ChangeMaterial>();
        mainCamera = Camera.main;

        if (myPhotonView.isMine)
        {
            Camera.main.GetComponent<CameraController>().player = gameObject;
        }

        currPos = myTransform.position;
        currRot = myTransform.rotation;

        ButtonBooster = GameObject.Find("ButtonBooster");

        myPhotonView.RPC("OnGameReady", PhotonTargets.MasterClient, null);

        //		StartText = GetComponent<Text> ();
    }

    //void FixedUpdate()
    //{

    //    if (!myPhotonView.isMine)
    //        return;

    //}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(myTransform.position);
            stream.SendNext(myTransform.rotation);
            stream.SendNext(moveSpeed);
            stream.SendNext(BoosterCheckSped);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
            receivedSpeed = (float)stream.ReceiveNext();
            receivedBoosterSpeed = (float)stream.ReceiveNext();

            if (precurrPos.x == currPos.x && precurrPos.z == currPos.z && receivedSpeed == 0)
            {
                ani.SetBool("IsRun", false);
                moveDustParticle.SetActive(false);
            }
            else if (receivedSpeed != 0)
            {
                ani.SetBool("IsRun", true);
                moveDustParticle.SetActive(true);
            }

            precurrPos = currPos;

            if (receivedBoosterSpeed == 0)
            {
                Booster.enabled = false;
            }
            else if (receivedBoosterSpeed == 10.8)
            {
                Booster.enabled = true;
            }
        }
    }

    public void JumpCheck()
    {
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
    }

    void FixedUpdate()
    {
        BoosterCheckSped = ButtonBooster.GetComponent<MultiBoosterButton>().AddBoosterSpeed;  // 부스터 효과체크를 위한 변수 

        if (myPhotonView.isMine)
        {
            if (CnInputManager.GetButtonDown("Jump"))
            {
                if (myCharacterController.isGrounded)
                {
                    //	JumpingSound();
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

            CrowdControlCheck(); // 상태 이상 체크 함수.
            inputVec = new Vector3(CnInputManager.GetAxis("JoyStickX"), CnInputManager.GetAxis("JoyStickY"));
            moveVec = Vector3.zero;

            // Animation 동작 부분
            if (inputVec.x == 0 && inputVec.y == 0)
            {
                moveSpeed = 0;
                ani.SetBool("IsRun", false);
                moveDustParticle.SetActive(false);
            }
            else
            {
                moveSpeed = NowSpeed;
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

            if (inputVec.sqrMagnitude > 0.001f && MultiGimmickDeath.Death == false)
            {
                float scaleFactor = 1.0f;   //moveVec 보정치
                moveVec = mainCamera.transform.TransformDirection(inputVec);
                moveVec.y = 0f;
                moveVec.Normalize();
                myTransform.forward = moveVec;

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

                NowSpeed = 100 * scaleFactor; //constant is MaxMovSpeed
                moveVec *= NowSpeed;
            }
            else if (inputVec.sqrMagnitude == 0f && doingSustain)
            {
                StopCoroutine("SustainCounter");
                doingSustain = false;
                sustainTime = 0f;
            }

            // 내리막길일때 표면따라서 움직이게
            if (Vector3.Dot(moveVec, tileNormal) > 0)
            {
                Vector3 temp = Vector3.Cross(tileNormal, moveVec);
                moveVec = Vector3.Cross(temp, tileNormal);
            }

            // 중력과 점프
            jumpVal -= gravity * Time.deltaTime;
            moveVec.y += jumpVal;

            // 경사면 슬라이딩
            //	moveVec += slidingDirection;

            // 적용
            myCharacterController.Move(moveVec * Time.deltaTime);
        }
        else
        {
            myTransform.position = Vector3.Lerp(myTransform.position, currPos, Time.deltaTime * 8.5f);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, currRot, Time.deltaTime * 8.5f);
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
            NowSpeed = NowSpeed * movespdFactor;
            jumpSpeed = jumpSpeed * jumpspdFactor;
        }
        //else
        //{
        //    NowSpeed = NowSpeed;
        //    jumpSpeed = jumpSpeed;
        //}
    }

    IEnumerator StartGame()
    {
        int time = 0;

        while (true)
        {
            if (time <= 3)
                time++;

            StartText.GetComponent<Text>().text = "" + time;

            if (time > 3 && time <= 5)
            {
                time++;
                StartText.GetComponent<Text>().text = "Start!!";
            }

            if (time > 5)
            {
                StartText.GetComponent<Text>().text = "";
                StartBoundary.SetActive(false);
                StartText.SetActive(false);
                ReadyLine.SetActive(false);
                break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log("ininiininininininininininininininin");
    //    switch (other.transform.tag)
    //    {
    //        case "StartZone":
    //            myPhotonView.RPC("OnGameReady", PhotonTargets.MasterClient, null);
    //            break;
    //        case "EndZone":
    //            myPhotonView.RPC("OnGameEnd", PhotonTargets.MasterClient, myTransform.name);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    switch (other.transform.tag)
    //    {
    //        case "StartZone":
    //            myPhotonView.RPC("OnReadyCancel", PhotonTargets.MasterClient, null);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    [PunRPC]
    void OnGameReady()
    {
        Debug.Log("PhotonNetwork.room.PlayerCount : " + PhotonNetwork.room.PlayerCount);

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            myPhotonView.RPC("OnGameStart", PhotonTargets.AllViaServer, null);
        }
    }

    //[PunRPC]
    //void StopStartgame()
    //{
    //    //StopCoroutine (Starttimer);
    //    StartText.GetComponent<Text>().text = "";
    //}

    [PunRPC]
    void OnGameStart()
    {
        GameObject.Find("StartLoadingManager").GetComponent<MultiStartLaoding>().enabled = true;
        Debug.Log("OnGameStart");
        //Starttimer = StartCoroutine (StartGame());
    }

    [PunRPC]
    void OnGameEnd(string name)
    {
        Debug.Log("OnGameEnd(1) : " + name);

        StartCoroutine(GameEndLogic());
    }

    IEnumerator GameEndLogic()
    {
        Debug.Log("OnGameEnd(3) : " + name);

        yield return new WaitForSeconds(3.0f);

        Debug.Log("OnGameEnd(2) : " + name);
        PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
    void OnCryAnim()
    {
        StartCoroutine(CryAnim());
    }

    IEnumerator CryAnim()
    {
        ani.SetBool("IsDead", true);
        GetComponentInChildren<ChangeMaterial>().ChangeLose();

        yield return new WaitForSeconds(1.5f);

        ani.SetBool("IsDead", false);
        GetComponentInChildren<ChangeMaterial>().ChangeIdle();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*충돌 처리 함수*/
        /* for determing down or up slope */
        tileNormal = hit.normal;

        /* sliding on slope */
        if (tileNormal != Vector3.up)
        {
            isSliding = true;

            Vector3 c = Vector3.Cross(Vector3.up, tileNormal);
            Vector3 u = Vector3.Cross(c, tileNormal);
            slidingDirection = u * 10;
        }
        else
        {
            isSliding = false;
            slidingDirection = Vector3.zero;
        }

        Collider col = hit.collider;

        if (col.tag == "Tile")
        {
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