using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour {

    public const int basePrice = 600;

    public static int price = basePrice;
    private int count = 0;

    private List<GameObject> cbList;
	// Use this for initialization
	void Start ()
    {
        CharacterBoxInit();
	}
	
	// Update is called once per frame
	void Update ()
    {
        count = 0;
        foreach(bool isUnlocked in PlayerInfo.isCharUnlocked.Values)
        {
            if(isUnlocked)
            {
                count++;
            }
        }
        price = basePrice * (int)(Mathf.Pow(2f,count-1));
	}

    void CharacterBoxInit()
    {
        cbList = new List<GameObject>();
        GameObject cbPrefab = Resources.Load("UI/CharacterBox") as GameObject;
        GameObject tempCb;
        RectTransform thisRt = this.GetComponent<RectTransform>();


        for(int i = 0; i < CharacterDB.characterList.Count; i ++)
        {
            cbList.Add(Instantiate(cbPrefab, thisRt.position, thisRt.rotation, thisRt));
            tempCb = cbList[i];
            tempCb.GetComponent<CharacterBox>().index = i;
            tempCb.GetComponentInChildren<Text>().text = CharacterDB.characterNameList[i];
            Instantiate(Resources.Load("Animation/"+CharacterDB.characterList[i]+"Shop") as GameObject, tempCb.transform);
        }
    }
}
