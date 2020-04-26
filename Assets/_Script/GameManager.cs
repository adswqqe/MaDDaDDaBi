using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIitemPrefabs;
    [SerializeField]
    Transform contentTr;
    [SerializeField]
    Text description;
    [SerializeField]
    Text materialName;
    [SerializeField]
    Image materialImage;
    [SerializeField]
    Button buyBtn;
    [SerializeField]
    Transform shoppingBaskeContentTr;
    [SerializeField]
    CountManager shoppingBakeCountManger;

    XMLManager xmlManager;
    OrderMaterialManager orderMaterialManager;
    // Start is called before the first frame update
    void Start()
    {
        xmlManager = gameObject.AddComponent<XMLManager>();
        orderMaterialManager = gameObject.AddComponent<OrderMaterialManager>();

        orderMaterialManager.Initialization(UIitemPrefabs, contentTr, xmlManager.GetOrderMaterial("재료"), description, materialName, materialImage, buyBtn, shoppingBaskeContentTr);

        Bind();
    }

    void Bind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn += orderMaterialManager.OnAddShoppingBaskeBtn;
    }

    void UnBind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn -= orderMaterialManager.OnAddShoppingBaskeBtn;
    }


}
