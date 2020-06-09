﻿using System.Collections;
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
    [SerializeField]
    BuildSystem buildSystem;
    [SerializeField]
    BuildSelector buildSelector;

    XMLManager xmlManager;
    OrderMaterialManager orderMaterialManager;
    DataManager dataManager;
    UIManager uiManager;
    BagManager bagManaer;
    ProductionMenuManager productionMenuManager;
    TimeManager timeManager;
    DisPlayMenuManager displayMenuManager;
    NPCManager npcManager;
    WorkstationManager workstationManager;
    FurnitureDisplayManager furnitureDisplayManager;

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
        npcManager = GetComponent<NPCManager>();
        workstationManager = GetComponent<WorkstationManager>();
        furnitureDisplayManager = GetComponent<FurnitureDisplayManager>();

        orderMaterialManager.Initialization(UIitemPrefabs, contentTr, xmlManager.GetOrderMaterial("재료"), description, materialName, materialImage, buyBtn, shoppingBaskeContentTr, choiceBtn);
        productionMenuManager.Initialization(xmlManager.GetProductionObjInfo());
        dataManager.Initialization(xmlManager.GetOrderMaterial("제작"), xmlManager.GetProductionObjInfo());
        workstationManager.Initialization(xmlManager.GetProductionObjInfo(), xmlManager.GetOrderMaterial("재료"));

        Bind();
    }

    void Bind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn += orderMaterialManager.OnAddShoppingBaskeBtn;
        orderMaterialManager.buyButtonPress += dataManager.CalcBuy;
        dataManager.resultCalcGold += orderMaterialManager.OnbuySuccessMatreial;

        dataManager.changeData += uiManager.OnChangeValueUI;
        dataManager.changeData += bagManaer.OnAddBagItem;
        dataManager.changeData += furnitureDisplayManager.OnAddBagItem;
        dataManager.changeData += productionMenuManager.OnAddMatrialViewPort;
        dataManager.changeData += displayMenuManager.OnAddItemViewPort;
        dataManager.changeData += workstationManager.OnGetData;

        productionMenuManager.CreateProduction += dataManager.OnCreateProduction;

        timeManager.EndDayTime += uiManager.OnEndDay;
        timeManager.EndDayTime += cameraManager.OnEndDay;
        timeManager.EndDayTime += sceneTransitionManager.OnStartSceneTransition;
        timeManager.EndDayTime += npcManager.OnEndDay;

        displayMenuManager.DisplayItemObj += dataManager.OnDisplayItemObj;
        displayMenuManager.DisPlayItem += npcManager.OnGetItemPos;

        npcManager.sellItem += dataManager.OnSellItem;

        workstationManager.ClickCrateBtnInWorkstation += dataManager.OnCreateInWorkStation;

        furnitureDisplayManager.StartBuild += buildSelector.OnStartBuild;

        buildSystem.ConfirmationFurniture += dataManager.OnConfirmationFurniture;
    }

    void UnBind()
    {
        shoppingBakeCountManger.clickAddSoppingBaskeBtn -= orderMaterialManager.OnAddShoppingBaskeBtn;
        orderMaterialManager.buyButtonPress -= dataManager.CalcBuy;
        dataManager.resultCalcGold -= orderMaterialManager.OnbuySuccessMatreial;

        dataManager.changeData -= uiManager.OnChangeValueUI;
        dataManager.changeData -= bagManaer.OnAddBagItem;
        dataManager.changeData -= furnitureDisplayManager.OnAddBagItem;
        dataManager.changeData -= productionMenuManager.OnAddMatrialViewPort;
        dataManager.changeData -= displayMenuManager.OnAddItemViewPort;
        dataManager.changeData -= workstationManager.OnGetData;

        productionMenuManager.CreateProduction -= dataManager.OnCreateProduction;

        timeManager.EndDayTime -= uiManager.OnEndDay;
        timeManager.EndDayTime -= cameraManager.OnEndDay;
        timeManager.EndDayTime -= sceneTransitionManager.OnStartSceneTransition;
        timeManager.EndDayTime -= npcManager.OnEndDay;

        displayMenuManager.DisplayItemObj -= dataManager.OnDisplayItemObj;
        displayMenuManager.DisPlayItem -= npcManager.OnGetItemPos;

        npcManager.sellItem -= dataManager.OnSellItem;

        workstationManager.ClickCrateBtnInWorkstation -= dataManager.OnCreateInWorkStation;

        furnitureDisplayManager.StartBuild -= buildSelector.OnStartBuild;

        buildSystem.ConfirmationFurniture -= dataManager.OnConfirmationFurniture;
    }


}
