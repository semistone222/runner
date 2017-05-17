using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class BuyManager : MonoBehaviour
{
    public ShopManager shopManager;
    public GameObject[] items;
    public int currentItemCode;

    public int currentCoin;
    int cost;

    public void Buy()
    {
        //인벤토리에추가는 isBought로 해결되었음

        GetComponent<AudioSource>().Play();
        //돈계산();
        countCoin();
        //아이템 비활성화();
        disableItem();
        //창닫기();
        shopManager.btnToggleTextBox();
    }

    void countCoin()
    {
        currentCoin = PlayerPrefs.GetInt("Coin");
        cost = items[currentItemCode].GetComponent<Item>().itemCost;

        PlayerPrefs.SetInt("Coin", currentCoin - cost);
    }

    void disableItem()
    {
        string currentItemString = "Item" + currentItemCode.ToString();
        items[currentItemCode].GetComponent<Button>().interactable = false;
        items[currentItemCode].GetComponent<Item>().isBought = true;
        PlayerPrefs.SetInt(currentItemString, 1);
    }

}
