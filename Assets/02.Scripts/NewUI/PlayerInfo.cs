using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    /*변수 선언부, 추후에 private으로 바꾸는게 나을 수도 있음*/
    public static string playerName;                         //플레이어 명
    public static int playerLevel;                           //플레이어 레벨
    public static int playerExpCur;                          //플레이어 현재 경험치 양
    public static int playerExpNext;                         //플레이어 요구 경험치 양
    public static int playerGold;                            //플레이어 골드 양
    public static int playerChar = 0;                        //현재 캐릭터 인덱스
    public static Dictionary<int,bool> isCharUnlocked;       //캐릭터 해금 여부

    public void Start()
    {
        GetValuesFromServer();
    }

    private void GetValuesFromServer()
    {
        //서버로 부터 값을 불러오는 함수
        playerName = "애니런";
        playerLevel = 30;
        playerExpCur = 3565;
        playerExpNext = 3565;
        playerGold = 500000;
        isCharUnlocked = new Dictionary<int, bool>();
        for(int i = 0; i < CharacterDB.characterList.Count; i++)
        {
            isCharUnlocked[i] = false;
        }
        playerChar = 0;
        isCharUnlocked[playerChar] = true;
        isCharUnlocked[2] = true;
    }
}
