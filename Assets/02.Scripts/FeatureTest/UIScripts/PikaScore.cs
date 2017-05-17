using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PikaScore : UIValue
{
    void Update()
    {
        value = PlayerPrefs.GetInt("PikaScore", value);
        valueText.text = value.ToString();
    }
    
    
}
