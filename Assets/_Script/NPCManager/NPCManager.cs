using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Action<ItemInfo> sellItem;

    [SerializeField]
    GameObject Npcprefab;
    [SerializeField]
    Transform spanPos;
    [SerializeField]
    Transform[] disaplyStandPos;
    
    bool isEndDay;
    bool isItemOn = false;
    float spawnCoolTime = 10.0f;
    int maxNPCCount = 5;
    int curNpcCount = 0;

    List<GameObject> npcs;
    List<Transform> targets;
    Transform target;

    List<DisplayItemCtrl> items;
    
    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<GameObject>();
        targets = new List<Transform>();
        items = new List<DisplayItemCtrl>();
        CreateNPC();
    }

    void CreateNPC()
    {
        for (int i = 0; i < maxNPCCount; i++)
        {
            GameObject tempgo = Instantiate(Npcprefab);
            tempgo.transform.position = spanPos.transform.position;
            tempgo.SetActive(false);
            npcs.Add(tempgo);
        }
    }

    public void OnDisableNPC(GameObject npcGO)
    {
        
    }

    public void OnSellingItem(ItemInfo item)
    {
        sellItem?.Invoke(item);
        Debug.Log("NPCMAger");
    }

    public void OnGetItemPos(DisplayItemCtrl item)
    {
        isItemOn = true;
        items.Add(item);
        target = item.ITEMPOS;
    }

    public void OnEndDay(bool isEndDay)
    {
        this.isEndDay = isEndDay;

        if (!isEndDay)
            StartCoroutine(StartNPCSpawn());
    }

    IEnumerator StartNPCSpawn()
    {
        while (true)
        {
            Debug.Log("테스ㅡㅌ");

            if (isEndDay)
                break;

            if (items.Count <= 0)
            {
                int index = UnityEngine.Random.Range(0, 4);
                target = disaplyStandPos[index];
                Debug.Log(index);
            }
            else
            {
                target = items[0].ITEMPOS;
                //items.RemoveAt(0);
                Debug.Log("target");
            }

            if (curNpcCount + 1 <= 5)
            {
                if (items.Count >= 1)
                {
                    npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, target, items[0], this);
                    items.RemoveAt(0);
                }
                else
                    npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, target, null, this);
                npcs[curNpcCount].SetActive(true);
                curNpcCount += 1;
            }
            else
            {
                curNpcCount = 0;
            }
            yield return new WaitForSeconds(spawnCoolTime);

        }

        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].SetActive(false);
        }
    }
}
