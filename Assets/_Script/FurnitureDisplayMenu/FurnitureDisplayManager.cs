using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureDisplayManager : MonoBehaviour
{
    public Action<int> StartBuild;

    [SerializeField]
    Transform furnitureScrollview;
    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    GameManager[] furniturePrefabs;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> furniturContents;

    ItemInfo selectIteminfo;

    // Start is called before the first frame update
    void Start()
    {
        furniturContents = new List<GameObject>();

        CreateContents();
    }

    void CreateContents()
    {
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(furnitureScrollview);
            furniturContents.Add(tempGO);
            tempGO.GetComponent<BagItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }
    }
    public void OnAddBagItem(Data data)
    {
        for (int k = 0; k < furniturContents.Count; k++)
        {
            furniturContents[k].SetActive(false);
        }
        int i = 0;
        foreach (var item in data.CURFURNITUREITEMLIST)
        {
            Debug.Log("호출 : " + item.ITEMINFO.ID);
            furniturContents[i].GetComponent<BagItemInfo>().Initialization(item.ITEMINFO);
            furniturContents[i].SetActive(true);
            i++;
        }


    }

    public void OnClickItem(ItemInfo item)
    {
        selectIteminfo = item;
        StartBuild?.Invoke(selectIteminfo.ID);
        //description.text = item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    }

    public void OnClickDisplayBtn()
    {
        if (selectIteminfo == null)
            return;

    }
}
