﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WorkstationManager : MonoBehaviour
{
    public Action<List<ItemInfo>, int, int> ClickCrateBtnInWorkstation;

    [SerializeField]
    GameObject toolsContent;
    [SerializeField]
    GameObject equipmentContent;
    [SerializeField]
    GameObject furnitureContent;
    [SerializeField]
    GameObject materialContent;
    [SerializeField]
    GameObject itemPrefabs;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Text itemNameText;
    [SerializeField]
    Text itemDescriptText;
    [SerializeField]
    Image[] materialImage;
    [SerializeField]
    Text[] materialText;
    [SerializeField]
    Text createCountNumbeText;

    List<ProductionObjInfo> productionList;
    List<ItemInfo> materialItems;

    Data data;
    ProductionObjInfo selectItem;

    GameObject Notification; //골드 부족, 재료 부족 등 알림창

    public void Initialization(List<ProductionObjInfo> productionList, List<ItemInfo> materialItems)
    {
        this.productionList = productionList;
        this.materialItems = materialItems;
        SetViewPort();
    }

    // Start is called before the first frame update
    void Start()
    {
        setActiveUI(false);

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].gameObject.SetActive(false);
            materialText[i].gameObject.SetActive(false);
        }
        Notification = GameObject.Find("Canvas").transform.Find("Notification").gameObject;
    }

    void setActiveUI(bool isActive)
    {
        itemImage.gameObject.SetActive(isActive);
        itemNameText.gameObject.SetActive(isActive);
        itemDescriptText.gameObject.SetActive(isActive);
    }

    void SetViewPort()
    {
        foreach (var item in productionList)
        {
            if(item.SORT == "도구")
            {
                CreateItemInVIewprot(toolsContent, item);
            }
            else if (item.SORT == "장비")
            {
                CreateItemInVIewprot(equipmentContent, item);
            }
            else if (item.SORT == "가구")
            {
                CreateItemInVIewprot(furnitureContent, item);
            }
            else if (item.SORT == "재료")
            {
                CreateItemInVIewprot(materialContent, item);
            }
        }
    }

    void CreateItemInVIewprot(GameObject ParentGO, ProductionObjInfo info)
    {
        var item = Instantiate(itemPrefabs);
        item.transform.SetParent(ParentGO.transform);
        item.GetComponent<WorkstationItem>().Initialization(info);
        item.GetComponent<WorkstationItem>().ClickItem += OnClickItem;

    }

    void UpdateList()
    {
        if (selectItem == null)
            return;

        itemImage.sprite = Resources.Load<Sprite>("ICON/" + selectItem.ICON_INDEX);
        itemNameText.text = selectItem.NAME;
        itemDescriptText.text = selectItem.DESCRIPTION;

        var needMaterialList = selectItem.COMBINATIONLIST;
        string materialId = needMaterialList[0];
        string materialName = "";
        int materialCounter = 0;
        int materialIndex = 0;
        int curMaterialCount = 0;
        int k = 0;

        for (k = 0; k < needMaterialList.Count; k++)
        {
            if (materialId == needMaterialList[k])
            {
                materialCounter++;
            }
            else
            {
                materialImage[materialIndex].sprite = Resources.Load<Sprite>("ICON/" + materialId);

                foreach (var materialItem in materialItems)
                {
                    if (materialId == materialItem.ID.ToString())
                    {
                        materialName = materialItem.NAME;
                        break;
                    }
                }

                foreach (var curMaterialItem in data.CURMATERIALITELIST)
                {
                    curMaterialCount = 0;
                    if (materialId == curMaterialItem.ITEMINFO.ID.ToString())
                    {
                        curMaterialCount = curMaterialItem.ITEMINFO.AMOUNTNUMBER;
                        break;
                    }
                }

                materialText[materialIndex].text = materialName + " " +
                    (materialCounter * int.Parse(createCountNumbeText.text)) + "/" + curMaterialCount;
                materialImage[materialIndex].gameObject.SetActive(true);
                materialText[materialIndex].gameObject.SetActive(true);
                //SetMaterialNameAndCount(materialId, materialCounter, materialIndex);
                materialIndex++;
                materialCounter = 1;
                Debug.Log("k : " + k + "count : " + needMaterialList.Count);
                materialId = needMaterialList[k];
                Debug.Log(materialId);

            }

            if (k == needMaterialList.Count - 1)
            {
                materialImage[materialIndex].sprite = Resources.Load<Sprite>("ICON/" + materialId);

                foreach (var materialItem in materialItems)
                {
                    if (materialId == materialItem.ID.ToString())
                    {
                        materialName = materialItem.NAME;
                        break;
                    }
                }

                foreach (var curMaterialItem in data.CURMATERIALITELIST)
                {
                    curMaterialCount = 0;
                    if (materialId == curMaterialItem.ITEMINFO.ID.ToString())
                    {
                        curMaterialCount = curMaterialItem.ITEMINFO.AMOUNTNUMBER;
                        break;
                    }
                }

                materialText[materialIndex].text = materialName + " " +
                    (materialCounter * int.Parse(createCountNumbeText.text)) + "/" + curMaterialCount;
                materialImage[materialIndex].gameObject.SetActive(true);
                materialText[materialIndex].gameObject.SetActive(true);
                //SetMaterialNameAndCount(materialId, materialCounter, materialIndex);
                materialIndex++;
                materialCounter = 1;
                Debug.Log("i : " + k + "count : " + needMaterialList.Count);
                materialId = needMaterialList[k];
            }
        }

        for (int i = k; i < materialImage.Length; i++)
        {
            materialImage[i].gameObject.SetActive(false);
            materialText[i].gameObject.SetActive(false);
        }

    }

    void OnClickItem(ProductionObjInfo item)
    {
        setActiveUI(true);
        SoundManager.instance.PlayEff(EffSound.SFX_UI_button);
        selectItem = item;
        itemImage.sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX);
        itemNameText.text = item.NAME;
        itemDescriptText.text = item.DESCRIPTION;

        var needMaterialList = item.COMBINATIONLIST;
        string materialId = needMaterialList[0];
        string materialName = "test";
        int materialCounter = 0;
        int materialIndex = 0;
        int curMaterialCount = 0;
        int k = 0;

        for (k = 0; k < needMaterialList.Count; k++)
        {
            if(materialId == needMaterialList[k])
            {
                materialCounter++;
            }
            else
            {
                materialImage[materialIndex].sprite = Resources.Load<Sprite>("ICON/" + materialId);

                foreach (var materialItem in materialItems)
                {
                    //Debug.Log("materialId : " + materialId);
                    //Debug.Log("materialItem.ID : " + materialItem.ID);
                    if (materialId == materialItem.ID.ToString())
                    {
                        materialName = materialItem.NAME;
                        //Debug.Log("Name : " + materialItem.NAME);
                        break;
                    }
                }

                foreach (var curMaterialItem in data.CURMATERIALITELIST)
                {
                    curMaterialCount = 0;
                    if (materialId == curMaterialItem.ITEMINFO.ID.ToString())
                    {
                        curMaterialCount = curMaterialItem.ITEMINFO.AMOUNTNUMBER;
                        break;
                    }
                }

                materialText[materialIndex].text = materialName + " " +
                    (materialCounter * int.Parse(createCountNumbeText.text)) + "/" + curMaterialCount;
                materialImage[materialIndex].gameObject.SetActive(true);
                materialText[materialIndex].gameObject.SetActive(true);
                Debug.Log("Name : " + materialName);
                //SetMaterialNameAndCount(materialId, materialCounter, materialIndex);
                materialIndex++;
                materialCounter = 1;
                Debug.Log("k : " + k + "count : " + needMaterialList.Count);
                materialId = needMaterialList[k];
                Debug.Log(materialId);

            }

            if(k == needMaterialList.Count -1)
            {
                materialImage[materialIndex].sprite = Resources.Load<Sprite>("ICON/" + materialId);

                foreach (var materialItem in materialItems)
                {
                    if (materialId == materialItem.ID.ToString())
                    {
                        materialName = materialItem.NAME;
                        break;
                    }
                }

                foreach (var curMaterialItem in data.CURMATERIALITELIST)
                {
                    curMaterialCount = 0;
                    if (materialId == curMaterialItem.ITEMINFO.ID.ToString())
                    {
                        curMaterialCount = curMaterialItem.ITEMINFO.AMOUNTNUMBER;
                        break;
                    }
                }

                materialText[materialIndex].text = materialName + " " +
                    (materialCounter * int.Parse(createCountNumbeText.text)) + "/" + curMaterialCount;

                Debug.Log("Name : " + materialName);
                materialImage[materialIndex].gameObject.SetActive(true);
                materialText[materialIndex].gameObject.SetActive(true);
                //SetMaterialNameAndCount(materialId, materialCounter, materialIndex);
                materialIndex++;
                materialCounter = 1;
                Debug.Log("i : " + k + "count : " + needMaterialList.Count);
                materialId = needMaterialList[k];
            }
        }


        for (int i = materialIndex; i < materialImage.Length; i++)
        {
            materialImage[i].gameObject.SetActive(false);
            materialText[i].gameObject.SetActive(false);
        }
      
    }

    public void OnClickPlusMinusBtn(int add)
    {
        if (int.Parse(createCountNumbeText.text) + add <= 1)
            createCountNumbeText.text = 1.ToString();
        else
        {
            if (int.Parse(createCountNumbeText.text) + add >= 99)
                createCountNumbeText.text = 99.ToString();
            else
                createCountNumbeText.text = (int.Parse(createCountNumbeText.text) + add).ToString();
        }

        UpdateList();
    }

    public void OnClickCreateBtn()
    {
        if (selectItem == null)
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "제작할 물품을 선택해주세요.";
            return;
        }
        bool isResult = false;
        var combinationList = selectItem.COMBINATIONLIST;
        string materialid = combinationList[0];
        int materialCounter = 0;
        int trueCounter = 0;
        int materialKindCounter = 0;
        int i = 0;
        List<ItemInfo> BuyList = new List<ItemInfo>();
        for (i = 0; i < combinationList.Count; i++)
        {
            if (materialid == combinationList[i])
                materialCounter++;
            else
            {

                materialKindCounter++;
                foreach (var curitem in data.CURMATERIALITELIST)
                {
                    //Debug.Log("materialId : " + materialid);
                    //Debug.Log("materialCounter : " + materialCounter);
                    //Debug.Log("curitem.ITEMINFO.ID.ToString() : " + curitem.ITEMINFO.ID.ToString());
                    //Debug.Log("curitem.ITEMINFO.AMOUNTNUMBER : " + curitem.ITEMINFO.AMOUNTNUMBER);
                    //Debug.Log("materialCounter * int.Parse(createCountNumbeText.text) : " + (materialCounter * int.Parse(createCountNumbeText.text)));

                    if (materialid == curitem.ITEMINFO.ID.ToString())
                    {
                        int number = 0;
                        Debug.Log("curitem.Info.Amo : " + curitem.ITEMINFO.AMOUNTNUMBER);
                        Debug.Log("CountNumbeText : " + (materialCounter * int.Parse(createCountNumbeText.text)));

                        if ((materialCounter * int.Parse(createCountNumbeText.text)) == 0)
                            number++;
                        else
                            number = (materialCounter * int.Parse(createCountNumbeText.text));

                        if (curitem.ITEMINFO.AMOUNTNUMBER - number < 0)
                        {
                            isResult = false;
                            break;
                        }
                        else
                        {
                            var temp = new ItemInfo(int.Parse(materialid), "", "", "", 0, 0, "");
                            temp.AMOUNTNUMBER = number;
                            BuyList.Add(temp);
                            isResult = true;
                            trueCounter++;
                        }

                    }
                }
                materialid = combinationList[i];
                materialCounter = 0;
            }

            if (i == combinationList.Count - 1)
            {
                //materialCounter = 1;
                if (materialid == combinationList[i])
                {
                    Debug.Log("Counter++");
                    materialCounter++;
                }

                materialKindCounter++;

                if (materialKindCounter == 1)
                {
                    //materialKindCounter++;
                    materialCounter--;
                }

                //materialCounter--;
                foreach (var curitem in data.CURMATERIALITELIST)
                {
                    //Debug.Log("materialId : " + materialid);
                    //Debug.Log("materialCounter : " + materialCounter);
                    //Debug.Log("curitem.ITEMINFO.ID.ToString() : " + curitem.ITEMINFO.ID.ToString());
                    //Debug.Log("curitem.ITEMINFO.AMOUNTNUMBER : " + curitem.ITEMINFO.AMOUNTNUMBER);
                    //Debug.Log("materialCounter * int.Parse(createCountNumbeText.text) : " + (materialCounter * int.Parse(createCountNumbeText.text)));

                    if (materialid == curitem.ITEMINFO.ID.ToString())
                    {
                        Debug.Log("curitem.Info.Amo : " + curitem.ITEMINFO.AMOUNTNUMBER);
                        Debug.Log("CountNumbeText : " + (materialCounter * int.Parse(createCountNumbeText.text)));
                        Debug.Log("MaterialCounter : " + materialCounter);
                        if (curitem.ITEMINFO.AMOUNTNUMBER - (materialCounter * int.Parse(createCountNumbeText.text)) < 0)
                        {
                            isResult = false;
                            //Debug.Log("flase ++");
                            break;
                        }
                        else
                        {
                            var temp = new ItemInfo(int.Parse(materialid), "", "", "", 0, 0, "");
                            temp.AMOUNTNUMBER = materialCounter * int.Parse(createCountNumbeText.text);
                            BuyList.Add(temp);
                            isResult = true;
                            trueCounter++;
                            //Debug.Log("true ++");
                        }

                    }
                }

            }
        }

        if(materialKindCounter == trueCounter)
        {
            ClickCrateBtnInWorkstation?.Invoke(BuyList, selectItem.ID, int.Parse(createCountNumbeText.text));
        }
        else
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "재료가 부족합니다.";
        }

        Debug.Log("Result : " + isResult);
        Debug.Log("materialKindCounter : " + materialKindCounter);
        Debug.Log("trueCounter : " + trueCounter);
    }

    public void OnGetData(Data data)
    {
        this.data = data;
        UpdateList();
    }
}
