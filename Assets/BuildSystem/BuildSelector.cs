using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{
    public Action<int, int> cancelBuild;

    [SerializeField]
    GameObject[] furniturePrefabs;

    public GameObject FurnitureSelection; //가구선택
    public BuildSystem buildSystem;
    private bool FurnitureSelectionState = false; //가구선택 보이기

    public int furnitureId = 0;
    public bool isBuildStart = false;

    private void Start()
    {
        FurnitureSelection.SetActive(false);
    }

    public void OnStartBuild(int id)//--this is what the buttons hook into
                                    //on the button click event in the inspector there is a place to 
                                    // add your own preview gameobject
    {
        buildSystem.Relocation = true;
        GameObject go = null;
        furnitureId = id;
        foreach (var item in furniturePrefabs)
        {
            if (item.name == id.ToString())
                go = item;
        }

        if (go != null)
        {
            buildSystem.curfurnGo = Instantiate(go, new Vector3(0, -6f, 0), transform.rotation);
            TogglePanel();
        }

        isBuildStart = true;
        //buildSystem.NewBuild(go);//this "Starts" a new build in the build system
    }

    public void TogglePanel()
    {
        FurnitureSelectionState = !FurnitureSelectionState;
        FurnitureSelection.SetActive(FurnitureSelectionState);
        cancelBuild?.Invoke(0, 0);
    }

}
