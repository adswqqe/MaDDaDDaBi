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
