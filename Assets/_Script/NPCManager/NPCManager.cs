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
    [SerializeField]
    Transform entrancePos;
    [SerializeField]
    Transform exitPos;

    bool isEndDay;
    bool isItemOn = false;
    float spawnCoolTime = 10.0f;
    int maxNPCCount = 5;
    int curNpcCount = 0;

    List<GameObject> npcs;
    List<Transform> targets;
    Transform target;

    List<GameObject> items;
    List<GameObject> furnitureitemList;
    
    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<GameObject>();
        targets = new List<Transform>();
        items = new List<GameObject>();
        CreateNPC();
    }

    void CreateNPC()
    {
        for (int i = 0; i < maxNPCCount; i++)
        {
            GameObject tempgo = Instantiate(Npcprefab);
            tempgo.transform.position = spanPos.transform.position;
            tempgo.GetComponent<NPCCtrl>().Initialization(spanPos, this, entrancePos, exitPos, i);
            tempgo.SetActive(false);
            npcs.Add(tempgo);
        }
    }

    public void OnGetFurnitureItem(Data data)
    {
        furnitureitemList = data.CURDISPLAYFURNITUREITEMLIST;

        if (furnitureitemList.Count >= 1)
            NpcUpdate();
    }

    public void OnDisableNPC(GameObject npcGO)
    {
        
    }

    public void OnSellingItem(GameObject item)
    {
        sellItem?.Invoke(item.GetComponent<DisplayItemCtrl>().ITEMINFO);
        Destroy(item);
        Debug.Log("NPCMAger");
    }

    public void OnGetItem(GameObject item)
    {
        isItemOn = true;
        items.Add(item);

        foreach (var furniture in furnitureitemList)
        {
            if (furniture.GetComponent<DisplayFurnitureItem>().isFull)
                continue;

            if(furniture.GetComponent<DisplayFurnitureItem>().
                ITEMINFO.NAME.Contains(item.GetComponent<DisplayItemCtrl>().ITEMINFO.SORT))
            {
                bool isSuccess = furniture.GetComponent<DisplayFurnitureItem>().OnDisplayItem(item);
                if (isSuccess)
                    break;
            }
        }
    }

    public void OnEndDay(bool isEndDay)
    {
        this.isEndDay = isEndDay;

        if (!isEndDay)
        {
            StartCoroutine("StartNPCSpawn");
        }
        else
        {
            StopCoroutine("StartNPCSpawn");

            if (npcs != null)
                for (int i = 0; i < npcs.Count; i++)
                {
                    npcs[i].SetActive(false);
                }
        }
    }

    void NpcUpdate()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].GetComponent<NPCCtrl>().SetFurnitureList(furnitureitemList);

            if (!npcs[i].GetComponent<NPCCtrl>().GetRetrun())
            {

            }
        }
    }
    IEnumerator StartNPCSpawn()
    {
        curNpcCount = 0;
        
        while (!isEndDay)
        {
            if(curNpcCount < npcs.Count)
            {
                npcs[curNpcCount].SetActive(true);
                npcs[curNpcCount].GetComponent<NPCCtrl>().OnStartNpc();
                curNpcCount++;
            }
            else
            {
                curNpcCount = 0;
            }


            //if (isEndDay)
            //    break;

            //if (items.Count <= 0 && furnitureitemList.Count <= 0)
            //{
            //    int index = UnityEngine.Random.Range(0, furnitureitemList.Count);
            //    target = disaplyStandPos[index];
            //    Debug.Log(index);
            //}
            //else if (items.Count <= 0 && furnitureitemList.Count >= 0)
            //{
            //    int index = UnityEngine.Random.Range(0, furnitureitemList.Count);
            //    target = disaplyStandPos[index];
            //    Debug.Log(index);
            //}
            //else
            //{
            //    target = items[0].ITEMPOS;
            //    //items.RemoveAt(0);
            //    Debug.Log("target");
            //}

            //if (curNpcCount + 1 <= 5)
            //{
            //    if (items.Count <= 0 && furnitureitemList.Count <= 0)
            //    {
            //        npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, null, null, this, entrancePos, exitPos);
            //    }
            //    else if (items.Count <= 0 && furnitureitemList.Count >= 0)
            //    {
            //        int index = UnityEngine.Random.Range(0, furnitureitemList.Count);
            //        target = furnitureitemList[index].transform;
            //        npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, target, null, this, entrancePos, exitPos);
            //    }
            //    else
            //    {
            //        npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, target, items[0], this, entrancePos, exitPos);
            //        items.RemoveAt(0);
            //    }
            //    npcs[curNpcCount].SetActive(true);
            //    curNpcCount += 1;
            //}
            //else
            //{
            //    curNpcCount = 0;
            //}
            yield return new WaitForSeconds(spawnCoolTime);

        }
    }
}
