using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Text txtConnect;
   // public Text txtLogMsg;

    private PhotonView myPhotonView;

    void Awake()
    {
        PreserveBGM.SetBGM(2);
        TileNameDefaultAdjust();

        myPhotonView = GetComponent<PhotonView>();
        PhotonNetwork.isMessageQueueRunning = true;
        StartCoroutine(CreatePlayer());
        GetConnectPlayerCount();
    }

    void TileNameDefaultAdjust()
    {
        GameObject tempGameObject;
        int count = 0;

        while(tempGameObject = GameObject.Find("default"))
        {
            if(tempGameObject == null)
            {
                break;
            }
            else
            {
                tempGameObject.name = "default" + count.ToString();
                count++;
            }
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);

        string msg = "\n<color=#00ff00>[" + PhotonNetwork.player.NickName + "] Connected</color>";
        myPhotonView.RPC("LogMsg", PhotonTargets.AllBuffered, msg);
    }

    IEnumerator CreatePlayer()
    {
        float pos = Random.Range(-3f, 3f);
        //PhotonNetwork.Instantiate("rabbit", new Vector3(pos, 60.0f, pos), Quaternion.identity, 0);
        PhotonNetwork.Instantiate(CharacterDB.characterList[PlayerInfo.playerChar]+"Player", new Vector3(pos, 60.0f, pos), Quaternion.identity, 0);
        yield return null;
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.room;

        //txtConnect.text = currRoom.PlayerCount.ToString() + "/" + currRoom.MaxPlayers.ToString();
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

    public void OnClickExitButton()
    {
        string msg = "\n<color=#ff0000>[" + PhotonNetwork.player.NickName + "] Disconnected</color>";
        myPhotonView.RPC("LogMsg", PhotonTargets.AllBuffered, msg);

        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
    void LogMsg(string msg)
    {
       // txtLogMsg.text = txtLogMsg.text + msg;
    }
}