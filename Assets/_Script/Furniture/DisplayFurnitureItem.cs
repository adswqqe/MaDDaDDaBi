using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFurnitureItem : MonoBehaviour
{
    ItemInfo itemInfo;

    List<Transform> itemPos;
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
        Debug.Log("가구 배치 : " + itemInfo.ID);
    }

}
