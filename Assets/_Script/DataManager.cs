using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Action<bool> resultCalcGold;
    public Action<Data> changeData;

    Data data;
    // Start is called before the first frame update
    void Start()
    {
        dataSet();       // 향후 세이브 로드로 수정해야함

        changeData?.Invoke(data);
    }

    void dataSet()
    {
        data = new Data(1, 0, 100, 0, 0, 10, new List<MaterialItemManager>());
    }


    public void CalcBuy(List<MaterialItemManager> curMatrialShoppingBaske)
    {
        bool isResult = false;

        int sumCost = 0;
        int itemAmonts = 0;
        foreach (var item in curMatrialShoppingBaske)
        {
            sumCost += item.BUYCOST * item.ITEMINFO.AMOUNTNUMBER;
            itemAmonts += item.ITEMINFO.AMOUNTNUMBER;
        }

        // 인벤토리의 공간이 부족했을 경우의 예외처리를 해야함.

        if ((data.GOLD - sumCost) < 0)
            Debug.Log("머니가 머니머니부족해");
        else
        {
            if (data.BAGSPACE + itemAmonts < data.MAX_BAGSPCE)
            {
                Debug.Log("구매구매성공");
                isResult = true;
                data.GOLD -= sumCost;
                data.BAGSPACE += itemAmonts;
                bool isHave = false;

                foreach (var item in curMatrialShoppingBaske)
                {
                    data.ADDMATERIALLIST.Add(item);
                }

                foreach (var item in data.ADDMATERIALLIST)
                {
                    foreach (var curItem in data.CURMATERIALITELIST)
                    {
                        if (item.NAME == curItem.NAME)
                        {
                            curItem.ITEMINFO.AMOUNTNUMBER += item.ITEMINFO.AMOUNTNUMBER;
                            isHave = true;
                        }
                    }
                    if (!isHave)
                        data.CURMATERIALITELIST.Add(item);
                }
                
            }
            else
                Debug.Log("공간이 부족해~");
        }

        resultCalcGold?.Invoke(isResult);
        changeData?.Invoke(data);
    }
}
