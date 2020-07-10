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

    float hour = 19;
    float min = 0;
    float sec = 0;

    bool isEndTime = true;
    bool isBuilding = false;    

    [SerializeField]
    Light light;
    GameObject SleepAni;
    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        SleepAni = GameObject.Find("PopMenuBundle").transform.Find("SleepAni").gameObject;
        EndDayTime?.Invoke(true);
        timeText.text = hour.ToString("00") + ":" + min.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEndTime)
        {
            if (hour >= 19)
            {
                isEndTime = true;
                SoundManager.instance.PlayBGM(BGMSound.BGM_inUnderG);
                SoundManager.instance.PlayEffBgm(BGMEffSound.AMB_UnderG);
                hour = 19;
                min = 0;
                EndDayTime?.Invoke(true);
                light.intensity = 0;
            }

            if (isBuilding == false)
            {
                timeText.text = hour.ToString("00") + ":" + min.ToString("00");
                min += Time.deltaTime * increaseTime;   // * (144 / 60)
            }

            if (min >= 60.0f)
            {
                min = 0;
                hour++;
            }
        }
    }

    public void OnStartBuilding(bool isBuilding)
    {
        this.isBuilding = isBuilding;
    }

    public void StartDay()
    {
        SleepAni.SetActive(true);
        light.intensity = 1;
        StartCoroutine("SleepAniOff");
    }

    IEnumerator SleepAniOff()
    {
        //Debug.Log("코루틴");
        SoundManager.instance.PlayEff(EffSound.SFX_UI_sleep);
        bool isOnce = true;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            //Debug.Log("플레이중");
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && isOnce)
            {
                EndDayTime?.Invoke(false);
                isOnce = false;
            }
            yield return null;
        }

            //Debug.Log("탈출");
            SleepAni.SetActive(false);
            isEndTime = false;
            hour = 7;
            min = 0;

        SoundManager.instance.PlayBGM(BGMSound.BGM_inStore);
        SoundManager.instance.PlayEffBgm(BGMEffSound.AMB_Forest);
        yield return null;
        
}

}
