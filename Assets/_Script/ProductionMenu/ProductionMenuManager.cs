using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuManager : MonoBehaviour
{
    List<ProductionObjInfo> productionList;

    [SerializeField]
    Transform viewPort;
    [SerializeField]
    GameObject materialPrefab;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> contents;

    public void Initialization(List<ProductionObjInfo> productionList)
    {
        this.productionList = productionList;

    }
    // Start is called before the first frame update
    void Start()
    {
        contents = new List<GameObject>();

        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(viewPort);
            contents.Add(tempGO);
            tempGO.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddMatrialViewPort(List<MaterialItemManager> curMaterialItems)
    {
        int i = 0;
        foreach (var item in curMaterialItems)
        {
            contents[i].GetComponent<ContentObjInfo>().Initialization(item.ITEMINFO);
            i++;
            Debug.Log(item.NAME);
        }
        viewPort.GetComponent<GridLayoutGroup>().constraintCount = curMaterialItems.Count;


    }

    //void OnClickBagMatrial(ItemInfo item)
    //{
    //    description.text = item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    //}
}
