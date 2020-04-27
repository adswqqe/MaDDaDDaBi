using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Action<bool> resultCalcGold;
    int lever;
    int exe;
    int gold = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void CalcGold(int cost)
    {
        bool isResult = false;
        if ((gold - cost) < 0)
            Debug.Log("머니가 머니머니부족해");
        else
        {
            Debug.Log("구매구매성공");
            isResult = true;
            gold -= cost;
        }

        resultCalcGold?.Invoke(isResult);
    }
}
