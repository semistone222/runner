using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ServerInit : MonoBehaviour {

	public string version = "v1.0";
	public InputField userId;
	public InputField roomName;
	public GameObject scrollContents;
	public GameObject roomItem;

	private string USER_ID = "USER_ID";
	private string USER_PREFIX = "USER_";
	private string ROOM_PREFIX = "ROOM_";

	void Awake () {
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings (version);
		}

		userId.text = GetUserId ();
		roomName.text = GetRoomName ();
	}

	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby !");
	}

	// TODO : PeerCreated에서 멈춘다. autoJoinLobby 이것 때문인가...
	// autoJoinLobby를 쓰지 않으면 방 목록을 불러 올 수 없는데...
	// when autoJoinLobby == false...
	void OnConnectedToMaster () {
		Debug.Log ("Entered Master !");
	}

	string GetUserId () {
		string uid = PlayerPrefs.GetString (USER_ID);

		if (string.IsNullOrEmpty (uid)) {
			Random.InitState (System.Environment.TickCount);
			uid = USER_PREFIX + Random.Range (0, 999);
		}

		return uid;
	}

	string GetRoomName() {
		Random.InitState (System.Environment.TickCount);
		string rn = ROOM_PREFIX + Random.Range (0, 999);
		return rn;
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("No Rooms !");

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		roomOptions.MaxPlayers = 20;

		PhotonNetwork.CreateRoom ("MyRoom", roomOptions, TypedLobby.Default);
	}

	void OnPhotonCreateRoomFailed (object[] error) {
		Debug.Log("OnPhotonCreateRoomFailed");
		Debug.Log(error[0].ToString()); // error code
		Debug.Log(error[1].ToString()); // error message
	}

	void OnJoinedRoom() {
		Debug.Log ("Enter Room");
		StartCoroutine (this.LoadGround ());
	}

	IEnumerator LoadGround() {
		PhotonNetwork.isMessageQueueRunning = false; // stop networking until loading scene

		SceneManager.LoadScene ("Ground");
		yield return null;
	}

	public void OnClickJoinRandomRoom() {
		string _userId = userId.text;

		if (string.IsNullOrEmpty (_userId)) {
			_userId = GetUserId ();
		}

		PhotonNetwork.player.NickName = _userId;
		PlayerPrefs.SetString (USER_ID, _userId);
		PhotonNetwork.JoinRandomRoom ();
	}

	public void OnClickCreateRoom() {
		string _userId = userId.text;
		string _roomName = roomName.text;

		if (string.IsNullOrEmpty (_userId)) {
			_userId = GetUserId ();
		}

		if(string.IsNullOrEmpty(_roomName)) {
			_roomName = GetRoomName();
		}

		PhotonNetwork.player.NickName = _userId;
		PlayerPrefs.SetString (USER_ID, _userId);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.IsOpen = true;
		roomOptions.IsVisible = true;
		roomOptions.MaxPlayers = 20;

		PhotonNetwork.CreateRoom (_roomName, roomOptions, TypedLobby.Default);
	}

	void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());	
	}

	void OnReceivedRoomListUpdate() {

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM")) {
			Destroy (obj);
		}

		int rowCount = 0;
		scrollContents.GetComponent<RectTransform> ().sizeDelta = Vector2.zero;

		foreach (RoomInfo _room in PhotonNetwork.GetRoomList ()) {
			GameObject room = (GameObject)Instantiate (roomItem);
			room.transform.SetParent (scrollContents.transform, false);

			RoomData roomData = room.GetComponent<RoomData> ();
			roomData.roomName = _room.Name;
			roomData.connectPlayer = _room.PlayerCount;
			roomData.maxPlayers = _room.MaxPlayers;

			roomData.DispRoomData ();

			roomData.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (delegate {
				OnClickRoomItem (roomData.roomName);
			});

			scrollContents.GetComponent<GridLayoutGroup> ().constraintCount = ++rowCount;
			scrollContents.GetComponent<RectTransform> ().sizeDelta += new Vector2 (0, 20);
		}
	}

	void OnClickRoomItem(string roomName) {
		string _userId = userId.text;

		if (string.IsNullOrEmpty (_userId)) {
			_userId = GetUserId ();
		}

		PhotonNetwork.player.NickName = _userId;
		PlayerPrefs.SetString (USER_ID, _userId);
		PhotonNetwork.JoinRoom (roomName);
	}
}
