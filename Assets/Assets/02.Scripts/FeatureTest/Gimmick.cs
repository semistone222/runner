﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Gimmick , by Jin-seok, Yu
 * 
 *  Last Update : May 19th, 2017
 * 
 *  모든 특수 오브젝트의 기초가 될 super class 입니다.

    -현재 충돌이슈문제로, 콜라이더 생성 기능을 제거하였습니다.
 */

public class Gimmick : MonoBehaviour
{
	private Collider pCollider; //오브젝트가 가지고 있어야하는 물리적 콜라이더 (is trigger 체크 해제)
	private Collider tCollider; //스크립트가 부착할 트리거 콜라이더 (is trigger 체크)

	private MeshCollider mpCollider; //오브젝트가 가지고 있어야하는 물리적 메시 콜라이더
	private MeshCollider mCollider; //위와 같으나, MeshCollider 인 경우(convex)

	private BoxCollider bpCollider; //오브젝트가 가지고 있어야하는 물리적 박스 콜라이더
	private BoxCollider bCollider; //위와 같으나, BoxCollider 인 경우

	static float bColFactorX = 0.03f;
    static float bColFactorY = 0.03f;
    static float bColFactorZ = 0.03f;
    static float sColFactor = 0.05f;

	private void Start()
	{/*기동시 콜라이더 있는지 확인*/
		//FindCollider(); 
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
			else if (pCollider.GetType().ToString() == "UnityEngine.BoxCollider")
			{
				bpCollider = GetComponent<BoxCollider>();

				bCollider = CopyComponent(bpCollider, this.gameObject);
				// bCollider.center = new Vector3(bCollider.center.x, bCollider.center.y + bColFactor
				//     , bCollider.center.z);
				bCollider.size = new Vector3(bCollider.size.x * (1 + bColFactorX), bCollider.size.y * (1 + bColFactorY), bCollider.size.z * (1 + bColFactorZ));
				bCollider.isTrigger = true;
			}
			else if (pCollider == null)
			{   /*Collider가 존재하지 않는 경우, 경고 메시지를 출력합니다.*/
				Debug.Log("Warning: [" + transform.name + "] does not have any physical collider!\n It must have it to check collision");
			}
			else
			{
				/*그 밖의 콜라이더 */
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
    /*
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
    */

	/*Obstacle의 sub class에서 override해 사용할 함수입니다.*/
	public virtual void EnterFunc(Collider other)
	{
		//Collider에 접근했을 때 호출할 함수.
	}
	public virtual void ExitFunc(Collider other)
	{
		//Collider를 빠져나갔을 때 호출할 함수.
	}
	public virtual void StayFunc(Collider other)
	{
		//Collider에 머무를 때 호출할 함수
	}

}
