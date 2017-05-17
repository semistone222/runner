using UnityEngine;
using System.Collections;

public class BellValue : UIValue
{
    void Update()
    {
        value = PlayerPrefs.GetInt("Bell");
        valueText.text = value.ToString();
    }
}
