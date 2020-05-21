using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisPlayItemInfo : MonoBehaviour
{
    public Action<ItemInfo> ClickItem;

    ItemInfo itemInfo;
    Button btn;
    Image img;

    public void Initialization(ItemInfo itemInfo)
    {
        this.itemInfo = new ItemInfo(itemInfo);

        img = GetComponentInChildren<Image>();

        img.enabled = true;
        img.sprite = Resources.Load<Sprite>("ICON/" + this.itemInfo.ICON_INDEX.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickBtn);

        img = GetComponentInChildren<Image>();

        if (img.sprite == null)
        {
            gameObject.SetActive(false);
            img.enabled = false;
        }
    }

    void OnClickBtn()
    {
        if (itemInfo == null)
            return;

        ClickItem?.Invoke(this.itemInfo);
    }

    void OnEnable()
    {
        if (itemInfo == null)
        {
            return;
        }

        img.sprite = Resources.Load<Sprite>("ICON/" + itemInfo.ICON_INDEX.ToString());
    }

}
