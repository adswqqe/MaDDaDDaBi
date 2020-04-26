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
    ItemInfo selectMaterialItem;
    Button buyBtn;
    Transform shoppingBaskeContentTr;
    List<MaterialItemManager> curShoppingBaskeList;

    bool isAlreadyAdded = false;

    public void Initialization(GameObject itemprefabs, Transform contentTr, List<ItemInfo> materialList, Text description, Text materialName, Image materialImage, Button buyBtn, Transform shoppingBaskeContentTr)
    {
        this.itemprefabs = itemprefabs;
        this.contentTr = contentTr;
        this.materialList = materialList;
        this.materialDescription = description;
        this.materialName = materialName;
        this.materialImage = materialImage;
        this.buyBtn = buyBtn;
        this.shoppingBaskeContentTr = shoppingBaskeContentTr;

        SetAddContent();

        materialName.enabled = false;       // 처음엔 아무것도 선택되지 않았기 떄문에 fasle;
        materialImage.enabled = false;
        materialDescription.enabled = false;

        curShoppingBaskeList = new List<MaterialItemManager>();
        buyBtn.onClick.AddListener(OnbuyBtn);
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
        selectMaterialItem = item;
        Debug.Log(selectMaterialItem);

        materialName.enabled = true;
        materialImage.enabled = true;
        materialDescription.enabled = true;

        materialImage.sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
        materialName.text = item.NAME;
        materialDescription.text = item.DESCRIPTION;
    }

    public void OnAddShoppingBaskeBtn(int number)
    {
        if (curShoppingBaskeList != null)
            foreach (var item in curShoppingBaskeList)
            {
                if (item.NAME == selectMaterialItem.NAME)
                {
                    isAlreadyAdded = true;
                    item.setShoppingBaske(number);
                }

            }

        if (!isAlreadyAdded)
        {
            var instance = Instantiate(itemprefabs);
            MaterialItemManager temp = instance.GetComponent<MaterialItemManager>();
            instance.GetComponent<MaterialItemManager>().InitializationShoppingBaske(selectMaterialItem, number);
            instance.transform.SetParent(shoppingBaskeContentTr);
            curShoppingBaskeList.Add(temp);
        }

        isAlreadyAdded = false;
    }

    void OnbuyBtn()     // 각 재료의 갯수가 저장되어 있으니 각 재료가 얼만지 계산해서 합산 한 뒤 결제하면 될 듯.
    {
        foreach (var item in curShoppingBaskeList)
        {
            Debug.Log(item.AMOUNTNUMBER);
        }
    }
}
