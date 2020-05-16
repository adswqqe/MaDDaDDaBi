using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItemManager : MonoBehaviour
{
    public Action<ItemInfo> ClickMaterial;
    public Action<MaterialItemManager> ClickMaterialInShoppingBaske;

    ItemInfo itemInfo;
    Button m_bt;

    bool isOrderMenu = false;

    public string NAME
    {
        get { return itemInfo.NAME; }
    }

    public bool ISORDERMENU
    {
        get { return isOrderMenu; }
    }

    //public int AMOUNTNUMBER
    //{
    //    set { amountNumber = value; }
    //    get { return amountNumber; }
    //}

    public int BUYCOST
    {
        get { return itemInfo.BUYCOST; }
    }

    public ItemInfo ITEMINFO
    {
        get { return itemInfo;}
    }

    public void Initialization(ItemInfo itemInfo, bool isOrderMenu)
    {
        this.itemInfo = new ItemInfo(itemInfo);
        this.isOrderMenu = isOrderMenu;

        setContent(); 
        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(onClickMaterial);
    }

    public void Initialization(MaterialItemManager materialItemManager, bool isOrderMenu)
    {
        this.itemInfo = new ItemInfo(materialItemManager.itemInfo);
        this.isOrderMenu = isOrderMenu;

        setContent();
        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(onClickMaterial);
    }

    public void InitializationShoppingBaske(ItemInfo iteminfo, int number)
    {
        this.itemInfo = new ItemInfo(iteminfo);

        setShoppingBaske(number);
        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(onClickMaterialInShoppingBaske);
    }

    public void setShoppingBaske(int number)
    {
        Debug.Log(number);
        itemInfo.AMOUNTNUMBER += number;
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = itemInfo.NAME + "    " + itemInfo.AMOUNTNUMBER + "개";
    }

    void setContent()
    {
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
        if (isOrderMenu)
            GetComponentInChildren<Text>().text = itemInfo.NAME + " " + itemInfo.BUYCOST + "골드";
    }

    void onClickMaterial()
    {
        ClickMaterial?.Invoke(this.itemInfo);
    }

    void onClickMaterialInShoppingBaske()
    {
        ClickMaterialInShoppingBaske?.Invoke(this);
        Destroy(this.gameObject);
    }

    public void setContetntProduction()
    {
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = itemInfo.NAME + "    " + itemInfo.AMOUNTNUMBER + "개";
    }
}
