using System.Collections;
using System.Collections.Generic;

public class ItemInfo
{
    int id;
    string sort;
    string name;
    string icon_index;
    int buycost;
    int sellcost;
    string description;
    int amountNumber = 0;

    public ItemInfo(int id, string sort, string name, string icon_index, int buycost, int sellcost, string description)
    {
        this.id = id;
        this.sort = sort;
        this.name = name;
        this.icon_index = icon_index;
        this.buycost = buycost;
        this.sellcost = sellcost;
        this.description = description;
    }

    public ItemInfo(ItemInfo itemInfo)
    {
        this.id = itemInfo.ID;
        this.sort = itemInfo.SORT;
        this.name = itemInfo.NAME;
        this.icon_index = itemInfo.ICON_INDEX;
        this.buycost = itemInfo.BUYCOST;
        this.sellcost = itemInfo.SELLCOST;
        this.description = itemInfo.DESCRIPTION;
        this.amountNumber = itemInfo.AMOUNTNUMBER;
    }

    public string SORT
    {
        get { return sort; }
    }

    public int AMOUNTNUMBER
    {
        set { amountNumber = value; }
        get { return amountNumber; }
    }

    public int ID
    {
        set { id = value; }
        get { return id; }
    }
    public string NAME
    {
        set { name = value; }
        get { return name; }
    }
    public string ICON_INDEX
    {
        get { return icon_index; }
    }
    public int BUYCOST
    {
        set { buycost = value; }
        get { return buycost; }
    }
    public int SELLCOST
    {
        set { sellcost = value; }
        get { return sellcost; }
    }
    public string DESCRIPTION
    {
        set { description = value; }
        get { return description; }
    }

    
}
