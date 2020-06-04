using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WorkstationItem : MonoBehaviour
{
    public Action<ProductionObjInfo> ClickItem;

    Text text;
    Image image;
    Button btn;
    ProductionObjInfo item;

    public void Initialization(ProductionObjInfo item)
    {
        this.item = new ProductionObjInfo(item);

        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        btn = GetComponentInChildren<Button>();

        btn.onClick.AddListener(OnClickItem);
        SetItem();
    }

    void SetItem()
    {
        text.text = item.NAME;
        image.sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX);
    }

    void OnClickItem()
    {
        ClickItem?.Invoke(item);
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }
}
