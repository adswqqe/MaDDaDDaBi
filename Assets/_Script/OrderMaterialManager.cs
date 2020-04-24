using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMaterialManager : MonoBehaviour
{
    GameObject itemprefabs;
    Transform contentTr;
    List<ItemInfo> materialList;
    GameObject description;


    public void Initialization(GameObject itemprefabs, Transform contentTr, List<ItemInfo> materialList, GameObject description)
    {
        this.itemprefabs = itemprefabs;
        this.contentTr = contentTr;
        this.materialList = materialList;
        this.description = description;
        SetContent();
    }

    void SetContent()
    {
        contentTr.GetComponent<GridLayoutGroup>().constraintCount = materialList.Count;

        foreach (var item in materialList)
        {
            var instance = Instantiate(itemprefabs);
            instance.GetComponent<MaterialItemManager>().Initialization(description.GetComponent<Text>(), item);
            instance.transform.SetParent(contentTr);
            //instance.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + 1.ToString());
            //instance.GetComponentInChildren<Text>().text = item.NAME + " " + item.BUYCOST + "골드";

        }
    }
}
