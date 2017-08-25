using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadName : ReadValue {

    protected override void UpdateValue()
    {
        base.UpdateValue();
        uiText.text = PlayerInfo.playerName.ToString();
    }
}
