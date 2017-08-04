using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ServerInit : MonoBehaviour
{
    public string version = "v1.0";
    public InputField userId, roomName;
    public GameObject scrollContents, roomItem;
    public string sceneName;

    private string USER_ID = "USER_ID";
    private string USER_PREFIX = "USER_";
    private string ROOM_PREFIX = "ROOM_";

    private static ServerInit instance;

    public static ServerInit GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ServerInit>();

            if (instance == null)
            {
                GameObject container = new GameObject("ServerInit");

                instance = container.AddComponent<ServerInit>();
            }
        }

        return instance;
    }

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
        
        PhotonNetwork.networkingPeer.DebugOut = ExitGames.Client.Photon.DebugLevel.ERROR;
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;

        userId.text = GetUserId();
        roomName.text = GetRoomName();
    }

    //void Start()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    //void OnJoinedLobby()
    //{
    //    Debug.Log("OnJoinedLobby !");
    //}

    // TODO : PeerCreated에서 멈춘다. autoJoinLobby 이것 때문인가...
    // autoJoinLobby를 쓰지 않으면 방 목록을 불러 올 수 없는데...
    // when autoJoinLobby == false...
    void OnConnectedToMaster()
    {
        Debug.Log("Entered Master !");
    }

    string GetUserId()
    {
        string uid = PlayerPrefs.GetString(USER_ID);

        if (string.IsNullOrEmpty(uid))
        {
            Random.InitState(System.Environment.TickCount);
            uid = USER_PREFIX + Random.Range(0, 999);
        }

        return uid;
    }

    string GetRoomName()
    {
        Random.InitState(System.Environment.TickCount);

        return ROOM_PREFIX + Random.Range(0, 999);
    }

    void OnPhotonRandomJoinFailed()
    {
        //Debug.Log("No Rooms !");

        PhotonNetwork.CreateRoom("MyRoom", new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 4 }, TypedLobby.Default);
    }

    void OnPhotonCreateRoomFailed(object[] error)
    {
        Debug.Log("OnPhotonCreateRoomFailed");
        Debug.Log(error[0].ToString()); // error code
        Debug.Log(error[1].ToString()); // error message
    }

    void OnJoinedRoom()
    {
        StartCoroutine(LoadtoMap());        
    }

    /*	
     *	IEnumerator LoadGround() {
            PhotonNetwork.isMessageQueueRunning = false; // stop networking until loading scene

            SceneManager.LoadScene ("Ground");
            yield return null;
        }
    */

    IEnumerator LoadtoMap()
    {
        PhotonNetwork.isMessageQueueRunning = false; // stop networking until loading scene

        //SceneManager.LoadScene ("Ch.1_Stage1Multi");
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    public void OnClickJoinRandomRoom()
    {
        string _userId = userId.text;

        if (string.IsNullOrEmpty(_userId))
        {
            _userId = GetUserId();
        }

        PhotonNetwork.player.NickName = _userId;
        PlayerPrefs.SetString(USER_ID, _userId);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnClickCreateRoom(string sceneName)
    {
        this.sceneName = sceneName;
        string _userId = userId.text;
        string _roomName = roomName.text;

        if (string.IsNullOrEmpty(_userId))
        {
            _userId = GetUserId();
        }

        if (string.IsNullOrEmpty(_roomName))
        {
            _roomName = GetRoomName();
        }

        PhotonNetwork.player.NickName = _userId;
        PlayerPrefs.SetString(USER_ID, _userId);

        PhotonNetwork.CreateRoom(_roomName, new RoomOptions { IsOpen = true, IsVisible = true, MaxPlayers = 4 }, TypedLobby.Default);
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
            GameObject room = Instantiate(roomItem);
            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;

            roomData.DispRoomData();

            roomData.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnClickRoomItem(roomData.roomName);
            });

            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
        }
    }

    void OnClickRoomItem(string roomName)
    {
        string _userId = userId.text;

        if (string.IsNullOrEmpty(_userId))
        {
            _userId = GetUserId();
        }

        PhotonNetwork.player.NickName = _userId;
        PlayerPrefs.SetString(USER_ID, _userId);
        PhotonNetwork.JoinRoom(roomName);
    }
}