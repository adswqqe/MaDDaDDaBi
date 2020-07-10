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
        item.transform.SetParent(transform);
        if (item.gameObject.name.Contains("301"))
        {
            item.transform.localPosition = new Vector3(-0.0002864352f, 1.013483f, 0.1088993f);
            item.transform.localEulerAngles = new Vector3(-65, 0, 0);
            item.transform.localScale = new Vector3(1.114803f, 1.295074f, 1.295074f);
        }
        else if(item.gameObject.name.Contains("303"))
        {
            item.transform.localPosition = new Vector3(0.01025567f, 1.574809f, 0.07122543f);
            item.transform.localEulerAngles = new Vector3(-87.36101f, -43.042f, 134.705f);
            item.transform.localScale = new Vector3(0.295215f, 0.295215f, 0.295215f);
        }
        else if(item.gameObject.name.Contains("304"))
        {
            item.transform.localPosition = new Vector3(0, 0.9378669f, 0.1006677f);
            item.transform.localEulerAngles = new Vector3(26.597f, 0, 0);
            item.transform.localScale = new Vector3(0.065764f, 0.065764f, 0.065764f);
        }
        else
        {
            item.transform.position = itemPos[itemIndex].position;
        }
        itemIndex++;
        Debug.Log(itemPos.Count);
        Debug.Log(itemIndex);

        if (itemIndex >= itemPos.Count)
            isFull = true;
        return true;
    }

    public GameObject SellItem()
    {
        itemIndex--;
        if (itemIndex <= 0)
            itemIndex = 0;
        isFull = false;
        GameObject temp = itemList[itemIndex];
        itemList.RemoveAt(itemIndex);
        if (itemList.Count == 0)
        {
            isHasItem = false;
        }

        SoundManager.instance.PlayEff(EffSound.SFX_UI_handleCoin);
        return temp;
    }

    public ItemInfo ITEMINFO
    {
        get { return itemInfo; }
    }

}
