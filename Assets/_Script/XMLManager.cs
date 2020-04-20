using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    string xmlFileName = "ITEMLIST_TEST";
    void Start()
    {
        LoadXML(xmlFileName);
    }

    void LoadXML(string _fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(textAsset.text);
        xmlDoc.LoadXml(textAsset.text);

        // 하나씩 가져오기
        XmlNodeList name_Table = xmlDoc.GetElementsByTagName("NAME");
        foreach (XmlNode name in name_Table)
        {
            Debug.Log("[one by one] name : " + name.InnerText);
        }

        // 전체 가져오기
        XmlNodeList all_nodes = xmlDoc.SelectNodes("ITEMLIST/text");
        foreach (XmlNode node in all_nodes)
        {
            Debug.Log("[at once] id : " + node.SelectSingleNode("ID").InnerText);
            Debug.Log("[at once] SORT : " + node.SelectSingleNode("SORT").InnerText);
            Debug.Log("[at once] NAME : " + node.SelectSingleNode("NAME").InnerText);
            Debug.Log("[at once] ICON : " + node.SelectSingleNode("ICON").InnerText);
            Debug.Log("[at once] BUYCOST : " + node.SelectSingleNode("BUYCOST").InnerText);
            Debug.Log("[at once] SELLCOST : " + node.SelectSingleNode("SELLCOST").InnerText);
            Debug.Log("[at once] DESCRIPTION : " + node.SelectSingleNode("DESCRIPTION").InnerText);
        }
    }
}
