using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDB : MonoBehaviour
{
    public static List<string> characterList;
    public static List<string> characterNameList;

    void Awake()
    {/*실제 DB 구축 전까지 사용할 임시 리스트*/
        characterList = new List<string>();
        characterNameList = new List<string>();
        characterList.Add("Rabbit");
        characterNameList.Add("토순이");
        characterList.Add("Fox");
        characterNameList.Add("여웅이");
        characterList.Add("Dog");
        characterNameList.Add("슬뭉이");
        characterList.Add("Bear");
        characterNameList.Add("곰곰이");
    }

}
