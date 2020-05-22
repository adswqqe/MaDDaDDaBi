using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    GameObject Npcprefab;
    [SerializeField]
    Transform spanPos;
    [SerializeField]
    Transform[] disaplyStandPos;
    
    bool isEndDay;
    bool isTargetOn = false;
    float spawnCoolTime = 10.0f;
    int maxNPCCount = 5;
    int curNpcCount = 0;

    List<GameObject> npcs;
    Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<GameObject>();
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

    public void OnGetItemPos(Transform pos)
    {
        isTargetOn = true;
        target = pos;
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

            if (!isTargetOn)
            {
                int index = Random.Range(0, 4);
                target = disaplyStandPos[index];
                Debug.Log(index);
            }
            else
                isTargetOn = false;

            if (curNpcCount + 1 <= 5)
            {
                npcs[curNpcCount].GetComponent<NPCCtrl>().Initialization(spanPos, target);
                npcs[curNpcCount].SetActive(true);
                curNpcCount += 1;
            }
            yield return new WaitForSeconds(spawnCoolTime);

        }

        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].SetActive(false);
        }
    }
}
