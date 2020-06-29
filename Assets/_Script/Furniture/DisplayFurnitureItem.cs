using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFurnitureItem : MonoBehaviour
{
    ItemInfo itemInfo;

    List<Transform> itemPos;
    int itemIndex = 0;

    public bool isFull = false;
    public bool isHasItem = false;
    List<GameObject> itemList;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialization(ItemInfo itemInfo)
    {
        this.itemInfo = new ItemInfo(itemInfo);

        itemPos = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains("ItemPos"))
                itemPos.Add(transform.GetChild(i).transform);
        }

        for (int i = 0; i < itemPos.Count; i++)
        {
            Debug.Log("item Pos : " + itemPos[i].position);
        }
        itemList = new List<GameObject>();
        Debug.Log("가구 배치 : " + itemInfo.ID);
    }

    public bool OnDisplayItem(GameObject item)
    {
        if (itemIndex > itemPos.Count)
        {
            isFull = true;
            return false;
        }

        isHasItem = true;
        itemList.Add(item);
        item.transform.position = itemPos[itemIndex++].position;
        item.transform.SetParent(transform);
        Debug.Log(itemPos.Count);
        Debug.Log(itemIndex);

        if (itemIndex >= itemPos.Count)
            isFull = true;
        return true;
    }

    public GameObject SellItem()
    {
        itemIndex--;
        isFull = false;
        GameObject temp = itemList[itemIndex];
        itemList.RemoveAt(itemIndex);
        if (itemList.Count == 0)
        {
            isHasItem = false;
        }

        return temp;
    }

    public ItemInfo ITEMINFO
    {
        get { return itemInfo; }
    }

}
