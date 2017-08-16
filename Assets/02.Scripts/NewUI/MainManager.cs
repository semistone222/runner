using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject runUI;
    public GameObject charUI;
    public GameObject spawn;
    private GameObject temp;

    void Start()
    {
        InstantiateLobbyChar(PlayerInfo.playerChar);
    }

    public void InstantiateLobbyChar(int i)
    {
        string name = CharacterDB.characterList[i];
        temp = Instantiate(Resources.Load("Animation/" + name + "Lobby"), spawn.transform) as GameObject;
        temp.GetComponent<LobbyCharacter>().index = i++;
    }

    public void ChangeUI()
    {
        runUI.SetActive(!runUI.activeSelf);
        charUI.SetActive(!charUI.activeSelf);
    }
}
