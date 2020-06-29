using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public Action<bool> resultCalcGold;
    public Action<Data> changeData;

    List<ItemInfo> createItem;
    List<ProductionObjInfo> workStationItemList;
    Data data;

    List<MaterialItemManager> curMaterialShopping;      // 코드 임시 대체
    // Start is called before the first frame update

    public bool pickup;
    int OrderNum; // 주문을 누른 횟수 1이면 상품 결제하고 박스 생성 2이면 박스 터치했을 때 아이템 획득
    Animator GoldPoping;
    GameObject Notification; //골드 부족, 재료 부족 등 알림창
    int itemAmonts = 0;

    void Start()
    {
        dataSet();       // 향후 세이브 로드로 수정해야함
        GoldPoping = GameObject.Find("Canvas").transform.Find("TOP").transform.Find("Gold_Poping").GetComponent<Animator>();
        Notification = GameObject.Find("Canvas").transform.Find("Notification").gameObject;
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
        int tempitemAmonts = 0;
        int sumCost = 0;

        foreach (var item in curMatrialShoppingBaske)
        {
            Debug.Log(pickup);
            if (OrderNum == 1)
            {
                Debug.Log("썸0");
                sumCost = 0;
            }
            else
            {
                Debug.Log("썸0");
                sumCost += item.BUYCOST * item.ITEMINFO.AMOUNTNUMBER;
            }


            tempitemAmonts += item.ITEMINFO.AMOUNTNUMBER;
        }

        // 인벤토리의 공간이 부족했을 경우의 예외처리를 해야함.

        Debug.Log(sumCost);

        if ((data.GOLD - sumCost) < 0)
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "골드가 부족합니다.";
        }
            
        else if (pickup == true)
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "배달함을 확인해주세요.";
            resultCalcGold?.Invoke(true);
            return;
        }
        else
        {
            Debug.Log(data.BAGSPACE + "현재공간 ");
            Debug.Log(itemAmonts + "구매할 아이템 갯수");
            if (data.BAGSPACE + tempitemAmonts <= data.MAX_BAGSPCE && OrderNum == 0)
            {
                Debug.Log(data.BAGSPACE + "현재공간 ");
                Debug.Log(itemAmonts + "구매할 아이템 갯수");

                Debug.Log("주문성공");
                data.GOLD -= sumCost;
                OrderNum++;

                pickup = true;
                itemAmonts = tempitemAmonts;
                changeData?.Invoke(data);
                foreach (var item in curMatrialShoppingBaske)
                {
                    data.ADDMATERIALLIST.Add(item);
                }

                resultCalcGold?.Invoke(true);
                GameObject.Find("UnderFloor").transform.Find("OrderBox").gameObject.SetActive(true); // 택배박스 활성화
                return;
            }
            else if (data.BAGSPACE + tempitemAmonts <= data.MAX_BAGSPCE && OrderNum >= 1)
            {
                data.BAGSPACE += itemAmonts;
                isResult = true;
                Debug.Log("픽업성공");
                bool isHave = false;
                GameObject.Find("OrderBox").SetActive(false); // 택배박스 활성화

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

                OrderNum = 0;

                resultCalcGold?.Invoke(isResult);

                changeData?.Invoke(data);

                data.ADDMATERIALLIST.Clear();   // 재사용을 위한 초기화
                itemAmonts = 0;

                pickup = false;
            }
            else
            {
                Notification.SetActive(true);
                Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "가방 공간이 부족합니다.";
            }
        }


    }

    public void OnCreateProduction(List<ItemInfo> materials, ProductionObjInfo production)
    {
        bool isHave = false;
        bool isWaste = false;

        // null이라면 아무것도 조합되지 않았다는 뜻이므로 쓰레기를 던져야함.
        if (production == null)
        {
            production = new ProductionObjInfo(999, "테스트", "쓰레기", "998", "실패한 연금술", "NONE");
            data.CURWASTEITEMLIST.Add(production);
            isWaste = true;
        }

        foreach (var curItem in data.CURPRODUCTIONITEMLIST)
        {
            if (production.NAME == curItem.NAME)
            {
                Debug.Log(production.NAME);
                curItem.ITEMINFO.AMOUNTNUMBER += production.ITEMINFO.AMOUNTNUMBER;
                isHave = true;
            }
        }

        if (!isHave && !isWaste)
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
                    data.BAGSPACE -= 1;
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

        data.BAGSPACE += 1;  //1인 이유는 현재 아이템이 하나씩 추가되서
        Debug.Log("가방 수량 : " + data.BAGSPACE + data.CURPRODUCTIONITEMLIST.Count);
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
                Debug.Log("판매");
                data.GOLD += item.SELLCOST;
                data.EXP += 5;
                GoldPoping.SetTrigger("Trigger");
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
                    data.BAGSPACE -= material.AMOUNTNUMBER;
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
                        Debug.Log("제작물");

                        var temp = new ProductionObjInfo(workstationItem);
                        if (amo >= 2)
                            temp.ITEMINFO.AMOUNTNUMBER = amo;
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
                        Debug.Log("가구");
                        var temp = new FurnitureItem();
                        temp.Initialization(workstationItem.ITEMINFO);
                        if (amo >= 2)
                            temp.ITEMINFO.AMOUNTNUMBER = amo;
                        data.CURFURNITUREITEMLIST.Add(temp);
                    }
                }
                else if (workstationItem.SORT == "재료")
                {
                    foreach (var item in data.CURMATERIALITELIST)
                    {
                        if(item.ITEMINFO.ID == workstationItem.ID)
                        {
                            isHave = transform;
                            item.ITEMINFO.AMOUNTNUMBER += amo;
                        }
                    }
                    if (!isHave)
                    {
                        var temp = new MaterialItemManager();
                        Debug.Log(workstationItem.ITEMINFO.ICON_INDEX);
                        temp.Initialization(workstationItem.ITEMINFO);
                        if (amo >= 2)
                            temp.ITEMINFO.AMOUNTNUMBER = amo;
                        data.CURMATERIALITELIST.Add(temp);
                    }
                }
            }
        }
        data.BAGSPACE += amo;
        changeData?.Invoke(data);
    }

    public void OnConfirmationFurniture(int id)
    {
        int index = 0;
        bool isFind = false;
        for (int i = 0; i < data.CURFURNITUREITEMLIST.Count; i++)
        {
            if(data.CURFURNITUREITEMLIST[i].ITEMINFO.ID == id)
            {
                data.CURFURNITUREITEMLIST[i].ITEMINFO.AMOUNTNUMBER -= 1;

                if(data.CURFURNITUREITEMLIST[i].ITEMINFO.AMOUNTNUMBER <= 0)
                {
                    isFind = true;
                    index = i;
                }

                if (id == 511)
                {
                    data.MAX_BAGSPCE += 10;
                }
            }
        }

        if (isFind)
            data.CURFURNITUREITEMLIST.RemoveAt(index);

        data.BAGSPACE -= 1;
        changeData?.Invoke(data);

        //foreach (var item in data.CURFURNITUREITEMLIST)
        //{
        //    if(item.ITEMINFO.ID == id)
        //    {
        //        item.ITEMINFO.AMOUNTNUMBER -= 1;

        //        if (item.ITEMINFO.AMOUNTNUMBER <= 0)
        //        {
        //            Debug.Log("itemITEMINFO.AMOUNTNUMBER : " + item.ITEMINFO.AMOUNTNUMBER);
        //            temp = item;
        //        }
        //    }
        //}

    }

    public void OnDisplayFurniture(GameObject furnitureGo)
    {
        if (furnitureGo.name.Contains(511.ToString()))
            return;

        data.CURDISPLAYFURNITUREITEMLIST.Add(furnitureGo);

        Debug.Log(data.CURDISPLAYFURNITUREITEMLIST[0].name);

        changeData?.Invoke(data);
    }

    public void OnWasteProcessing(int count)
    {
        data.GOLD -= count * 2;
        Debug.Log(data.CURWASTEITEMLIST.Count + "쓰레기 count");
        for (int i = 0; i < count; i++)
        {
            data.CURWASTEITEMLIST.RemoveAt(0);
        }

        changeData?.Invoke(data);
    }
}
