using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ObjectMoving , by Jin-seok, Yu
 * 
 *  Last Update : Apr 26th, 2017
 * 
 *  이동하는 오브젝트를 위한 컴포넌트 입니다.
 */

public class ObjectMoving : MonoBehaviour {

    public Vector3 moveSpeed;    //오브젝트의 이동속도
    public bool isRepeat;   //오브젝트가 왕복하는가?
    public Vector3 maxAmount; //움직이는 최대 position

    private Vector3 start;
    private Vector3 last;
    private Vector3 finish;
    private Vector3 moveVector;

    private float minusFactor = 1f;

    void Awake()
    {
        maxAmount = new Vector3(Mathf.Abs(maxAmount.x), Mathf.Abs(maxAmount.y), Mathf.Abs(maxAmount.z));
    }

    void Start()
    {
        start = transform.position;
        StartCoroutine(MovingCoroutine());
    }

    IEnumerator MovingCoroutine()
    {
        for (;;)
        {
            moveVector = (Vector3.right * moveSpeed.x + Vector3.up * moveSpeed.y + Vector3.forward * moveSpeed.z) * minusFactor;
            transform.Translate(moveVector, Space.World);

            moveVector = new Vector3(Mathf.Clamp(transform.position.x, start.x - maxAmount.x, start.x + maxAmount.x),
                Mathf.Clamp(transform.position.y, start.y - maxAmount.y, start.y + maxAmount.y), 
                Mathf.Clamp(transform.position.z, start.z - maxAmount.z, start.z + maxAmount.z));
            transform.position = moveVector;

            last = transform.position;

            yield return new WaitForSeconds(Time.deltaTime);    //On-line에서도 문제 없을지 확인해야함!

            if (isRepeat)
            {
                if (Mathf.Abs(last.x - start.x) >= maxAmount.x && Mathf.Abs(last.y - start.y) >= maxAmount.y && Mathf.Abs(last.z - start.z) >= maxAmount.z)
                {
                    minusFactor *= -1; //방향 전환
                }
            }
        }
    }
}
