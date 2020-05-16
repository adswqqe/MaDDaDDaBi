using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuManager : MonoBehaviour
{
    List<ProductionObjInfo> productionList;

    [SerializeField]
    Transform viewPort;
    [SerializeField]
    GameObject materialPrefab;

    public void Initialization(List<ProductionObjInfo> productionList)
    {
        this.productionList = productionList;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddMatrialViewPort(List<MaterialItemManager> curMaterialItems)
    {
        bool isHaved = false;


        foreach (var item in curMaterialItems)
        {
            isHaved = false;
            foreach (var curItem in curMaterialItems)
            {
                if (item.NAME == curItem.NAME)
                {
                    //Debug.Log(curItem.ITEMINFO.AMOUNTNUMBER);
                    //Debug.Log(item.ITEMINFO.AMOUNTNUMBER);
                    //curItem.ITEMINFO.AMOUNTNUMBER += 1;
                    //Debug.Log(item.ITEMINFO.AMOUNTNUMBER);
                    curItem.ITEMINFO.AMOUNTNUMBER += item.ITEMINFO.AMOUNTNUMBER;
                    isHaved = true;
                }
                Debug.Log(item.NAME);
            }
            if (!isHaved)
            {
                var tempGO = Instantiate(materialPrefab);
                MaterialItemManager temp = tempGO.GetComponent<MaterialItemManager>();
                tempGO.transform.SetParent(viewPort);
                temp.Initialization(item.ITEMINFO, false);     // 나중에 아이템이 갑자기 사라지는 버그 나올 듯.
            }
        }

    }

    //void OnClickBagMatrial(ItemInfo item)
    //{
    //    description.text = item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    //}
}
