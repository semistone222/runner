using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadExpCur : ReadValue {

    protected override void UpdateValue()
    {
        base.UpdateValue();
        uiText.text = PlayerInfo.playerExpCur.ToString();
    }
}
