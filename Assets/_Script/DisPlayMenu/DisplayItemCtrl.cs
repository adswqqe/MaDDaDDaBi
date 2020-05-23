using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemCtrl : MonoBehaviour
{
    public Action<ItemInfo> itemSell;

    ItemInfo ItemInfo;
    Transform itemPos;

    public Transform ITEMPOS
    {
        get { return itemPos; }
    }

    public void Initialization(ItemInfo itemInfo, Transform itemPos)
    {
        this.ItemInfo = new ItemInfo(itemInfo);
        this.itemPos = itemPos;
        //this.itemPos.position = itemPos.position;

        transform.position = itemPos.position;
    }

    void Start()
    {
            
    }

    public void OnSellItem()
    {
        gameObject.SetActive(false);
        Debug.Log("itemCtrl 함수");
        itemSell?.Invoke(ItemInfo);
    }
}
