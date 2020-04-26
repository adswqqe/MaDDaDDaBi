using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItemManager : MonoBehaviour
{
    public Action<ItemInfo> ClickMaterial;

    Text descriptionText;
    ItemInfo itemInfo;
    Button m_bt;
    int amountNumber = 0;

    public string NAME
    {
        get { return itemInfo.NAME; }
    }

    public int AMOUNTNUMBER
    {
        get { return amountNumber; }
    }

    public void Initialization(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;


        setContent(); 
        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(onClickMaterial);
    }

    public void InitializationShoppingBaske(ItemInfo iteminfo, int number)
    {
        this.itemInfo = iteminfo;

        setShoppingBaske(number);
    }

    public void setShoppingBaske(int number)
    {
        amountNumber += number;
        Debug.Log(itemInfo);
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = itemInfo.NAME + "    " + amountNumber + "개";
    }

    void setContent()
    {
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = itemInfo.NAME + " " + itemInfo.BUYCOST + "골드";
    }

    void onClickMaterial()
    {
        ClickMaterial?.Invoke(this.itemInfo);
    }
}
