﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecipeMenuManager : MonoBehaviour
{
    List<ProductionObjInfo> haveProductionList;
    List<ItemInfo> materialList;

    [SerializeField]
    GameObject[] pagesGo;
    [SerializeField]
    Text[] itemNames;
    [SerializeField]
    Image[] itemImages;
    [SerializeField]
    Text[] materialOneTexts;
    [SerializeField]
    Text[] materialTwoTexts;

    int pageNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialization(List<ItemInfo> materialList)
    {
        this.materialList = materialList;
    }

    void SetUIcontent()
    {
        for (int i = 0; i < haveProductionList.Count; i++)
        {
            int combinationIndex = 0;
            itemNames[i].enabled = true;
            itemImages[i].enabled = true;
            materialOneTexts[i].enabled = true;
            materialTwoTexts[i].enabled = true;

            Debug.Log(haveProductionList[i].COMBINATIONLIST.Count);
            Debug.Log(haveProductionList[i].COMBINATIONLIST[combinationIndex + 1]);

            itemNames[i].text = haveProductionList[i].ITEMINFO.NAME;
            itemImages[i].sprite = Resources.Load<Sprite>("ICON/" + haveProductionList[i].ITEMINFO.ICON_INDEX);
            materialOneTexts[i].text = GetMaterialName(haveProductionList[i].COMBINATIONLIST[combinationIndex++]);
            materialTwoTexts[i].text = GetMaterialName(haveProductionList[i].COMBINATIONLIST[combinationIndex]);

        }
    }

    string GetMaterialName(string id)
    {
        string name = "";
        foreach (var item in materialList)
        {
            if (id == item.ID.ToString())
            {
                name = item.NAME;
                break;
            }
        }

        return name;
    }

    public void GetHaveProductionList(List<ProductionObjInfo> list)
    {
        haveProductionList = list;

        SetUIcontent();
    }

    public void OnClickNextPagebtn()
    {
        pagesGo[pageNum++].SetActive(false);
        pagesGo[pageNum].SetActive(true);
    }
}
