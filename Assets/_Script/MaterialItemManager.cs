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

    public void Initialization(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        setContent();

        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(onClickMaterial);
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
