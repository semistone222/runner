using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject text;
    public GameObject FinishTimer;
    public GameObject RunningTimer;
    
    // Use this for initialization
    void Start()
    {
        text.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            text.SetActive(true);
            FinishTimer.SetActive(true);
            FinishTimer.GetComponent<Text>().text = Timer.timesec;
            RunningTimer.SetActive(false);

            PhotonView photonView = other.GetComponent<PhotonView>();
            photonView.RPC("OnGameEnd", PhotonTargets.AllViaServer, other.GetComponent<Transform>().name);

            //GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControllerOff> ().MOVESPD_ORIGIN = 0;
            //GameObject.Find ("ButtonBooster").GetComponent<Button> ().interactable = false;  // 부스터 버튼 클릭 불가
            //StartCoroutine (FinishGame());
        }
    }

    IEnumerator FinishGame()
    {
        SoundManager sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        yield return new WaitForSeconds(3.5f);

        GameObject.Find("ButtonBooster").GetComponent<Button>().interactable = true;  // 부스터 버튼 클릭 활성
        sm.SetBGMNull();
        sm.SetBGM1();

        SceneManager.LoadScene("Result");
    }
}