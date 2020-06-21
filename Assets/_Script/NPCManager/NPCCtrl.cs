﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCtrl : MonoBehaviour
{
    public Action ArrivalItem;

    Transform target;
    Transform dumyTarget;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    //
    Transform spawnPos;
    DisplayItemCtrl item;
    NPCManager npcManager;
    Transform entrancePos;
    Transform exitPos;

    float curTime = 0.0f;
    float returnTime = 3.0f;
    int roofIndex = 0;
    int maxRoofIndex = 2;
    int index = 0;
    bool isNotFindItem = true;
    bool isReturn = false;


    [SerializeField]
    float moveSpeed = 4;

    List<GameObject> furnitureList;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    public void Initialization(Transform spawnPos, NPCManager npcManager,
                            Transform entrancePos, Transform exitPos, int index)
    {
        this.spawnPos = spawnPos;
        this.entrancePos = entrancePos;
        this.exitPos = exitPos;
        this.index = index;

        this.npcManager = npcManager;
        furnitureList = new List<GameObject>();

        //if (item == null && target == null)
        //{
        //    this.target = entrancePos;
        //    return;
        //}
        //else if(item == null)
        //{
        //    this.target = target;
        //    isNotFindItem = true;
        //    return;
        //}
        //else
        //{
        //    this.target = target;
        //}
    }

    public void SetFurnitureList(List<GameObject> furnitureList)
    {
        this.furnitureList = furnitureList;
    }

    public void GetTargetPos(Transform targetTr)
    {
        target = targetTr;
    }

    public void SetTarget(Transform targetTr)
    {
        if (targetTr == null)
        {
            dumyTarget = entrancePos;
            agent.SetDestination(dumyTarget.position);
        }
        target = targetTr;
        curTime = 0;
    }

    public void OnStartNpc()
    {
        transform.position = spawnPos.position;
        isReturn = false;
        curTime = 0;
        roofIndex = 0;

        if (furnitureList.Count <= 0)
        {
            dumyTarget = entrancePos;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(dumyTarget.position);
        }
        else
        {
            SetNpc();
        }
    

    }

    public bool GetRetrun()
    {
        return isReturn;
    }

    public void SetNpc()
    {
        if (isNotFindItem)
        {
            int randIndex = UnityEngine.Random.Range(0, furnitureList.Count);
            target = furnitureList[randIndex].transform;
        }
    }

    bool buyOrNot()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        if (rand == 1)   //33퍼센트 확률
            return true;
        else
            return false;
    }

    // Update is called once per frame
                float dumyTimer = 0;
    void Update()
    {
        if(agent.enabled == false)
        {
            curTime += Time.deltaTime;

            if (curTime >= returnTime)
            {
                obstacle.enabled = false;
                dumyTimer += Time.deltaTime;
                if (dumyTimer >= 0.5f)
                {
                    //dumyTarget = spawnPos;
                    agent.enabled = true;
                    agent.speed = 5;
                    dumyTimer = 0;
                }
            }
            return;
        }

        if (target == null)
        {
            if (dumyTarget == null)
                return;
            //transform.LookAt(target);
            //transform.position = Vector3.Lerp(transform.position, dumyTarget.position, Time.deltaTime * moveSpeed);
            agent.SetDestination(dumyTarget.position);
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (dumyTarget == spawnPos)
                    {
                        //if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                        //{
                            gameObject.SetActive(false);
                            Debug.Log("순간이동");
                        //}
                    }
                    else
                    {
                        if (curTime >= returnTime)
                        {
                            dumyTarget = spawnPos;
                            isReturn = true;
                            Debug.Log("asdasd");
                            //agent.enabled = true;
                            //obstacle.enabled = false;
                        }
                        else
                        {
                            if (furnitureList.Count >= 1)
                            {
                                SetNpc();
                            }
                            agent.speed = 0;
                            agent.enabled = false;
                            obstacle.enabled = true;
                            curTime += Time.deltaTime;
                        }
                    }
                }
            }
        }
        else if(isNotFindItem)
        {
            Debug.Log("isNot");
            agent.SetDestination(target.position);
            transform.LookAt(target);
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    {
                        if(target == spawnPos)
                        {
                            gameObject.SetActive(false);
                        }

                        if (curTime >= returnTime)
                        {
                            if (roofIndex <= maxRoofIndex)
                            {
                                roofIndex++;
                                curTime = 0;
                                if (target.gameObject.GetComponent<DisplayFurnitureItem>().isHasItem)
                                {
                                    bool isBuy = buyOrNot();
                                    Debug.Log("isBuy :" + isBuy);
                                    if (isBuy)
                                    {
                                        npcManager.OnSellingItem(target.gameObject.GetComponent<DisplayFurnitureItem>().SellItem());
                                        target = spawnPos;
                                        isReturn = true;
                                    }
                                    else
                                    {
                                        SetNpc();

                                        agent.speed = 0;
                                        agent.enabled = false;
                                        obstacle.enabled = true;
                                    }
                                }
                                else
                                {
                                    SetNpc();

                                    agent.speed = 0;
                                    agent.enabled = false;
                                    obstacle.enabled = true;
                                }
                            }
                            else
                            {
                                target = spawnPos;
                                isReturn = true;
                            }

                            agent.speed = 0;
                            agent.enabled = false;
                            obstacle.enabled = true;
                        }
                        else
                        {
                            transform.LookAt(target);
                            curTime += Time.deltaTime;
                        }
                    }
                }
            }
        }

        //agent.SetDestination(target.position);

        //if(!agent.pathPending)
        //{
        //    if(agent.remainingDistance <= agent.stoppingDistance)
        //    {
        //        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
        //            if (target == spawnPos)
        //            {
        //                gameObject.SetActive(false);
        //                if (item != null)
        //                {
        //                    ArrivalItem -= item.OnSellItem;
        //                    item.itemSell -= npcManager.OnSellingItem;
        //                }

        //            }
        //            else
        //            {
        //                if (curTime >= returnTime)
        //                {
        //                    if (isNotFindItem)
        //                    {
        //                        if (index <= 3)
        //                        {
        //                            index++;
        //                            curTime = 0;
        //                            isNotFindItem = false;
        //                            return;
        //                        }
        //                        Debug.Log("asdasd");
        //                    }
        //                    else
        //                    {
        //                        target = spawnPos;
        //                        if (item != null)
        //                        {
        //                            Debug.Log("판매");
        //                            ArrivalItem?.Invoke();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    curTime += Time.deltaTime;
        //                }
        //            }
        //    }
        //}
    }
}
