using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentObjInfo : MonoBehaviour
{
    ItemInfo item;

    public void Initialization(ItemInfo item)
    {
        this.item = new ItemInfo(item);

        GetComponentInChildren<Image>().enabled = true;
        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
        GetComponentInChildren<Text>().text = item.NAME + "    " + item.AMOUNTNUMBER + "개";
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
