using System.Collections;
using System.Collections.Generic;


public class ProductionObjInfo
{
    int id;
    string sort;
    string name;
    string icon_index;
    string description;

    List<string> combinationList;

    string combinationId;

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
    }

    public string NAME
    {
        get { return name; }
    }

    public string COMVINATIONID
    {
        get { return combinationId; }
    }

    public List<string> COMBINATIONLIST
    {
        get { return combinationList; }
    }
}
