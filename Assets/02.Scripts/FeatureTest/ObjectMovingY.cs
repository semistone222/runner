﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ObjectMovingY , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  Y방향으로 이동하는 오브젝트를 위한 컴포넌트 입니다.
 */

public class ObjectMovingY : MonoBehaviour
{

    public float moveSpeed;    //오브젝트의 이동속도
    public bool isRepeat;   //오브젝트가 왕복하는가?
    public float maxAmount; //움직이는 최대 position

    private float start;
    private float last;
    private float finish;
    private Vector3 moveVector;

    private float minusFactor = 1f;

    void Awake()
    {
        maxAmount = Mathf.Abs(maxAmount);
        start = transform.position.y;
        StartCoroutine(MovingCoroutine());
    }

    void Start()
    {
        start = transform.position.y;
        StartCoroutine(MovingCoroutine());
    }

    IEnumerator MovingCoroutine()
    {
        for (;;)
        {
            moveVector = (Vector3.up * moveSpeed) * minusFactor;
            transform.Translate(moveVector, Space.World);

            moveVector = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, start-maxAmount, start+maxAmount), transform.position.z);
            transform.position = moveVector;

            last = transform.position.y;

            yield return new WaitForSeconds(Time.deltaTime);    //On-line에서도 문제 없을지 확인해야함!

            if (isRepeat)
            {
                if (Mathf.Abs(last - start) >= maxAmount)
                {
                    minusFactor *= -1; //방향 전환
                }
            }
        }
    }
}
