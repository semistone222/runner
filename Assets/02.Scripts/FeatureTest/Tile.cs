using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Tile , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  모든 타일의 기초가 될 super class 입니다.
 */

public class Tile : MonoBehaviour
{
    /*Collider 동적 생성시 크기를 결정할 X Y Z 상수입니다.*/
    const float colX = 10f;
    const float colY = 1f;
    const float colZ = 10f;
    /******************************************************/

    private BoxCollider bCollider;

    private void Awake()
    {   /*초기 기동시 , Collider가 있는지 체크합니다.*/
        FindCollider();
    }

    private void FindCollider()
    {
        /*Collider가 존재하지 않는 경우, Collider를 생성합니다. */
        try
        {
            bCollider = GetComponent<BoxCollider>();
            if (bCollider)
            {
                Debug.Log(transform.name + " has BoxCollider (is trigger)");
            }
            else
            {
                bCollider = this.gameObject.AddComponent<BoxCollider>();
                bCollider.size = new Vector3(colX, colY, colZ); //현재, 상수값으로 Collider 크기를 결정하므로, 추후 수정 필요

                Debug.Log("Warning: [" + transform.name + "] does not have any BoxCollider, so it has automatically attatched");
            }

            bCollider.isTrigger = true;
        }
        catch (System.Exception)
        {

            throw;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        EnterFunc(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitFunc(other);
    }

    private void OnTriggerStay(Collider other)
    {
        StayFunc(other);
    }

    /*Tile의 sub class에서 override해 사용할 함수입니다.*/
    protected virtual void EnterFunc(Collider other)
    {
        //Collider에 접근했을 때 호출할 함수.
    }
    protected virtual void ExitFunc(Collider other)
    {
        //Collider를 빠져나갔을 때 호출할 함수.
    }
    protected virtual void StayFunc(Collider other)
    {
        //Collider에 머무를 때 호출할 함수
    }


}
