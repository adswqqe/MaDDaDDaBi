using System.Collections;
using System.Collections.Generic;


public class ProductionObjInfo
{
    int id;
    string sort;
    string name;
    string icon_index;
    string description;
    int amountNumber;

    List<string> combinationList;

    ItemInfo ItemInfo;

    public ProductionObjInfo(int id, string sort, string name, string icon_index, string description, string combinationString)
    {
        combinationList = new List<string>();

        this.id = id;
        this.sort = sort;
        this.name = name;
        this.icon_index = icon_index;
        this.description = description;

        string[] temp = combinationString.Split(',');

        for (int i = 0; i < temp.Length; i++)
        {
            combinationList.Add(temp[i]);
        }

        ItemInfo = new ItemInfo(id, sort, name, icon_index, 0, 0, description);
        ItemInfo.AMOUNTNUMBER = 1;
        //amountNumber = 1;
    }

    public ProductionObjInfo(ProductionObjInfo info)
    {
        this.id = info.ID;
        this.sort = info.SORT;
        this.name = info.NAME;
        this.icon_index = info.ICON_INDEX;
        this.description = info.DESCRIPTION;

        combinationList = new List<string>();
        foreach (var item in info.COMBINATIONLIST)
        {
            combinationList.Add(item);
        }
        amountNumber = info.AMOUNTNUMBER;

        ItemInfo = new ItemInfo(id, sort, name, icon_index, 0, 0, description);
        ItemInfo.AMOUNTNUMBER = info.AMOUNTNUMBER;
    }

    public int AMOUNTNUMBER
    {
        get { return ItemInfo.AMOUNTNUMBER; }
    }

    public string NAME
    {
        get { return name; }
    }

    public string SORT
    {
        get { return sort; }
    }

    public int ID
    {
        get { return id; }
    }

    public string ICON_INDEX
    {
        get { return icon_index; }
    }

    public string DESCRIPTION
    {
        get { return description; }
    }

    public List<string> COMBINATIONLIST
    {
        get { return combinationList; }
    }

    public ItemInfo ITEMINFO
    {
        get { return ItemInfo; }
    }
}
