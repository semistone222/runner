using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotating : MonoBehaviour
{

    public float rotateSpeed;    //오브젝트의 회전속도
    public bool isClockwise;   //true: 시계방향, false: 반시계방향


    void Start()
    {
        StartCoroutine(MovingCoroutine());
    }

    IEnumerator MovingCoroutine()
    {
        for (;;)
        {
            transform.Rotate(Vector3.up, rotateSpeed);
            yield return new WaitForSeconds(Time.deltaTime * 2);    //On-line에서도 문제 없을지 확인해야함!

            if (isClockwise)
            {
                rotateSpeed = Mathf.Abs(rotateSpeed);
            }
            else
            {
                rotateSpeed = -Mathf.Abs(rotateSpeed);
            }
        }
    }
}