using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureItem : MonoBehaviour
{
    ItemInfo itemInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialization(ItemInfo itemInfo)
    {
        this.itemInfo = new ItemInfo(itemInfo);
    }

    public ItemInfo ITEMINFO
    {
        get { return itemInfo; }
    }
}
