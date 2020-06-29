using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public Action<List<MaterialItemManager>> ChageBag;

    [SerializeField]
    GameObject materialPrefab;
    [SerializeField]
    Transform materialViewPort;
    [SerializeField]
    Transform EquipmentViewPort;
    [SerializeField]
    Transform FurnitureViewPort;
    [SerializeField]
    Text description;
    List<MaterialItemManager> curMaterialItems;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> materialContents;
    List<GameObject> equipmentContents;
    List<GameObject> FurniturContents;

    // Start is called before the first frame update
    void Start()
    {
        curMaterialItems = new List<MaterialItemManager>();
        materialContents = new List<GameObject>();
        equipmentContents = new List<GameObject>();
        FurniturContents = new List<GameObject>();

        CreateContents();
    }

    void CreateContents()
    {
        // 재료
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(materialViewPort);
            materialContents.Add(tempGO);
            tempGO.GetComponent<BagItemInfo>().ClickItem += OnClickItem;
            tempGO.SetActive(true);
        }

        // 제작물
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(EquipmentViewPort);
            equipmentContents.Add(tempGO);
            tempGO.GetComponent<BagItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }

        // 가구
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(FurnitureViewPort);
            FurniturContents.Add(tempGO);
            tempGO.GetComponent<BagItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }
    }

    public void OnAddBagItem(Data data)
    {
        for (int k = 0; k < materialContents.Count; k++)
        {
            materialContents[k].SetActive(false);
            equipmentContents[k].SetActive(false);
            FurniturContents[k].SetActive(false);
        }

        int i = 0;
        foreach (var item in data.CURMATERIALITELIST)
        {
            materialContents[i].GetComponent<BagItemInfo>().Initialization(item.ITEMINFO);
            materialContents[i].SetActive(true);
            i++;
        }

        int j = 0;
        foreach (var item in data.CURPRODUCTIONITEMLIST)
        {
            //Debug.Log(i + " 번 째");
            //Debug.Log(item.ITEMINFO.NAME + "??");
            Debug.Log(item.ITEMINFO.AMOUNTNUMBER + "??");
            equipmentContents[j].GetComponent<BagItemInfo>().Initialization(item.ITEMINFO);
            equipmentContents[j].SetActive(true);
            j++;
        }

        int t = 0;
        foreach (var item in data.CURFURNITUREITEMLIST)
        {
            FurniturContents[t].GetComponent<BagItemInfo>().Initialization(item.ITEMINFO);
            FurniturContents[t].SetActive(true);
            t++;
        }
        //ChageBag?.Invoke(data.CURMATERIALITELIST);


        //materialViewPort.GetComponent<GridLayoutGroup>().constraintCount = data.CURMATERIALITELIST.Count;
        //EquipmentViewPort.GetComponent<GridLayoutGroup>().constraintCount = data.CURPRODUCTIONITEMLIST.Count;

        //bool isHaved = false;

        //if (curMaterialItems.Count == 0 && data.ADDMATERIALLIST.Count != 0) //맨 처음 추가용
        //{
        //    foreach (var item in data.ADDMATERIALLIST)
        //    {
        //        var tempGO = Instantiate(materialPrefab);
        //        MaterialItemManager temp = tempGO.GetComponent<MaterialItemManager>();
        //        tempGO.transform.SetParent(materialViewPort);
        //        temp.Initialization(item.ITEMINFO, false);     // 나중에 아이템이 갑자기 사라지는 버그 나올 듯.
        //        temp.ClickMaterial += OnClickBagMatrial;
        //        curMaterialItems.Add(temp);
        //    }
        //    data.ADDMATERIALLIST.Clear();
        //}
        //else
        //{
        //    foreach (var item in data.ADDMATERIALLIST)
        //    {
        //        isHaved = false;
        //        foreach (var curItem in curMaterialItems)
        //        {
        //            if (item.NAME == curItem.NAME)
        //            {
        //                //Debug.Log(curItem.ITEMINFO.AMOUNTNUMBER);
        //                //Debug.Log(item.ITEMINFO.AMOUNTNUMBER);
        //                //curItem.ITEMINFO.AMOUNTNUMBER += 1;
        //                //Debug.Log(item.ITEMINFO.AMOUNTNUMBER);
        //                curItem.ITEMINFO.AMOUNTNUMBER += item.ITEMINFO.AMOUNTNUMBER;
        //                isHaved = true;
        //            }
        //            Debug.Log(item.NAME);
        //        }
        //        if (!isHaved)
        //        {
        //            var tempGO = Instantiate(materialPrefab);
        //            MaterialItemManager temp = tempGO.GetComponent<MaterialItemManager>();
        //            tempGO.transform.SetParent(materialViewPort);
        //            temp.Initialization(item.ITEMINFO, false);     // 나중에 아이템이 갑자기 사라지는 버그 나올 듯.
        //            temp.ClickMaterial += OnClickBagMatrial;
        //            curMaterialItems.Add(temp);
        //        }
        //    }
        //    data.ADDMATERIALLIST.Clear();
        //}

    }
    void OnClickItem(ItemInfo item)
    {
       description.text = item.NAME + "\n" + item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    }

    public void OnClickExitBtn()
    {
        description.text = "";
    }
}
