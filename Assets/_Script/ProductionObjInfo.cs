using System.Collections;
using System.Collections.Generic;


public class ProductionObjInfo
{
    int id;
    string sort;
    string name;
    string icon_index;
    string description;

    string oneMaterial;     // 배열로 교체하던가 하는 방법을 고안하는 것도 괜찮을 듯.
    string twoMaterial;
    string threeMaterial;
    string foruMaterial;
    string fiveMaterial;
    string sixMaterial;

    public ProductionObjInfo(int id, string sort, string name, string icon_index, string description, string oneMaterial, string twoMaterial, string threeMaterial, string foruMaterial, string fiveMaterial, string sixMaterial)
    {
        this.id = id;
        this.sort = sort;
        this.name = name;
        this.icon_index = icon_index;
        this.description = description;
        this.oneMaterial = oneMaterial;
        this.twoMaterial = twoMaterial;
        this.threeMaterial = threeMaterial;
        this.foruMaterial = foruMaterial;
        this.fiveMaterial = fiveMaterial;
        this.sixMaterial = sixMaterial;
    }

    public string NAME
    {
        get { return name; }
    }
}
