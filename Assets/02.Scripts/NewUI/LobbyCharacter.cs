using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacter : MonoBehaviour {
    public int index;
    // Use this for initialization
    void Start ()
    {
        GetComponent<Animator>().SetBool("IsRun", true);
	}

    void Update()
    {
        if(PlayerInfo.playerChar == index)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
}
