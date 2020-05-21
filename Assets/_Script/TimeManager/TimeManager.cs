using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action<bool> EndDayTime;

    [SerializeField]
    Text timeText;
    [SerializeField]
    float increaseTime = 2.0f;

    float hour = 7;
    float min = 0;
    float sec = 0;

    bool isEndTime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEndTime)
        {
            if (hour >= 17)
            {
                isEndTime = true;
                hour = 17;
                min = 0;
                EndDayTime?.Invoke(true);
            }

            timeText.text = hour.ToString("00") + ":" + min.ToString("00");
            min += Time.deltaTime * increaseTime;   // * (144 / 60)

            if (min >= 60.0f)
            {
                min = 0;
                hour++;
            }
        }
    }

    public void StartDay()
    {
        isEndTime = false;
        hour = 7;
        min = 0;

        EndDayTime?.Invoke(false);
    }
}
