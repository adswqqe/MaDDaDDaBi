using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    string xmlFileName = "ITEMLIST_TEST";
    string productionObjXmlFileName = "CRAFTLIST_TEST";
    XmlNodeList all_nodes;
    XmlNodeList productionAllnodes;

    void Start()
    {
        //LoadXML(xmlFileName);
    }

    void LoadXML(string _fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(textAsset.text);
        xmlDoc.LoadXml(textAsset.text);

        // 하나씩 가져오기
        //XmlNodeList name_Table = xmlDoc.GetElementsByTagName("NAME");
        //foreach (XmlNode name in name_Table)
        //{
        //    Debug.Log("[one by one] name : " + name.InnerText);
        //}

        //// 전체 가져오기
        all_nodes = xmlDoc.SelectNodes("ITEMLIST/text");
        //foreach (XmlNode node in all_nodes)
        //{
        //    Debug.Log("[at once] id : " + node.SelectSingleNode("ID").InnerText);
        //    Debug.Log("[at once] SORT : " + node.SelectSingleNode("SORT").InnerText);
        //    Debug.Log("[at once] NAME : " + node.SelectSingleNode("NAME").InnerText);
        //    Debug.Log("[at once] ICON : " + node.SelectSingleNode("ICON").InnerText);
        //    Debug.Log("[at once] BUYCOST : " + node.SelectSingleNode("BUYCOST").InnerText);
        //    Debug.Log("[at once] SELLCOST : " + node.SelectSingleNode("SELLCOST").InnerText);
        //    Debug.Log("[at once] DESCRIPTION : " + node.SelectSingleNode("DESCRIPTION").InnerText);
        //}
    }

    public List<ItemInfo> GetOrderMaterial(string sortName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + xmlFileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(textAsset.text);
        xmlDoc.LoadXml(textAsset.text);
        all_nodes = xmlDoc.SelectNodes("ITEMLIST/text");

        List<ItemInfo> materialItems = new List<ItemInfo>();

        foreach (XmlNode node in all_nodes)
        {
            if (sortName == "재료")
            {
                if (node.SelectSingleNode("SORT").InnerText == sortName)
                {
                    ItemInfo item = new ItemInfo(
                        int.Parse(node.SelectSingleNode("ID").InnerText),
                        node.SelectSingleNode("SORT").InnerText,
                        node.SelectSingleNode("NAME").InnerText,
                        node.SelectSingleNode("ICON").InnerText,
                        int.Parse(node.SelectSingleNode("BUYCOST").InnerText),
                        0,      //재료이기 때문에 sellCost가 없다.
                        node.SelectSingleNode("DESCRIPTION").InnerText
                        );

                    materialItems.Add(item);
                }
            }
            else if(sortName == "제작")
            {
                if (node.SelectSingleNode("SORT").InnerText != "재료" && node.SelectSingleNode("SORT").InnerText != "가구")
                {
                    int number;
                    if(int.TryParse(node.SelectSingleNode("SELLCOST").InnerText, out number))
                    {
                        number = int.Parse(node.SelectSingleNode("SELLCOST").InnerText);
                    }
                    else
                    {
                        number = 0;
                    }
                    ItemInfo item = new ItemInfo(
                        int.Parse(node.SelectSingleNode("ID").InnerText),
                        node.SelectSingleNode("SORT").InnerText,
                        node.SelectSingleNode("NAME").InnerText,
                        node.SelectSingleNode("ICON").InnerText,
                        0,//int.Parse(node.SelectSingleNode("BUYCOST").InnerText),  //제작물이기 때문에 BUYCost가 없다.
                        number,//int.TryParse(node.SelectSingleNode("SELLCOST").InnerText),      
                        node.SelectSingleNode("DESCRIPTION").InnerText
                        );

                    materialItems.Add(item);
                }
            }
        }


        return materialItems;
    }

    public List<ProductionObjInfo> GetProductionObjInfo()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + productionObjXmlFileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(textAsset.text);
        xmlDoc.LoadXml(textAsset.text);
        productionAllnodes = xmlDoc.SelectNodes("CRAFTLIST/text");

        List<ProductionObjInfo> productionObjs = new List<ProductionObjInfo>();

        foreach (XmlNode node in productionAllnodes)
        {

            ProductionObjInfo item = new ProductionObjInfo(
                int.Parse(node.SelectSingleNode("ID").InnerText),
                node.SelectSingleNode("SORT").InnerText,
                node.SelectSingleNode("NAME").InnerText,
                node.SelectSingleNode("ICON").InnerText,
                node.SelectSingleNode("DESCRIPTION").InnerText,
                node.SelectSingleNode("needMaterial").InnerText
                );

            productionObjs.Add(item);

        }


        return productionObjs;
    }
}
