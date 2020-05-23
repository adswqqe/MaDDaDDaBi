using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCtrl : MonoBehaviour
{
    public Action ArrivalItem;

    Transform target;
    NavMeshAgent agent;

    //
    Transform spawnPos;
    DisplayItemCtrl item;
    NPCManager npcManager;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Initialization(Transform spawnPos, Transform target, DisplayItemCtrl item, NPCManager npcManager)
    {
        this.spawnPos = spawnPos;
        this.target = target;
        this.item = item;

        this.npcManager = npcManager;

        if (item == null)
            return;

        ArrivalItem += item.OnSellItem;
        item.itemSell += npcManager.OnSellingItem;

        Debug.Log("item");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        agent.SetDestination(target.position);

        if(!agent.pathPending)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    if (target == spawnPos)
                    {
                        gameObject.SetActive(false);
                        if (item != null)
                        {
                            ArrivalItem -= item.OnSellItem;
                            item.itemSell -= npcManager.OnSellingItem;
                        }

                    }
                    else
                    {
                        target = spawnPos;

                        if (item != null)
                        {
                            Debug.Log("판매");
                            ArrivalItem?.Invoke();
                            
                        }
                    }
            }
        }
    }
}
