using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadValue : MonoBehaviour {

    protected Text uiText;
	
    void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        UpdateValue();
    }

    protected virtual void UpdateValue()
    {
        //상속받아서, 각 UI에 맞게 값을 갱신할 함수.
    }
}
