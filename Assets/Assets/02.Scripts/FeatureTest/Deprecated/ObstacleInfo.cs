using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ObstacleInfo , by Jin-seok, Yu
 * 
 *  Last Update : Apr 28th, 2017
 * 
 *  각 장애물의 기능과 관련한 컴포넌트입니다. Inspector에서 설정 가능합니다.
 */

public class ObstacleInfo : Obstacle
{
    CrowdControl[] ccArray; 
    CrowdControl cc;

    public string Tag;
    public float movespeedMultiplier;
    public float jumpspeedMultiplier;
    public float durationTime;
    public bool isDestroyable;

    private bool hasTag = false;

    protected override void EnterFunc(Collider other)
    {
        hasTag = false;
        ccArray = other.GetComponents<CrowdControl>();
        foreach(CrowdControl c in ccArray)
        {
            if( string.Equals(c.Tag, Tag) )
            {
                hasTag = true;
                break;
            }
        }

        if(hasTag)
        {
            cc.currentTime = 0f;
        }
        else
        {
            cc = other.gameObject.AddComponent<CrowdControl>();
            cc.Tag = Tag;
            cc.movespdMultiplier = movespeedMultiplier;
            cc.jumpspdMultiplier = jumpspeedMultiplier;
            cc.durationTime = durationTime;
        }
        
        if(isDestroyable)
        {
            Destroy(this.gameObject);
        }
    }
}
