using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class InitServer : MonoBehaviour
{

    public string version = "v1.0";
    public string sceneName = "3.Loading";
    public Button runButton;
    public Text playersText;

    private string USER_ID = "USER_ID";
    private string USER_PREFIX = "USER_";
    private string ROOM_PREFIX = "ROOM_";
    private string roomName;

    public byte MAX_PLAYER = 2;

    void Awake()
    {
        CheckConnection();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        CheckConnection();
    }

    void CheckConnection()
    {
        if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        {
            if (runButton != null)
            {
                runButton.interactable = false;
            }
            PhotonNetwork.ConnectUsingSettings(version);
            roomName = GetRoomName();
        }
    }

    string GetRoomName()
    {
        Random.InitState(System.Environment.TickCount);
        string rn = ROOM_PREFIX + Random.Range(0, 999);
        return rn;
    }

    void OnJoinedLobby()
    {
        runButton.interactable = true;
        Debug.Log("OnJoinedLobby !");
    }

    public void OnClickJoinRandomRoom()
    {
        string _userId = GetUserId();

        PhotonNetwork.player.NickName = _userId;
        //PlayerPrefs.SetString(USER_ID, _userId);
        PhotonNetwork.JoinRandomRoom();
    }

    string GetUserId()
    {
        return PlayerInfo.playerName;
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms !");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = MAX_PLAYER;

        //PhotonNetwork.CreateRoom("MyRoom", roomOptions, TypedLobby.Default);
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnPhotonCreateRoomFailed(object[] error)
    {
        Debug.Log("OnPhotonCreateRoomFailed");
        Debug.Log(error[0].ToString()); // error code
        Debug.Log(error[1].ToString()); // error message
    }

    void OnJoinedRoom()
    {
        Debug.Log("Entered Room");
        Debug.Log(PhotonNetwork.room);
        GetConnectPlayerCount();
        //StartCoroutine(this.LoadtoMap());
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.room;

        playersText.text = currRoom.PlayerCount.ToString() + "/" + currRoom.MaxPlayers.ToString();
    }

    [PunRPC]
    public void OnClickQuickStartButton()
    {
        GetComponent<PhotonView>().RPC("LoadRoom", PhotonTargets.AllViaServer, null);
    }

    [PunRPC]
    public void LoadRoom()
    {
        //Room닫기();
        StartCoroutine(this.LoadtoMap());
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected: " + newPlayer);
        Debug.Log(newPlayer.ToStringFull());
        GetConnectPlayerCount();
    }

    void OnPhotonPlayerDisConnected(PhotonPlayer outPlayer)
    {
        Debug.Log("OnPhotonPlayerDisconnected: " + outPlayer);
        Debug.Log(outPlayer.ToStringFull());
        GetConnectPlayerCount();
    }

    public void OnClickLobbyExitButton()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        playersText.text = "...";
    }

    IEnumerator LoadtoMap()
    {
        PhotonNetwork.isMessageQueueRunning = false; // stop networking until loading scene

        //SceneManager.LoadScene ("Ch.1_Stage1Multi");
        SceneManager.LoadScene(sceneName);
        yield return null;
    }



    /*	IEnumerator LoadGround() {
            PhotonNetwork.isMessageQueueRunning = false; // stop networking until loading scene

            SceneManager.LoadScene ("Ground");
            yield return null;
        }*/

    /*
    public void OnClickCreateRoom(string sceneName)
    {
        this.sceneName = sceneName;
        string _userId = GetUserId();
        string _roomName = roomName;

        if (string.IsNullOrEmpty(_roomName))
        {
            _roomName = GetRoomName();
        }

        PhotonNetwork.player.NickName = _userId;
        PlayerPrefs.SetString(USER_ID, _userId);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = MAX_PLAYER;

        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }
    void OnReceivedRoomListUpdate()
    {

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        int rowCount = 0;
        scrollContents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        foreach (RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            GameObject room = (GameObject)Instantiate(roomItem);
            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;

            roomData.DispRoomData();

            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                OnClickRoomItem(roomData.roomName);
            });

            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
        }
    }

    void OnClickRoomItem(string roomName)
    {
        string _userId = GetUserId();

        PhotonNetwork.player.NickName = _userId;
        PlayerPrefs.SetString(USER_ID, _userId);
        PhotonNetwork.JoinRoom(roomName);

    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    */

    // TODO : PeerCreated에서 멈춘다. autoJoinLobby 이것 때문인가...
    // autoJoinLobby를 쓰지 않으면 방 목록을 불러 올 수 없는데...
    // when autoJoinLobby == false...
    void OnConnectedToMaster()
    {
        Debug.Log("Entered Master !");
    }

}



