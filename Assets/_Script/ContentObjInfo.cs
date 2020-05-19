using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentObjInfo : MonoBehaviour
{
    public Action<ItemInfo> ClickMaterial;

    ItemInfo item;
    Button btn;

    int clickCount = 0;
    int amountNumber = 0;

    public void Initialization(ItemInfo item)
    {
        this.item = new ItemInfo(item);

        amountNumber = this.item.AMOUNTNUMBER;

        GetComponentInChildren<Image>().enabled = true;
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = item.NAME + "    " + amountNumber + "개";
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInChildren<Image>().sprite == null)
        {
            GetComponentInChildren<Image>().enabled = false;
            gameObject.SetActive(false);
        }
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onClickMaterial);
    }

    void onClickMaterial()
    {
        if (item == null)
            return;

        clickCount += 1;
        if ((amountNumber - clickCount) < 0)
            return;
        else
        {
            GetComponentInChildren<Text>().text = item.NAME + "    " + (amountNumber - clickCount) + "개";
            ClickMaterial?.Invoke(item);
        }
    }

    void OnEnable()
    {
        if (item != null)
        {
            amountNumber = this.item.AMOUNTNUMBER;
            clickCount = 0;
            GetComponentInChildren<Text>().text = item.NAME + "    " + amountNumber + "개";
        }
    }
}
