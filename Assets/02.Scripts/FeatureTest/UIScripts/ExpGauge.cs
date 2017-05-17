using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpGauge : MonoBehaviour
{
    Slider slider;
    int exp;
    int expChunk;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        exp = PlayerPrefs.GetInt("EXP");
        if(exp < 1000 && exp >= 0)
        {
            slider.value = exp % 100;
        }
        else if( exp >= 1000)
        {
            slider.value = 100;
        }
        else
        {
            slider.value = 0;
        }
    }
}
