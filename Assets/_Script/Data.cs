using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    int lever;
    int exp;
    int gold;
    int reputation;
    int bagSpace;
    int max_bagSpace;
    List<MaterialItemManager> curMaterialItmList;
    List<MaterialItemManager> addMaterialItmList;
    List<ProductionObjInfo> curProductionItemList;
    List<FurnitureItem> curFurnitureItemItemList;
    List<GameObject> curDisplayFurnitureItemList;
    List<ProductionObjInfo> curWasteItemList;
    List<RequestCount> requestCounts;

    public Data(int lever, int exp, int gold, int reputation, int bagSpace, int max_bagSpace, List<MaterialItemManager> curMaterialItemList)
    {
        this.lever = lever;
        this.exp = exp;
        this.gold = gold;
        this.reputation = reputation;
        this.bagSpace = bagSpace;
        this.max_bagSpace = max_bagSpace;
        this.curMaterialItmList = curMaterialItemList;
        addMaterialItmList = new List<MaterialItemManager>();
        curProductionItemList = new List<ProductionObjInfo>();
        curFurnitureItemItemList = new List<FurnitureItem>();
        curDisplayFurnitureItemList = new List<GameObject>();
        curWasteItemList = new List<ProductionObjInfo>();
        requestCounts = new List<RequestCount>();
    }

    public Data(Data data)
    {
        this.lever = data.lever;
        this.exp = data.exp;
        this.gold = data.gold;
        this.reputation = data.reputation;
        this.bagSpace = data.bagSpace;
        this.max_bagSpace = data.max_bagSpace;
        this.curMaterialItmList = data.CURMATERIALITELIST;
        addMaterialItmList = new List<MaterialItemManager>();
        curProductionItemList = new List<ProductionObjInfo>();
        curFurnitureItemItemList = new List<FurnitureItem>();
        curDisplayFurnitureItemList = new List<GameObject>();
        curWasteItemList = new List<ProductionObjInfo>();
        requestCounts = new List<RequestCount>();
    }

    public int Level
    {
        set { lever = value; }
        get { return lever; }
    }

    public int EXP
    {
        set { exp = value; }
        get { return exp; }
    }

    public int GOLD
    {
        set { gold = value; }
        get { return gold; }
    }

    public int REPUTATION
    {
        set { reputation = value; }
        get { return reputation; }
    }

    public int BAGSPACE
    {
        set { bagSpace = value; }
        get { return bagSpace; }
    }

    public int MAX_BAGSPCE
    {
        set { max_bagSpace = value; }
        get { return max_bagSpace; }
    }

    public List<MaterialItemManager> CURMATERIALITELIST
    {
        get { return curMaterialItmList; }
    }

    public List<MaterialItemManager> ADDMATERIALLIST
    {
        get { return addMaterialItmList; }
    }

    public List<ProductionObjInfo> CURPRODUCTIONITEMLIST
    {
        get { return curProductionItemList; }
    }

    public List<FurnitureItem> CURFURNITUREITEMLIST
    {
        get { return curFurnitureItemItemList; }
    }

    public List<GameObject> CURDISPLAYFURNITUREITEMLIST
    {
        get { return curDisplayFurnitureItemList; }
    }
    
    public List<ProductionObjInfo> CURWASTEITEMLIST
    {
        get { return curWasteItemList; }
    }

    public List<RequestCount> REQUESTCOUNTS
    {
        get { return requestCounts; }
    }
}
