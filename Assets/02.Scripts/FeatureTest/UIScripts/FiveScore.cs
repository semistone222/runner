using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FiveScore : UIValue
{
    void Update()
    {
        value = PlayerPrefs.GetInt("FiveScore", value);
        valueText.text = value.ToString();
    }
    
    
}
