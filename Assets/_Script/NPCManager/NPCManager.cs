using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Action<ItemInfo> sellItem;

    [SerializeField]
    GameObject[] Npcprefab;
    [SerializeField]
    Transform spanPos;
    [SerializeField]
    Transform[] disaplyStandPos;
    [SerializeField]
    Transform entrancePos;
    [SerializeField]
    Transform exitPos;
    [SerializeField]
    SpeechManager[] speechmanagers;

    bool isEndDay;
    bool isItemOn = false;
    float spawnCoolTime = 10.0f;
    int maxNPCCount = 5;
    int curNpcCount = 0;
    int maxActiveNpcNum = 3;
    int wasteCount = 0;    

    List<GameObject> npcs;
    List<Transform> targets;
    Transform target;

    List<GameObject> items;
    List<GameObject> furnitureitemList;
    List<int> randIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<GameObject>();
        targets = new List<Transform>();
        items = new List<GameObject>();
        randIndex = new List<int>();
        CreateNPC();
    }

    void CreateNPC()
    {
        int index = 1;
        for (int i = 0; i < maxNPCCount; i++)
        { 

            GameObject tempgo = Instantiate(Npcprefab[i]);
            tempgo.transform.position = spanPos.transform.position;
            tempgo.GetComponent<NPCCtrl>().Initialization(spanPos, this, entrancePos, exitPos, i, speechmanagers[i]);
            tempgo.SetActive(false);
            npcs.Add(tempgo);
        }
    }

    public void OnGetFurnitureItem(Data data)
    {
        furnitureitemList = data.CURDISPLAYFURNITUREITEMLIST;

        if (furnitureitemList.Count >= 1)
            NpcUpdate();

        wasteCount = data.CURWASTEITEMLIST.Count;

        if (wasteCount <= 4)
            spawnCoolTime = 10;
        else if (wasteCount >= 5 && wasteCount <= 9)
            spawnCoolTime = 12;
        else if (wasteCount >= 10 && wasteCount <= 19)
            spawnCoolTime = 16;
        else if (wasteCount >= 20)
            spawnCoolTime = 18;

        foreach (var item in npcs)
        {
            item.GetComponent<NPCCtrl>().GetData(data);
        }

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
                    npcs[i].GetComponent<NPCCtrl>().OffSpeechActive();
                    npcs[i].SetActive(false);
                }

            if (randIndex != null)
                randIndex.Clear();
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

    bool CheckActiveNpc()
    {
        int activeCount = 0;
        for (int i = 0; i < npcs.Count; i++)
        {
            if (npcs[i].activeSelf)
                activeCount++;
        }

        if (activeCount >= maxActiveNpcNum)
            return false;
        else
            return true;
    }

    int RandNpcIndex()
    {
        bool isFind = true;
        int rand = 0;


        while (isFind)
        {
            rand = UnityEngine.Random.Range(0, 4);
            if (randIndex.Contains(rand) && npcs[rand].activeSelf == true)
                continue;
            else
            {
                isFind = false;
                randIndex.Add(rand);
            }
        }
        Debug.Log(rand);
        return rand;
    }

    IEnumerator StartNPCSpawn()
    {
        curNpcCount = 0;
        int index = 0;
        while (!isEndDay)
        {
            if(curNpcCount < npcs.Count)
            {
                if (CheckActiveNpc())
                {
                    index = RandNpcIndex();
                    npcs[index].SetActive(true);
                    npcs[index].GetComponent<NPCCtrl>().OnStartNpc();
                    curNpcCount++;
                }
            }
            else
            {
                curNpcCount = 0;
                randIndex.Clear();
            }

            yield return new WaitForSeconds(spawnCoolTime);

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

        }
    }
}
