using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisPlayMenuManager : MonoBehaviour
{
    public Action<ItemInfo> DisplayItemObj;
    public Action<GameObject> DisPlayItem;

    [SerializeField]
    GameObject[] displayItemObjs;
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Transform potionViewPort;
    [SerializeField]
    Transform toolsViewPort;
    [SerializeField]
    Transform equipmentViewPort;
    [SerializeField]
    Text description;
    [SerializeField]
    Button displayBtn;
    //[SerializeField]
    //Transform[] displayStandPos;

    int curDisplayCount = 0;
    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> potionContents;
    List<GameObject> toolsContents;
    List<GameObject> equipmentContents;

    List<GameObject> curDisplayItemObjList;

    ItemInfo seletingItem;
    List<GameObject> curFurnitureList;

    // Start is called before the first frame update
    void Start()
    {
        potionContents = new List<GameObject>();
        toolsContents = new List<GameObject>();
        equipmentContents = new List<GameObject>();
        curDisplayItemObjList = new List<GameObject>();
        curFurnitureList = new List<GameObject>();

        CreateContents();
    }

    void CreateContents()
    {
        // 포션
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(potionViewPort);
            potionContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }

        // 도구
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(toolsViewPort);
            toolsContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }

        // 장비
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(equipmentViewPort);
            equipmentContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }
    }

    public void OnAddItemViewPort(Data data)
    {
        curFurnitureList = data.CURDISPLAYFURNITUREITEMLIST;

        for (int k = 0; k < MAX_CONTENT_SIZE; k++)
        {
            potionContents[k].SetActive(false);
            toolsContents[k].SetActive(false);
            equipmentContents[k].SetActive(false);
        }

        int i = 0;
        foreach (var item in data.CURPRODUCTIONITEMLIST)
        {
            Debug.Log(item.SORT);

            if (item.SORT == "물약")
            {
                potionContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                potionContents[i].SetActive(true);
            }
            else if (item.SORT == "도구")
            {
                toolsContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                toolsContents[i].SetActive(true);
            }
            else if (item.SORT == "장비")
            {
                equipmentContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                equipmentContents[i].SetActive(true);
            }

            i++;
        }
    }

    void OnClickItem(ItemInfo item)
    {
        description.text = item.NAME + "\n" + item.DESCRIPTION + "\n" + item.AMOUNTNUMBER + "개";
        seletingItem = new ItemInfo(item);
        displayBtn.gameObject.SetActive(true);
    }

    public void OnClickDisplayBtn()
    {
        bool hasSortFurnitrue = false;
        bool isFull = false;
        int fullCounter = 0;
        if (seletingItem == null)
            return;

        //if (curDisplayCount >= displayStandPos.Length)
        //    curDisplayCount = 0;
        int sortCount = 0;
        foreach (var item in curFurnitureList)
        {
            Debug.Log("seletingItem.SORT" + seletingItem.SORT);
            Debug.Log("item.ITEMINFO.SORT" + item.GetComponent<DisplayFurnitureItem>().ITEMINFO.NAME);
            if(item.GetComponent<DisplayFurnitureItem>().ITEMINFO.NAME.Contains
                (seletingItem.SORT))
            {
                hasSortFurnitrue = true;
                sortCount++;

                if (item.GetComponent<DisplayFurnitureItem>().isFull == true)
                {
                    isFull = true;
                    fullCounter++;
                }
            }
            Debug.Log("item.GetComponent<DisplayFurnitureItem>().isFull :" + item.GetComponent<DisplayFurnitureItem>().isFull);

        }

        if (hasSortFurnitrue == false)
        {
            Debug.Log("asdasd");
            return;
        }

        Debug.Log("FullCounter : " + fullCounter);
        Debug.Log("CurFurnListCount : " + curFurnitureList.Count);
        //if (fullCounter >= curFurnitureList.Count)
        //    return;

        if (fullCounter >= sortCount)
            return;

        Debug.Log(seletingItem.NAME);
        int goIndex = 0;
        for (int i = 0; i < displayItemObjs.Length; i++)
        {
            if(seletingItem.ID.ToString().Contains(displayItemObjs[i].name))
            {
                goIndex = i;
            }
        }
        var tempGo = Instantiate(displayItemObjs[goIndex]);
        //Debug.Log(displayStandPos[curDisplayCount].position);
        tempGo.GetComponent<DisplayItemCtrl>().Initialization(seletingItem);
        curDisplayItemObjList.Add(tempGo);

        DisplayItemObj?.Invoke(seletingItem);
        DisPlayItem?.Invoke(tempGo);

        curDisplayCount += 1;
        seletingItem = null;
        description.text = "";

    }
}
