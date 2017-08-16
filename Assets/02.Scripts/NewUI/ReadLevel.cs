using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadLevel : ReadValue {

    protected override void UpdateValue()
    {
        base.UpdateValue();
        uiText.text = PlayerInfo.playerLevel.ToString();
    }
}
