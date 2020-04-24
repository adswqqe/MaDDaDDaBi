using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItemManager : MonoBehaviour
{
    Text descriptionText;
    ItemInfo itemInfo;
    Button m_bt;

    public void Initialization(Text descriptionText, ItemInfo itemInfo)
    {
        this.descriptionText = descriptionText;
        this.itemInfo = itemInfo;
        setContent();

        m_bt = GetComponent<Button>();
        m_bt.onClick.AddListener(OnClick);
    }

    void setContent()
    {
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + 1.ToString());
        GetComponentInChildren<Text>().text = itemInfo.NAME + " " + itemInfo.BUYCOST + "골드";
    }

    void OnClick()
    {
        descriptionText.text = itemInfo.DESCRIPTION;
    }
}
