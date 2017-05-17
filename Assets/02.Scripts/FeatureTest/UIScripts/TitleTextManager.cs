using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleTextManager : MonoBehaviour
{
    Strings strings;
    Colors colors;
    Text textA;
    int exp;

    void Awake()
    {
        strings = GetComponent<Strings>();
        colors = GetComponent<Colors>();
        textA = GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update()
    {
        exp = PlayerPrefs.GetInt("EXP");
        exp = Mathf.FloorToInt(exp / 100) + 1;
        if(exp > 10)
        {
            exp = 10;
        }
        else if(exp <= 0)
        {
            exp = 1;
        }
        exp--;
        textA.text = strings.stateStrings[exp];
        textA.color = colors.stateColors[exp];

    }
}
