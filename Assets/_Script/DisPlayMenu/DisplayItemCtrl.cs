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

    public ItemInfo ITEMINFO
    {
        get { return ItemInfo; }
    }

    public void Initialization(ItemInfo itemInfo)
    {
        this.ItemInfo = new ItemInfo(itemInfo);
    }

    public void SetItemPos(Transform itemPos)
    {
        this.itemPos = itemPos;
        transform.position = this.itemPos.position;
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
