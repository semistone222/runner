using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadGold : ReadValue {

    protected override void UpdateValue()
    {
        base.UpdateValue();
        uiText.text = PlayerInfo.playerGold.ToString();
    }
}
