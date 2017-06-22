using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    UIValue.cs by Jin-seok, Yu, Jun 22th

    UI 값을 static으로 저장하고 있습니다.
    추후에 PlayerPref 이나 DB등으로 게임 기동시 읽어와야 합니다.

    대신 PlayerPref은 보안에 취약함
*/

public class UIValue : MonoBehaviour {

    
    public const float camValueLow = 0.05f;
    public const float camValueMid = 0.25f;
    public const float camValueHigh = 0.5f;
    public const float volMAX = 0f;
    public const float volMIN = -80f;

    public static float camValue = camValueLow;
    public static bool snapsToFinger = false;
    public static bool bgmOn = true;
    public static bool seOn = true;

    void Awake()
    {
        GetUserUiValues();
    }

    void GetUserUiValues()
    {
        //추후에 여기서 값을 읽어올 것
    }
}
