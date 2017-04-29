using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Text txtConnect;
	public Text txtLogMsg;

	private PhotonView myPhotonView;
	
	void Awake () {
		myPhotonView = GetComponent<PhotonView> ();
		PhotonNetwork.isMessageQueueRunning = true;
		StartCoroutine (this.CreatePlayer ());
		GetConnectPlayerCount ();
	}

	IEnumerator Start() {
		yield return new WaitForSeconds (1.0f);

		string msg = "\n<color=#00ff00>[" + PhotonNetwork.player.NickName + "] Connected</color>";
		myPhotonView.RPC("LogMsg", PhotonTargets.AllBuffered, msg);
	}

	IEnumerator CreatePlayer() {
		float pos = Random.Range (-3f, 3f);
		PhotonNetwork.Instantiate ("Player", new Vector3 (pos, 5.0f, pos), Quaternion.identity, 0);
		yield return null;
	}

	void GetConnectPlayerCount() {
		Room currRoom = PhotonNetwork.room;

		txtConnect.text = currRoom.PlayerCount.ToString () + "/" + currRoom.MaxPlayers.ToString ();
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
		Debug.Log (newPlayer.ToStringFull ());
		GetConnectPlayerCount ();
	}

	void OnPhotonPlayerDisConnected(PhotonPlayer outPlayer) {
		Debug.Log (outPlayer.ToStringFull ());
		GetConnectPlayerCount ();
	}

	public void OnClickExitButton() {

		string msg = "\n<color=#ff0000>[" + PhotonNetwork.player.NickName + "] Disconnected</color>";
		myPhotonView.RPC ("LogMsg", PhotonTargets.AllBuffered, msg);

		PhotonNetwork.LeaveRoom ();
	}

	void OnLeftRoom() {
        Debug.Log("OnLeftRoom");
		SceneManager.LoadScene("Lobby");
	}

    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerDisconnected: " + player);

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerDisconnected - isMasterClient");
        }
    }

    [PunRPC]
	void LogMsg(string msg) {
		txtLogMsg.text = txtLogMsg.text + msg;
	}    
}