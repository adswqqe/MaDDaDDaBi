using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{
    public Action<int> cancelBuild;

    [SerializeField]
    GameObject[] furniturePrefabs;

    public GameObject 가구목록, 가구선택;
    public BuildSystem buildSystem;
    private bool 가구목록보이기 = true;
    private bool 가구선택보이기 = false;

    public int furnitureId = 0;
    public bool isBuildStart = false;

    private void Start()
    {
        가구선택.SetActive(false);
    }

    public void OnStartBuild(int id)//--this is what the buttons hook into
                                         //on the button click event in the inspector there is a place to 
                                         // add your own preview gameobject
    {
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
        //TogglePanel();
    }

    public void TogglePanel()
    {
        //가구목록보이기 = !가구목록보이기;
        //가구목록.SetActive(가구목록보이기);

        가구선택보이기 = !가구선택보이기;
        가구선택.SetActive(가구선택보이기);
        cancelBuild?.Invoke(0);
    }

}
