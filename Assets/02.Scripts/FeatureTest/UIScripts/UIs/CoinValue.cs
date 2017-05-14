using UnityEngine;
using System.Collections;

public class CoinValue : UIValue
{
    void Update()
    {
        value = PlayerPrefs.GetInt("Coin");
        valueText.text = value.ToString();
    }

}
