using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisPlayMenuManager : MonoBehaviour
{
    public Action<ItemInfo> DisplayItemObj;
    public Action<DisplayItemCtrl> DisPlayItem;

    [SerializeField]
    GameObject displayItemObj;
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
    [SerializeField]
    Transform[] displayStandPos;

    int curDisplayCount = 0;
    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> potionContents;
    List<GameObject> toolsContents;
    List<GameObject> equipmentContents;

    List<GameObject> curDisplayItemObjList;

    ItemInfo seletingItem;

    // Start is called before the first frame update
    void Start()
    {
        potionContents = new List<GameObject>();
        toolsContents = new List<GameObject>();
        equipmentContents = new List<GameObject>();
        curDisplayItemObjList = new List<GameObject>();

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
        if (seletingItem == null)
            return;

        if (curDisplayCount >= displayStandPos.Length)
            curDisplayCount = 0;

        Debug.Log(seletingItem.NAME);
        var tempGo = Instantiate(displayItemObj);
        Debug.Log(displayStandPos[curDisplayCount].position);
        tempGo.GetComponent<DisplayItemCtrl>().Initialization(seletingItem, displayStandPos[curDisplayCount]);
        curDisplayItemObjList.Add(tempGo);

        DisplayItemObj?.Invoke(seletingItem);
        DisPlayItem?.Invoke(tempGo.GetComponent<DisplayItemCtrl>());

        curDisplayCount += 1;
        seletingItem = null;
        description.text = "";

    }
}
