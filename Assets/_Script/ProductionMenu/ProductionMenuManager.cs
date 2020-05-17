using System;
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

    [SerializeField]
    Image[] slot = new Image[6];
    int curSelectIndex = 0;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> contents;
    List<ItemInfo> selectObj;

    public void Initialization(List<ProductionObjInfo> productionList)
    {
        this.productionList = productionList;

    }
    // Start is called before the first frame update
    void Start()
    {
        contents = new List<GameObject>();
        selectObj = new List<ItemInfo>();

        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(viewPort);
            contents.Add(tempGO);
            tempGO.GetComponent<ContentObjInfo>().ClickMaterial += OnClickMatrial;
            tempGO.SetActive(true);
        }
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

    void OnClickMatrial(ItemInfo item)
    {
        slot[curSelectIndex].sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
        selectObj.Add(item);

        curSelectIndex += 1;
    }

    public void ExitProductionMenu()
    {
        foreach (var item in slot)
        {
            item.sprite = null;
            curSelectIndex = 0;
            selectObj.Clear();
        }
    }

    public void CreateProduction()
    {
        string comId = "";
        bool isFind = false;

        List<string> tempIdList = new List<string>();

        foreach (var item in selectObj)
        {
            tempIdList.Add(item.ID.ToString());
        }

        foreach (var item in productionList)
        {
            int conter = 0;
            //if (isFind)
            //    break;

            Debug.Log(item.NAME);

            if (item.COMBINATIONLIST.Count != tempIdList.Count)
            {
                continue;
            }

            if (item.COMBINATIONLIST.Contains(tempIdList[0]))
            {
                Debug.Log(item.NAME);
                for (int i = 0; i < item.COMBINATIONLIST.Count; i++)
                {
                    if(item.COMBINATIONLIST.Contains(tempIdList[i]))
                    {
                        conter += 1;
                    }

                    if (item.COMBINATIONLIST.Count == conter)
                    {
                        isFind = true;
                        comId = item.NAME;
                    }
                }
            }
        }

        Debug.Log(comId);
    }
}
