using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadExpNext : ReadValue {

    protected override void UpdateValue()
    {
        base.UpdateValue();
        uiText.text = PlayerInfo.playerExpNext.ToString();
    }
}
