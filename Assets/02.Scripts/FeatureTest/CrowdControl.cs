using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControl : MonoBehaviour
{

    public string Tag;
    public float movespdMultiplier;
    public float jumpspdMultiplier;
    public float durationTime;
    //[HideInInspector]
    public float currentTime = 0f;

    private void Update()
    {
        if(TimeCheck())
        {
            Destroy(this);
        }
    }

    private bool TimeCheck()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= durationTime)
        {
            return true;
        }

        return false;
    }

}
