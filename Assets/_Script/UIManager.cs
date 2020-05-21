using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text goldText;
    [SerializeField]
    Text reputationText;
    [SerializeField]
    Text bagSpaceText;
    [SerializeField]
    GameObject orderBtn;
    [SerializeField]
    GameObject displayBtn;
    [SerializeField]
    GameObject openStoreBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnChangeValueUI(Data data)
    {
        levelText.text = "레벨 : " + data.Level.ToString();
        goldText.text = data.GOLD.ToString();
        reputationText.text = data.REPUTATION.ToString();
        bagSpaceText.text = "공간 : " + data.BAGSPACE + "/" + " " + data.MAX_BAGSPCE;
    }

    public void OnEndDay(bool isEndDay)
    {
        if (isEndDay)
        {
            orderBtn.GetComponent<Button>().enabled = true;
            displayBtn.GetComponent<Button>().enabled = false;
            openStoreBtn.SetActive(true);
        }
        else
        {
            orderBtn.GetComponent<Button>().enabled = false;
            displayBtn.GetComponent<Button>().enabled = true;
            openStoreBtn.SetActive(false);
        }
    }

    //public void OnStartDay()
    //{
    //    orderBtn.GetComponent<Button>().enabled = false;
    //    displayBtn.GetComponent<Button>().enabled = false;
    //}
}
