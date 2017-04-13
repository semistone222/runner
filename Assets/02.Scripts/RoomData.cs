using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RoomData : MonoBehaviour {

	[HideInInspector]
	public string roomName = "";
	[HideInInspector]
	public int connectPlayer = 0;
	[HideInInspector]
	public int maxPlayers = 0;

	public Text textRoomName;
	public Text textConnectInfo;

	public void DispRoomData() {
		textRoomName.text = roomName;
		textConnectInfo.text = "(" + connectPlayer.ToString() + "/" + maxPlayers.ToString() + ")";
	}
}
