using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemManager : MonoBehaviour
{
    public GameObject textBox;
    public ShopManager shopManager;
    public BuyManager buyManager;

    Item itemInfo;

    void Awake()
    {
        itemInfo = GetComponent<Item>();
        transform.FindChild("Cost")
            .gameObject.GetComponent<Text>().text = itemInfo.itemCost.ToString();

        StatusChange();
    }

    void Update()
    {

        StatusChange();
    }

    void StatusChange()
    {
        string currentItemString = "Item" + itemInfo.itemCode.ToString();
        if (PlayerPrefs.GetInt(currentItemString) == 0)
        {
            itemInfo.isBought = false;
        }
        else
        {
            itemInfo.isBought = true;
        }

        if (PlayerPrefs.GetInt("Coin") < itemInfo.itemCost)
        {
            GetComponent<Button>().interactable = false;
            transform.FindChild("Cost")
                .gameObject.GetComponent<Text>().color = new Color(1f, 0f, 0f);
        }
        else
        {
            transform.FindChild("Cost")
                .gameObject.GetComponent<Text>().color = new Color(1f, 1f, 1f);

            if (!itemInfo.isBought)
            {
                GetComponent<Button>().interactable = true;
            }
            else
            {
                GetComponent<Button>().interactable = false;
            }
        }


    }

    public void itemManage()
    {
        GetComponent<AudioSource>().Play();

        textBox.transform.FindChild("NameText")
            .gameObject.GetComponent<Text>().text = itemInfo.itemName;

        Image img = GetComponent<Image>();
        textBox.transform.FindChild("ExplainImage")
            .gameObject.GetComponent<Image>().sprite
            = img.sprite;
        float ratio = img.rectTransform.rect.height/img.rectTransform.rect.width;
        textBox.transform.FindChild("ExplainImage").localScale
            = new Vector3(1f, ratio, 1f);
           
        textBox.transform.FindChild("Cost")
            .gameObject.GetComponent<Text>().text = itemInfo.itemCost.ToString();
        textBox.transform.FindChild("ExplainText")
            .gameObject.GetComponent<Text>().text = itemInfo.itemText.ToString();
        buyManager.currentItemCode = itemInfo.itemCode;

        shopManager.btnToggleTextBox();

    }

}
