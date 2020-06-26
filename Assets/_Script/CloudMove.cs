using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    Vector3 pos; //현재위치

    float delta = 100.0f; // 좌(우)로 이동가능한 (x)최대값

    float speed = 0.01f; // 이동속도

    float time; // 시간

    public GameObject TextUI;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        Vector3 v = pos;

        v.x += delta * Mathf.Sin(Time.time * speed);

        // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.

        transform.position = v;

        time += Time.deltaTime;
        if(time > 3)
        {
            if(TextUI != null)
            TextUI.SetActive(true);
            time = 0;

        }
        else if(time > 1)
        {
            if(TextUI != null)
            TextUI.SetActive(false);
           
        }

    }
}
