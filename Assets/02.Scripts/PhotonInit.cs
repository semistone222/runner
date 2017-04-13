using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInit : MonoBehaviour {

	public string version = "v1.0";

	void Awake () {
		PhotonNetwork.ConnectUsingSettings (version);
	}

	void OnConnectedToMaster () {
		Debug.Log ("Entered Lobby !");

		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("No Rooms !");

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		roomOptions.MaxPlayers = 20;

		TypedLobby typedLobby = new TypedLobby ();

		PhotonNetwork.CreateRoom ("MyRoom", roomOptions, typedLobby);
	}

	void OnPhotonCreateRoomFailed (object[] error) {
		Debug.Log(error[0].ToString()); // error code
		Debug.Log(error[1].ToString()); // error message
	}

	void OnJoinedRoom() {
		Debug.Log ("Enter Room");

		StartCoroutine (this.CreatePlayer ());
	}

	IEnumerator CreatePlayer() {
		float pos = Random.Range (-3f, 3f);
		PhotonNetwork.Instantiate ("Player", new Vector3 (pos, 5.0f, pos), Quaternion.identity, 0);
		yield return null;
	}

	void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());	
	}
}
