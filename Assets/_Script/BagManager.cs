using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField]
    GameObject materialPrefab;
    [SerializeField]
    Transform materialViewPort;
    [SerializeField]
    Transform EquipmentViewPort;
    [SerializeField]
    Text description;
    List<MaterialItemManager> curMaterialItems;

    // Start is called before the first frame update
    void Start()
    {
        curMaterialItems = new List<MaterialItemManager>();
    }

    public void OnAddBagItem(Data data)
    {
        bool isHaved = false;

        if (curMaterialItems.Count == 0 && data.ADDMATERIALLIST.Count != 0) //맨 처음 추가용
        {
            foreach (var item in data.ADDMATERIALLIST)
            {
                var tempGO = Instantiate(materialPrefab);
                MaterialItemManager temp = tempGO.GetComponent<MaterialItemManager>();
                tempGO.transform.SetParent(materialViewPort);
                temp.Initialization(item.ITEMINFO, false);     // 나중에 아이템이 갑자기 사라지는 버그 나올 듯.
                temp.ClickMaterial += OnClickBagMatrial;
                curMaterialItems.Add(temp);
            }
            data.ADDMATERIALLIST.Clear();
        }
        else
        {
            foreach (var item in data.ADDMATERIALLIST)
            {
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
                }
                if (!isHaved)
                {
                    var tempGO = Instantiate(materialPrefab);
                    MaterialItemManager temp = tempGO.GetComponent<MaterialItemManager>();
                    tempGO.transform.SetParent(materialViewPort);
                    temp.Initialization(item.ITEMINFO, false);     // 나중에 아이템이 갑자기 사라지는 버그 나올 듯.
                    temp.ClickMaterial += OnClickBagMatrial;
                    curMaterialItems.Add(temp);
                }
            }
            data.ADDMATERIALLIST.Clear();
        }
    }

    void OnClickBagMatrial(ItemInfo item)
    {
       description.text = item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    }
}
