using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPlayMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Transform potionViewPort;
    [SerializeField]
    Transform toolsViewPort;
    [SerializeField]
    Transform equipmentViewPort;

    const int MAX_CONTENT_SIZE = 30;

    List<GameObject> potionContents;
    List<GameObject> toolsContents;
    List<GameObject> equipmentContents;

    // Start is called before the first frame update
    void Start()
    {
        potionContents = new List<GameObject>();
        toolsContents = new List<GameObject>();
        equipmentContents = new List<GameObject>();

        CreateContents();
    }

    void CreateContents()
    {
        // 포션
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(potionViewPort);
            potionContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }

        // 도구
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(toolsViewPort);
            toolsContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }

        // 장비
        for (int i = 0; i < MAX_CONTENT_SIZE; i++)
        {
            var tempGO = Instantiate(itemPrefab);
            tempGO.transform.SetParent(equipmentViewPort);
            equipmentContents.Add(tempGO);
            tempGO.GetComponent<DisPlayItemInfo>().ClickItem += OnClickItem;
            //tempGO.SetActive(true);
        }
    }

    public void OnAddItemViewPort(Data data)
    {
        for (int k = 0; k < MAX_CONTENT_SIZE; k++)
        {
            potionContents[k].SetActive(false);
            toolsContents[k].SetActive(false);
            equipmentContents[k].SetActive(false);
        }

        int i = 0;
        foreach (var item in data.CURPRODUCTIONITEMLIST)
        {
            Debug.Log(item.SORT);

            if (item.SORT == "물약")
            {
                potionContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                potionContents[i].SetActive(true);
            }
            else if (item.SORT == "도구")
            {
                toolsContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                toolsContents[i].SetActive(true);
            }
            else if (item.SORT == "장비")
            {
                equipmentContents[i].GetComponent<DisPlayItemInfo>().Initialization(item.ITEMINFO);
                equipmentContents[i].SetActive(true);
            }

            i++;
        }
    }

    void OnClickItem(ItemInfo item)
    {
     //   description.text = item.AMOUNTNUMBER.ToString() + "개\n" + item.DESCRIPTION.ToString();
    }
}
