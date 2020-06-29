using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WasteMenu : MonoBehaviour
{
    public Action<int> WasteProcessing;

    [SerializeField]
    Slider slider;
    [SerializeField]
    Text wasteNumberText;
    [SerializeField]
    Text wasteCountText;
    [SerializeField]
    Text wasteDescriptText;
    [SerializeField]
    Text wasteProcessingText;

    int wasteCount = 0;
    int count = 0;
    int curGold = 0;

    GameObject Notification; //골드 부족, 재료 부족 등 알림창

    // Start is called before the first frame update
    void Start()
    {
        Notification = GameObject.Find("Canvas").transform.Find("Notification").gameObject;
    }

    void SetUIcontent()
    {
        slider.value = wasteCount;
        wasteNumberText.text = wasteCount + "개 보관중";

        if(wasteCount >= 4 && wasteCount <= 9)
        {
            wasteDescriptText.text = "악취가 조금 심하다. 손님이 줄어들 것 같다.";
        }
        else if(wasteCount >= 10 && wasteCount <= 14)
        {
            wasteDescriptText.text = "악취가 심하다. 손님이 적게 올 것 같다. ";
        }
        else if(wasteCount >= 15 && wasteCount <= 19)
        {
            wasteDescriptText.text = "악취가 매우 심하다. 손님이 많이 줄어들 것 같다.";
        }
        else if(wasteCount >= 20)
        {
            wasteDescriptText.text = "악취가 심각하다. 손님이 거의 안 올 것 같다.";
        }
        else
            wasteDescriptText.text = "깨끗한 상태이다.";

    }

    public void OnGetData(Data data)
    {
        wasteCount = data.CURWASTEITEMLIST.Count;
        curGold = data.GOLD;
        SetUIcontent();
    }

    public void AddCount(int count)
    {
        this.count += count;
        if (this.count <= 0)
            this.count = 0;
        else if (this.count >= wasteCount)
            this.count = wasteCount;

        wasteCountText.text = this.count.ToString();
        wasteProcessingText.text = ("처리비용: " + this.count * 2).ToString() + "냥";
    }

    public void OnClickProcessingButton()
    {
        if (curGold < count * 2)
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "골드가 부족합니다.";
            return;
        }

        if (count == 0)
        {
            Notification.SetActive(true);
            Notification.transform.GetChild(1).transform.GetComponent<Text>().text = "처리 갯수를 선택해주세요.";
            return;
        }

        WasteProcessing?.Invoke(count);
        AddCount(-count);
    }
}
