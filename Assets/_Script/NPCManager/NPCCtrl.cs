using System;
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
    Animator anim;
    SpeechManager speech;
    Data data;

    List<string> speechListUnder50;
    List<string> speechList50Morethan;
    List<string> speechList100Morethan;
    int speechIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        anim = GetComponent<Animator>();

        speechListUnder50 = new List<string>();
        speechList50Morethan = new List<string>();
        speechList100Morethan = new List<string>();

        speechListUnder50.Add("요즘 날씨가 좋아요.");
        speechListUnder50.Add("새로운 물약이 나왔으면 좋겠어요.");
        speechListUnder50.Add("좋은 하루 입니다!");
        speechListUnder50.Add("새로 시작한 잡화점인가요?");
        speechListUnder50.Add("잡화점이 너무 횅해요!");
        speechListUnder50.Add("상품이 없나요? 진열이 안 되어 있어요.");

        speechList50Morethan.Add("요즘 날씨가 좋아요.");
        speechList50Morethan.Add("새로운 물약이 나왔으면 좋겠어요.");
        speechList50Morethan.Add("좋은 하루 입니다!");
        speechList50Morethan.Add("안녕하세요! 소문 듣고 왔습니다.");
        speechList50Morethan.Add("잡화점이 너무 횅해요!");
        speechList50Morethan.Add("상품이 없나요? 진열이 안 되어 있어요.");

        speechList100Morethan.Add("요즘 날씨가 좋아요.");
        speechList100Morethan.Add("새로운 물약이 나왔으면 좋겠어요.");
        speechList100Morethan.Add("좋은 하루 입니다!");
        speechList100Morethan.Add("가구가 예쁜 것 같아요.");
        speechList100Morethan.Add("안녕하세요! 소문 듣고 왔습니다.");
        speechList100Morethan.Add("상품이 없나요? 진열이 안 되어 있어요.");
    }

    public void Initialization(Transform spawnPos, NPCManager npcManager,
                            Transform entrancePos, Transform exitPos, int index, SpeechManager speech)
    {
        this.spawnPos = spawnPos;
        this.entrancePos = entrancePos;
        this.exitPos = exitPos;
        this.index = index;
        this.speech = speech;

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

    public void GetData(Data data)
    {
        this.data = data;
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
        gameObject.layer = 11;
        isReturn = false;
        curTime = 0;
        roofIndex = 0;

        if (furnitureList.Count <= 0)
        {
            dumyTarget = entrancePos;
            Debug.Log(entrancePos);
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

    void setStartSpeech(bool isHasItem)
    {
        if (isHasItem)
        {
            speechIndex = UnityEngine.Random.Range(0, speechListUnder50.Count - 1);

            if (data.REPUTATION <= 49)
            {
                speech.OnStartSpeech(speechListUnder50[speechIndex], transform);
            }
            else if(data.REPUTATION >= 50 && data.REPUTATION <= 99)
            {
                speech.OnStartSpeech(speechList50Morethan[speechIndex], transform);
            }
            else if(data.REPUTATION >= 100)
            {
                speech.OnStartSpeech(speechList100Morethan[speechIndex], transform);
            }
        }
        else
        {
            speechIndex = UnityEngine.Random.Range(0, speechListUnder50.Count);

            if (data.REPUTATION <= 49)
            {
                speech.OnStartSpeech(speechListUnder50[speechIndex], transform);
            }
            else if (data.REPUTATION >= 50 && data.REPUTATION <= 99)
            {
                speech.OnStartSpeech(speechList50Morethan[speechIndex], transform);
            }
            else if (data.REPUTATION >= 100)
            {
                speech.OnStartSpeech(speechList100Morethan[speechIndex], transform);
            }
        }
    }

    public void OffSpeechActive()
    {
        speech.gameObject.SetActive(false);
    }

    // Update is called once per frame
                float dumyTimer = 0;
    void Update()
    {
        //anim.SetFloat("Speed", agent.speed);

        //if (agent.enabled =! false)
        //    anim.SetBool("walk", true);
        //else
        //    anim.SetBool("walk", false);

        if (agent.enabled == false)
        {
            curTime += Time.deltaTime;
            anim.SetTrigger("Idle");
            if (curTime >= returnTime)
            {
                obstacle.enabled = false;
                dumyTimer += Time.deltaTime;
                if (furnitureList.Count >= 1 && target == null)
                {
                    SetNpc();
                    agent.enabled = true;
                    agent.speed = 4;
                    dumyTimer = 0;
                }
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
                            anim.SetTrigger("Idle");
                            speech.OnStartSpeech("상품을 보고 싶은데 진열대가 안보여요!", transform);
                            agent.enabled = false;
                            obstacle.enabled = true;
                            curTime += Time.deltaTime;
                        }
                    }
                }
                else
                {
                    anim.SetTrigger("WalkTrigger");
                }
            }
        }
        else if(isNotFindItem)
        {
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
                                        setStartSpeech(true);
                                        //anim.SetTrigger("Idle");
                                        //agent.speed = 0;
                                        //agent.enabled = false;
                                        //obstacle.enabled = true;
                                    }
                                }
                                else
                                {
                                    SetNpc();
                                    setStartSpeech(false);
                                    //agent.speed = 0;
                                    //agent.enabled = false;
                                    //obstacle.enabled = true;
                                    //anim.SetTrigger("Idle");
                                }
                            }
                            else
                            {
                                target = spawnPos;
                                isReturn = true;
                            }

                            //agent.speed = 0;
                            //agent.enabled = false;
                            //obstacle.enabled = true;
                            //anim.SetTrigger("Idle");
                        }
                        else
                        {
                            agent.speed = 0;
                            agent.enabled = false;
                            obstacle.enabled = true;
                            transform.LookAt(target);
                            curTime += Time.deltaTime;
                            Debug.Log("asdasd");
                            anim.SetTrigger("Idle");
                        }
                    }
                }
                else
                {
                    anim.SetTrigger("WalkTrigger");
                }
            }
        }
    }
}
