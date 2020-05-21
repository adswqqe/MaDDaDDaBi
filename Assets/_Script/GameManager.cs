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
    [SerializeField]
    CameraManager cameraManager;
    [SerializeField]
    SceneTransitionManager sceneTransitionManager;

    XMLManager xmlManager;
    OrderMaterialManager orderMaterialManager;
    DataManager dataManager;
    UIManager uiManager;
    BagManager bagManaer;
    ProductionMenuManager productionMenuManager;
    TimeManager timeManager;
    DisPlayMenuManager displayMenuManager;

    // Start is called before the first frame update
    void Start()
    {
        xmlManager = gameObject.AddComponent<XMLManager>();
        orderMaterialManager = gameObject.AddComponent<OrderMaterialManager>();
        dataManager = gameObject.AddComponent<DataManager>();
        uiManager = GetComponent<UIManager>();
        bagManaer = GetComponent<BagManager>();
        productionMenuManager = GetComponent<ProductionMenuManager>();
        timeManager = GetComponent<TimeManager>();
        displayMenuManager = GetComponent<DisPlayMenuManager>();

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
        dataManager.changeData += productionMenuManager.OnAddMatrialViewPort;
        dataManager.changeData += displayMenuManager.OnAddItemViewPort;

        productionMenuManager.CreateProduction += dataManager.OnCreateProduction;

        timeManager.EndDayTime += uiManager.OnEndDay;
        timeManager.EndDayTime += cameraManager.OnEndDay;
        timeManager.EndDayTime += sceneTransitionManager.OnStartSceneTransition;
    }

    void UnBind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn -= orderMaterialManager.OnAddShoppingBaskeBtn;
        orderMaterialManager.buyButtonPress -= dataManager.CalcBuy;
        dataManager.resultCalcGold -= orderMaterialManager.OnbuySuccessMatreial;

        dataManager.changeData -= uiManager.OnChangeValueUI;
        dataManager.changeData -= bagManaer.OnAddBagItem;
        dataManager.changeData -= productionMenuManager.OnAddMatrialViewPort;
        dataManager.changeData -= displayMenuManager.OnAddItemViewPort;

        productionMenuManager.CreateProduction -= dataManager.OnCreateProduction;

        timeManager.EndDayTime -= uiManager.OnEndDay;
        timeManager.EndDayTime -= cameraManager.OnEndDay;
        timeManager.EndDayTime -= sceneTransitionManager.OnStartSceneTransition;
    }


}
