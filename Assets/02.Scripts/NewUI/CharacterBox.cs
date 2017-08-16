using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBox : MonoBehaviour
{
    public int index;

    public GameObject btnChecked;
    public GameObject btnChoice;
    public GameObject btnBuy;
    public Text price;
    
    void Update()
    {
        if(PlayerInfo.playerChar == index)
        {
            btnChecked.SetActive(true);
            btnChoice.SetActive(false);
            btnBuy.SetActive(false);
        }
        else
        {
            if(PlayerInfo.isCharUnlocked[index])
            {
                btnChecked.SetActive(false);
                btnChoice.SetActive(true);
                btnBuy.SetActive(false);
            }
            else
            {
                btnChecked.SetActive(false);
                btnChoice.SetActive(false);
                btnBuy.SetActive(true);

                price.text = CharacterShop.price.ToString();
            }
        }
    }

    public void ButtonChoice()
    {
        PlayerInfo.playerChar = index;
        GameObject.Find("@MainManager").GetComponent<MainManager>().InstantiateLobbyChar(index);
    }

    public void ButtonPurchace()
    {
        PlayerInfo.playerGold -= CharacterShop.price;
        PlayerInfo.isCharUnlocked[index] = true;
    }
}
