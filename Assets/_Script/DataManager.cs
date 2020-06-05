using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Action<bool> resultCalcGold;
    public Action<Data> changeData;

    List<ItemInfo> createItem;
    List<ProductionObjInfo> workStationItemList;
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

    public void Initialization(List<ItemInfo> createItem, List<ProductionObjInfo> workStationItemList)
    {
        this.createItem = createItem;
        this.workStationItemList = workStationItemList;
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
            if (data.BAGSPACE + itemAmonts <= data.MAX_BAGSPCE)
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
                    Debug.Log(item.NAME);
                    foreach (var curItem in data.CURMATERIALITELIST)
                    {
                        if (item.NAME == curItem.NAME)
                        {
                            curItem.ITEMINFO.AMOUNTNUMBER += item.ITEMINFO.AMOUNTNUMBER;
                            isHave = true;
                        }
                    }
                    if (!isHave)
                    {
                        data.CURMATERIALITELIST.Add(item);
                        Debug.Log(item.NAME);
                    }
                }
                
            }
            else
                Debug.Log("공간이 부족해~");
        }

        resultCalcGold?.Invoke(isResult);

        changeData?.Invoke(data);
        data.ADDMATERIALLIST.Clear();   // 재사용을 위한 초기화
    }

    public void OnCreateProduction(List<ItemInfo> materials, ProductionObjInfo production)
    {
        bool isHave = false;

        // null이라면 아무것도 조합되지 않았다는 뜻이므로 쓰레기를 던져야함.
        if (production == null)
            production = new ProductionObjInfo(999, "테스트", "쓰레기", "998", "실패한 연금술", "NONE");


        foreach (var curItem in data.CURPRODUCTIONITEMLIST)
        {
            if (production.NAME == curItem.NAME)
            {
                Debug.Log(production.NAME);
                curItem.ITEMINFO.AMOUNTNUMBER += production.ITEMINFO.AMOUNTNUMBER;
                isHave = true;
            }
        }

        if (!isHave)
        {
            data.CURPRODUCTIONITEMLIST.Add(production);
            Debug.Log("isHaved" + production.AMOUNTNUMBER);
        }

        MaterialItemManager tempItem = null;
        foreach (var item in materials)     // 소지 재료 감소
        {
            foreach (var dataItem in data.CURMATERIALITELIST)
            {
                if (item.NAME == dataItem.NAME)
                {
                    //Debug.Log(item.AMOUNTNUMBER + " 갯수");
                    dataItem.ITEMINFO.AMOUNTNUMBER -= 1;
                    if (dataItem.ITEMINFO.AMOUNTNUMBER <= 0)
                    {
                        //         tempItem = dataItem;
                        data.CURMATERIALITELIST.Remove(dataItem);
                        //Debug.Log(dataItem.NAME +"이게 왜 NULL?");
                        break;
                    }
                }
            }
            //if (tempItem.NAME != null)
            //{
            //    data.CURMATERIALITELIST.Remove(tempItem);
            //    Debug.Log("진입");
            //}
        }

        data.BAGSPACE = data.CURMATERIALITELIST.Count + 1;  //1인 이유는 현재 아이템이 하나씩 추가되서
        Debug.Log("가방 수량 : " + data.CURMATERIALITELIST.Count + data.CURPRODUCTIONITEMLIST.Count);
        changeData?.Invoke(data);

    }

    public void OnDisplayItemObj(ItemInfo iteminfo)
    {
        foreach (var item in data.CURPRODUCTIONITEMLIST)
        {
            if(item.ID == iteminfo.ID)
            {
                item.ITEMINFO.AMOUNTNUMBER -= 1;

                if (item.ITEMINFO.AMOUNTNUMBER <= 0)
                {
                    data.CURPRODUCTIONITEMLIST.Remove(item);
                    break;
                }
            }
        }

        data.BAGSPACE -= 1;
        changeData?.Invoke(data);
    }


    public void OnSellItem(ItemInfo itemInfo)
    {
        foreach (var item in createItem)
        {
            if(item.ID == itemInfo.ID)
            {
                data.GOLD += item.SELLCOST;
                data.EXP += 5;
                break;
            }
        }

        if (data.EXP >= 15)
        {
            data.Level += 1;
            data.EXP = 0;
        }
        changeData?.Invoke(data);

    }

    public void OnCreateInWorkStation(List<ItemInfo> materials, int id, int amo)
    {
        foreach (var material in materials)
        {
            MaterialItemManager finditem = null;
            bool isZreo = false;
            foreach (var dataItem in data.CURMATERIALITELIST)
            {
                if(material.ID == dataItem.ITEMINFO.ID)
                {
                    dataItem.ITEMINFO.AMOUNTNUMBER -= material.AMOUNTNUMBER;
                    if (dataItem.ITEMINFO.AMOUNTNUMBER <= 0)
                    {
                        finditem = dataItem;
                        isZreo = true;
                    }
                }
            }
            if (isZreo)
                data.CURMATERIALITELIST.Remove(finditem);
        }

        foreach (var workstationItem in workStationItemList)
        {
            bool isHave = false;
            if(workstationItem.ID == id)
            {
                if (workstationItem.SORT != "가구" && workstationItem.SORT != "재료")
                {
                    foreach (var item in data.CURPRODUCTIONITEMLIST)
                    {
                        if (item.ID == workstationItem.ID)
                        {
                            isHave = true;
                            item.ITEMINFO.AMOUNTNUMBER += amo;
                        }
                    }
                    if (!isHave)
                    {
                        var temp = new ProductionObjInfo(workstationItem);
                        data.CURPRODUCTIONITEMLIST.Add(temp);
                    }
                }
                else if (workstationItem.SORT == "가구")
                {
                    foreach (var item in data.CURFURNITUREITEMLIST)
                    {
                        if (item.ITEMINFO.ID == workstationItem.ID)
                        {
                            isHave = true;
                            item.ITEMINFO.AMOUNTNUMBER += amo;
                        }
                    }
                    if (!isHave)
                    {
                        var temp = new FurnitureItem();
                        temp.Initialization(workstationItem.ITEMINFO);
                        data.CURFURNITUREITEMLIST.Add(temp);
                    }
                }
                else if (workstationItem.SORT == "제작재료")
                {

                }
            }
        }

        changeData?.Invoke(data);
    }
}
