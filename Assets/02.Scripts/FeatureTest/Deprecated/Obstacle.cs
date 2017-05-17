using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Obstacle , by Jin-seok, Yu
 * 
 *  Last Update : Apr 28th, 2017
 * 
 *  모든 장애물의 기초가 될 super class 입니다.
 */

public class Obstacle : MonoBehaviour
{
    /******************************************************/

    private Collider pCollider; //오브젝트가 가지고 있어야하는 물리적 콜라이더 (is trigger 체크 해제)
    private MeshCollider mpCollider; //오브젝트가 가지고 있어야하는 물리적 메시 콜라이더
    private Collider tCollider; //스크립트가 부착할 트리거 콜라이더 (is trigger 체크)
    private MeshCollider mCollider; //위와 같으나, MeshCollider 인 경우(convex)

    private void Awake()
    {   /*초기 기동시 , Collider가 있는지 체크합니다.*/
        FindCollider();
    }

    private void FindCollider()
    {
        try
        {
            pCollider = GetComponent<Collider>();
            if (pCollider.GetType().ToString() == "UnityEngine.MeshCollider")
            {   
                mpCollider = GetComponent<MeshCollider>();

                mCollider = CopyComponent(mpCollider, this.gameObject);
                mCollider.convex = true;
                mCollider.isTrigger = true;
            }
            else if(pCollider == null)
            {   /*Collider가 존재하지 않는 경우, 경고 메시지를 출력합니다.*/
                Debug.Log("Warning: [" + transform.name + "] does not have any physical collider!\n It must have it to check collision");
            }
            else
            {
                /*Mesh Collider가 아닐때 */
                /*Collider가 존재하는 경우, 같은 형태의 Trigger Colider를 복사.*/
                tCollider = CopyComponent(pCollider, this.gameObject);
                tCollider.isTrigger = true;
            }

        }
        catch (System.Exception)
        {

            throw;
        }
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
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

    /*Obstacle의 sub class에서 override해 사용할 함수입니다.*/
    protected virtual void EnterFunc(Collider other)
    {
        //Collider에 접근했을 때 호출할 함수.
        Debug.Log("으악!");
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
