using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerScore : UIValue
{
    void Update()
    {
        value = PlayerPrefs.GetInt("TowerScore", value);
        valueText.text = value.ToString();
    }
    
    
}
