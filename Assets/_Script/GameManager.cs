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
    Button choiceBtn;
    [SerializeField]
    Transform shoppingBaskeContentTr;
    [SerializeField]
    CountManager shoppingBakeCountManger;

    XMLManager xmlManager;
    OrderMaterialManager orderMaterialManager;
    DataManager dataManager;
    UIManager uiManager;
    BagManager bagManaer;
    ProductionMenuManager productionMenuManager;
    // Start is called before the first frame update
    void Start()
    {
        xmlManager = gameObject.AddComponent<XMLManager>();
        orderMaterialManager = gameObject.AddComponent<OrderMaterialManager>();
        dataManager = gameObject.AddComponent<DataManager>();
        uiManager = GetComponent<UIManager>();
        bagManaer = GetComponent<BagManager>();
        productionMenuManager = GetComponent<ProductionMenuManager>();

        orderMaterialManager.Initialization(UIitemPrefabs, contentTr, xmlManager.GetOrderMaterial("재료"), description, materialName, materialImage, buyBtn, shoppingBaskeContentTr, choiceBtn);
        productionMenuManager.Initialization(xmlManager.GetProductionObjInfo());
        Bind();
    }

    void Bind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn += orderMaterialManager.OnAddShoppingBaskeBtn;
        orderMaterialManager.buyButtonPress += dataManager.CalcBuy;
        dataManager.resultCalcGold += orderMaterialManager.OnbuySuccessMatreial;
        dataManager.changeData += uiManager.OnChangeValueUI;
        dataManager.changeData += bagManaer.OnAddBagItem;
        bagManaer.ChageBag += productionMenuManager.OnAddMatrialViewPort;

    }

    void UnBind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn -= orderMaterialManager.OnAddShoppingBaskeBtn;
        orderMaterialManager.buyButtonPress -= dataManager.CalcBuy;
        dataManager.resultCalcGold -= orderMaterialManager.OnbuySuccessMatreial;
        dataManager.changeData -= uiManager.OnChangeValueUI;
        dataManager.changeData -= bagManaer.OnAddBagItem;
        bagManaer.ChageBag -= productionMenuManager.OnAddMatrialViewPort;
    }


}
