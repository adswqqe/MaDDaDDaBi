using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public Action<int, int, int> RequestSuccess;
    /// <summary>
    [SerializeField]
    Text[] requstTitle;
    /// </summary>

    ///
    [SerializeField]
    Text popRequestTitle;
    [SerializeField]
    Text popRequestDescript;
    [SerializeField]
    Text popRequestGoal;
    [SerializeField]
    Text popRequestProgress;
    [SerializeField]
    Text popRequestReward;
    [SerializeField]
    GameObject popRequest;
    [SerializeField]
    GameObject completion;
    [SerializeField]
    Text completionText;

    ///

    List<RequstInfo> requestInfos;
    List<ItemInfo> allItem;
    int curRequstIndex = 0;
    const int MAXREQUST = 3;
    Data curData;
    public void Initialization(List<RequstInfo> requstInfos, List<ItemInfo> allItem)
    {
        this.requestInfos = requstInfos;
        this.allItem = allItem;
        setContent();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setContent()
    {
        for (int i = 0; i < MAXREQUST; i++)
        {
            requstTitle[i].text = requestInfos[i].NAME;
        }
    }

    public void OnGetData(Data data)
    {
        curData = data;
    }

    public void PopRequest(Text name)
    {
        if (name.text.Length == 0)
        {
            popRequest.SetActive(false);
        }
        else
        {
            string title = name.text;
            int requestIndex = 999;
            string itemName = "";
            int GoalNumber = 0;
            int goalRequestNumber = 0;
            for (int i = 0; i < requestInfos.Count; i++)
            {
                if (title == requestInfos[i].NAME)
                {
                    requestIndex = i;
                    break;
                }

            }

            foreach (var item in allItem)
            {
                if (item.ID == requestInfos[requestIndex].ONNDATA)
                {
                    itemName = item.NAME;
                    break;
                }
            }

            if (requestInfos[requestIndex].TYPE == 0)
            {
                GoalNumber = findOneData(requestInfos[requestIndex].ONNDATA);
                goalRequestNumber = requestInfos[requestIndex].TWODATA;
            }
            else if (requestInfos[requestIndex].TYPE == 1)
            {
                GoalNumber = findOneDataTypeTwo(requestInfos[requestIndex].ONNDATA);
                goalRequestNumber = requestInfos[requestIndex].TWODATA;
            }
            else if (requestInfos[requestIndex].TYPE == 2)
            {
                GoalNumber = findOneData(requestInfos[requestIndex].ONNDATA);
                goalRequestNumber = requestInfos[requestIndex].TWODATA;

                if (requestInfos[requestIndex].ONNDATA == 10)   //골드 일 경우
                    GoalNumber = curData.GOLD;

                if (requestInfos[requestIndex].TWODATA >= 10000)
                    goalRequestNumber = requestInfos[requestIndex].ONNDATA;
            }

            popRequestTitle.text = requestInfos[requestIndex].NAME;
            popRequestDescript.text = requestInfos[requestIndex].DESCRIPT;
            popRequestGoal.text = itemName + " " + goalRequestNumber + "개";
            popRequestProgress.text = GoalNumber + "/" + goalRequestNumber;
            popRequestReward.text = "보상 - " + "골드 " + requestInfos[requestIndex].GOLD + " / " +
                "평판 " + requestInfos[requestIndex].REP + " / " +
                "경험치 " + requestInfos[requestIndex].EXP;
            completionText.text = popRequestReward.text;
        }
    }

    int findOneDataTypeTwo(int id)
    {
        int goalNumber = 0;
        foreach (var item in curData.REQUESTCOUNTS)
        {
            if (id == item.id)
            {
                goalNumber = item.sellCount;
                break;
            }
        }

        return goalNumber;
    }

    int findOneData(int id)
    {
        int amo = 0;
        bool isFind = false;

        foreach (var item in curData.CURMATERIALITELIST)
        {
            if(id == item.ITEMINFO.ID)
            {
                amo = item.ITEMINFO.AMOUNTNUMBER;
                isFind = true;
                break;
            }
        }

        foreach (var item in curData.CURPRODUCTIONITEMLIST)
        {
            if (id == item.ITEMINFO.ID)
            {
                amo = item.ITEMINFO.AMOUNTNUMBER;
                isFind = true;
                break;
            }
        }

        if (isFind)
            return amo;
        else
            return 0;
    }

    public void OnClickCompletionBtn(Text name)
    {
        string title = name.text;
        int requestIndex = 999;
        int GoalNumber = 0;
        bool isSuccess = false;

        for (int i = 0; i < requestInfos.Count; i++)
        {
            if (title == requestInfos[i].NAME)
            {
                requestIndex = i;
                break;
            }
        }

        if (requestInfos[requestIndex].TYPE == 0)
        {
            GoalNumber = findOneData(requestInfos[requestIndex].ONNDATA);
            if (GoalNumber >= requestInfos[requestIndex].TWODATA)
                isSuccess = true;
        }
        else if(requestInfos[requestIndex].TYPE == 1)
        {
            GoalNumber = findOneDataTypeTwo(requestInfos[requestIndex].ONNDATA);
            if (GoalNumber >= requestInfos[requestIndex].TWODATA)
            {
                int index = 0;
                for (int i = 0; i < curData.REQUESTCOUNTS.Count; i++)
                {
                    if (requestInfos[requestIndex].ONNDATA == curData.REQUESTCOUNTS[i].id)
                    {
                        index = i;
                        break;
                    }
                }
                Debug.Log(index + "requestIndex");
                curData.REQUESTCOUNTS.RemoveAt(index);
                isSuccess = true;
            }
        }
        else if(requestInfos[requestIndex].TYPE == 2)
        {
            if (requestInfos[requestIndex].ONNDATA == 10)   //골드 일 경우
            {
                if (curData.GOLD >= requestInfos[requestIndex].ONNDATA)
                {
                    curData.GOLD -= requestInfos[requestIndex].ONNDATA;
                    isSuccess = true;
                }
            }
            else
            {
                GoalNumber = findOneData(requestInfos[requestIndex].ONNDATA);
                if (GoalNumber >= requestInfos[requestIndex].TWODATA)
                {
                    int index = 9999;
                    bool isZero = false;
                    for (int i = 0; i < curData.CURMATERIALITELIST.Count; i++)
                    {
                        if(requestInfos[requestIndex].ONNDATA == curData.CURMATERIALITELIST[i].ITEMINFO.ID)
                        {
                            curData.CURMATERIALITELIST[i].ITEMINFO.AMOUNTNUMBER -= requestInfos[requestIndex].TWODATA;
                            curData.BAGSPACE -= requestInfos[requestIndex].TWODATA;
                            isSuccess = true;
                            if (curData.CURMATERIALITELIST[i].ITEMINFO.AMOUNTNUMBER <= 0)
                                index = i;
                            break;
                        }
                    }

                    if (index != 9999)
                        curData.CURMATERIALITELIST.RemoveAt(index);
                }
            }
                
        }

        if (isSuccess)
        {
            popRequest.SetActive(false);
            completion.SetActive(true);
            SoundManager.instance.PlayEff(EffSound.SFX_UI_C_suc);
            RequestSuccess?.Invoke(requestInfos[requestIndex].GOLD, requestInfos[requestIndex].REP, requestInfos[requestIndex].EXP);
            SetRequest(requestIndex);
        }
        else
        {
            Debug.Log("실패");
        }
    }

    void SetRequest(int requestIndex)
    {
        int nextQuest = 0;
        List<int> nextQuests = new List<int>();
        bool isOne = false;
        if(int.TryParse(requestInfos[requestIndex].NEXTQUEST, out nextQuest))
        {
            nextQuest = int.Parse(requestInfos[requestIndex].NEXTQUEST);
            isOne = true;
        }
        else
        {
            for (int i = 0; i < requestInfos[requestIndex].NEXTQUEST.Length; i++)
            {
                if (requestInfos[requestIndex].NEXTQUEST[i] != ',')
                    nextQuests.Add(int.Parse(requestInfos[requestIndex].NEXTQUEST[i].ToString()));
            }
        }

        if (isOne)
        {
            for (int i = 0; i < MAXREQUST; i++)
            {
                if (requstTitle[i].text == requestInfos[requestIndex].NAME)
                {
                    if (nextQuest >= 10000)
                        requstTitle[i].text = "";
                    else
                    {
                        requstTitle[i].text = requestInfos[nextQuest].NAME;

                        if (requestInfos[nextQuest].TYPE == 1)
                        {
                            curData.REQUESTCOUNTS.Add(new RequestCount(requestInfos[nextQuest].ONNDATA));
                            //Debug.Log("추가");
                            //Debug.Log(curData.REQUESTCOUNTS[0].id);
                        }
                    }
                }
            }
        }
        else
        {
            int index = 0;
            for (int i = 0; i < MAXREQUST; i++)
            {
                if(requstTitle[i].text == requestInfos[requestIndex].NAME)
                {
                    requstTitle[i].text = requestInfos[nextQuests[index]].NAME;
                    if(requestInfos[nextQuests[index]].TYPE == 1)
                        curData.REQUESTCOUNTS.Add(new RequestCount(requestInfos[nextQuests[index]].ONNDATA));

                    index++;
                }
                else if(requstTitle[i].text.Length == 0)
                {
                    requstTitle[i].text = requestInfos[nextQuests[index]].NAME;
                    if (requestInfos[nextQuests[index]].TYPE == 1)
                        curData.REQUESTCOUNTS.Add(new RequestCount(requestInfos[nextQuests[index]].ONNDATA));

                    index++;
                }
            }
        }
    }

}
