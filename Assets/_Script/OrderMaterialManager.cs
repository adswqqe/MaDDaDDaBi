using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMaterialManager : MonoBehaviour
{
    Image materialImage;
    Text materialName;
    GameObject itemprefabs;
    Transform contentTr;
    Text materialDescription;
    List<ItemInfo> materialList;
    ItemInfo materialItem;

    public void Initialization(GameObject itemprefabs, Transform contentTr, List<ItemInfo> materialList, Text description, Text materialName, Image materialImage)
    {
        this.itemprefabs = itemprefabs;
        this.contentTr = contentTr;
        this.materialList = materialList;
        this.materialDescription = description;
        this.materialName = materialName;
        this.materialImage = materialImage;

        SetAddContent();

        materialName.enabled = false;       // 처음엔 아무것도 선택되지 않았기 떄문에 fasle;
        materialImage.enabled = false;
        materialDescription.enabled = false;
    }

    void SetAddContent()
    {
        contentTr.GetComponent<GridLayoutGroup>().constraintCount = materialList.Count;

        foreach (var item in materialList)
        {
            var instance = Instantiate(itemprefabs);
            instance.GetComponent<MaterialItemManager>().Initialization(item);
            instance.GetComponent<MaterialItemManager>().ClickMaterial += OnMaterialClick;
            instance.transform.SetParent(contentTr);
            //instance.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("ICON/" + 1.ToString());
            //instance.GetComponentInChildren<Text>().text = item.NAME + " " + item.BUYCOST + "골드";

        }
    }

    private void OnDestroyMaterial(MaterialItemManager item)
    {
        item.ClickMaterial -= this.OnMaterialClick;
    }

    void OnMaterialClick(ItemInfo item)
    {
        materialItem = item;

        materialName.enabled = true;
        materialImage.enabled = true;
        materialDescription.enabled = true;

        materialImage.sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
        materialName.text = item.NAME;
        materialDescription.text = item.DESCRIPTION;
    }
}
