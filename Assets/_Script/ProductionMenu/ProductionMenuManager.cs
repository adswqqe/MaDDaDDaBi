using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuManager : MonoBehaviour
{
    public Action<List<ItemInfo>, ProductionObjInfo> CreateProduction;
    public Action<int> ClickSlot;

    List<ProductionObjInfo> productionList;

    [SerializeField]
    Transform viewPort;
    [SerializeField]
    GameObject materialPrefab;

    [SerializeField]
    Image[] slot = new Image[6];
    [SerializeField]
    Image GamasotInIcon;
    [SerializeField]
    Image GamasotInIconOnItem;

    int curSelectIndex = 0;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> contents;
    List<ItemInfo> selectObj;

    List<ProductionObjInfo> haveProductionList;

    public void Initialization(List<ProductionObjInfo> productionList)
    {
        this.productionList = productionList;

    }
    // Start is called before the first frame update
    void Start()
    {
        contents = new List<GameObject>();
        selectObj = new List<ItemInfo>();
        haveProductionList = new List<ProductionObjInfo>();

        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(materialPrefab);
            tempGO.transform.SetParent(viewPort);
            contents.Add(tempGO);
            tempGO.GetComponent<ContentObjInfo>().ClickMaterial += OnClickMatrial;
            ClickSlot += tempGO.GetComponent<ContentObjInfo>().OnClickSlot;
            //tempGO.SetActive(true);
        }

    }

    public void OnAddMatrialViewPort(Data data)
    {
        for (int k = 0; k < contents.Count; k++)
        {
            contents[k].SetActive(false);
        }

        int i = 0;
        foreach (var item in data.CURMATERIALITELIST)
        {
            contents[i].GetComponent<ContentObjInfo>().Initialization(item.ITEMINFO);
            contents[i].SetActive(true);
            i++;
        }
        viewPort.GetComponent<GridLayoutGroup>().constraintCount = data.CURMATERIALITELIST.Count;
    }

    void OnClickMatrial(ItemInfo item)  // 재료 표시 함수
    {
        if (curSelectIndex >= 6)
            return;

        slot[curSelectIndex].sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX);
        selectObj.Add(item);

        curSelectIndex += 1;

        CheckGamasotInIcon();
    }

    void CheckGamasotInIcon()
    {
        List<string> tempIdList = new List<string>();
        List<string> tempCombiation;
        string iconIndex = "";
        bool isFind = false;

        foreach (var item in selectObj)
        {
            tempIdList.Add(item.ID.ToString());
        }

        foreach (var item in haveProductionList)
        {
            tempCombiation = new List<string>(item.COMBINATIONLIST);
            int conter = 0;
            int combiationCount = tempCombiation.Count;

            if (tempCombiation.Count != tempIdList.Count)
            {
                continue;
            }

            if (tempCombiation.Contains(tempIdList[0]))
            {
                for (int i = 0; i < combiationCount; i++)
                {
                    if (tempCombiation.Contains(tempIdList[i]))
                    {
                        tempCombiation.Remove(tempIdList[i]);
                        conter += 1;
                    }
                }

                if (combiationCount == conter)
                {
                    isFind = true;
                    iconIndex = item.ICON_INDEX;
                }
            }
        }

        if (isFind)
        {
            //   GamasotInIcon.sprite = Resources.Load<Sprite>("ICON/" + iconIndex);
            GamasotInIconOnItem.sprite = Resources.Load<Sprite>("ICON/" + iconIndex);
            GamasotInIconOnItem.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 1);
            GamasotInIcon.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 0);
        }
        else
        {
            GamasotInIcon.sprite = Resources.Load<Sprite>("ICON/" + "999");
            GamasotInIcon.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 1);
            GamasotInIconOnItem.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 0);
        }

        //GamasotInIcon.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 1);
    }

    public void ExitProductionMenu()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].sprite = Resources.Load<Sprite>("ICON/" + "9998");
        }
        curSelectIndex = 0;
        selectObj.Clear();

        GamasotInIcon.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 0);
        GamasotInIconOnItem.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 0);
    }

    public void OnClickCreateProduction()
    {
        if (selectObj.Count == 0)
            return;

        ProductionObjInfo production = null;

        List<string> tempIdList = new List<string>();
        List<string> tempCombiation;

        foreach (var item in selectObj)
        {
            tempIdList.Add(item.ID.ToString());
        }

        foreach (var item in productionList)
        {
            tempCombiation = new List<string>(item.COMBINATIONLIST);
            int conter = 0;
            int combiationCount = tempCombiation.Count;

            if (tempCombiation.Count != tempIdList.Count)
            {
                continue;
            }

            if (tempCombiation.Contains(tempIdList[0]))
            {
                for (int i = 0; i < combiationCount; i++)
                {
                    if(tempCombiation.Contains(tempIdList[i]))
                    {
                        conter += 1;
                        tempCombiation.Remove(tempIdList[i]);
                    }

                    if (combiationCount == conter)
                    {
                        production = new ProductionObjInfo(item);
                        Debug.Log(production.AMOUNTNUMBER + " 이름 " + production.NAME);
                    }
                }
            }
        }
        if (production != null)
            haveProductionList.Add(production);
        if (production != null)
        {
            //GamasotInIcon.sprite = Resources.Load<Sprite>("ICON/" + production.ICON_INDEX);
            GamasotInIconOnItem.sprite = Resources.Load<Sprite>("ICON/" + production.ICON_INDEX);

        }
        else
        {
            GamasotInIconOnItem.sprite = Resources.Load<Sprite>("ICON/" + "998"); // 쓰레기 이미지 출력
        }

        GamasotInIcon.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 0);
        GamasotInIconOnItem.color = new Color(GamasotInIcon.color.r, GamasotInIcon.color.g, GamasotInIcon.color.b, 1);
        CreateProduction?.Invoke(selectObj, production);
        selectObj.Clear();
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].sprite = Resources.Load<Sprite>("ICON/" + "9998");
        }
        curSelectIndex = 0;
    }

    public void OnClickSlot(int index)
    {
        if (slot[index].sprite.name == "9998")
            return;

        int id = 0;

        foreach (var item in selectObj)
        {
            if (item.ICON_INDEX == slot[index].sprite.name)
            {
                Debug.Log(slot[index].sprite.name);
                id = item.ID;
                selectObj.Remove(item);
                break;
            }
        }

        for (int j = 0; j < slot.Length; j++)
        {
            slot[j].sprite = Resources.Load<Sprite>("ICON/" + "9998");
        }

        int i = 0;
        foreach (var item in selectObj)
        {
            slot[i].sprite = Resources.Load<Sprite>("ICON/" + item.ICON_INDEX.ToString());
            i++;
        }

        curSelectIndex -= 1;
        ClickSlot?.Invoke(id);
    }
}
