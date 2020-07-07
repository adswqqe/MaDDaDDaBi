using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    GameObject furnitureDisplaybtn;
    [SerializeField]
    GameObject[] CanvasUIs;
    [SerializeField]
    GameObject furnitureDisplayMenuGo;
    [SerializeField]
    GameObject FurnitureSelection;
    [SerializeField]
    GameObject todaySales;

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
            orderBtn.GetComponent<Button>().interactable = true;
            displayBtn.GetComponent<Button>().interactable = false;
            furnitureDisplaybtn.GetComponent<Button>().interactable = false;
            openStoreBtn.SetActive(true);
            todaySales.SetActive(false);
        }
        else
        {
            orderBtn.GetComponent<Button>().interactable = false;
            displayBtn.GetComponent<Button>().interactable = true;
            furnitureDisplaybtn.GetComponent<Button>().interactable = true;
            openStoreBtn.SetActive(false);
            todaySales.SetActive(true);
        }
    }

    public void OnStartBuild(int temp)
    {
        for (int i = 0; i < CanvasUIs.Length; i++)
        {
            CanvasUIs[i].SetActive(false);
        }

        furnitureDisplayMenuGo.SetActive(false);
    }

    public void OnEndBuild(int temp)
    {
        for (int i = 0; i < CanvasUIs.Length; i++)
        {
            CanvasUIs[i].SetActive(true);
        }
    }

    public void OnClickResetBtn()
    {
        SceneManager.LoadScene(0);
    }

    //public void OnStartDay()
    //{
    //    orderBtn.GetComponent<Button>().enabled = false;
    //    displayBtn.GetComponent<Button>().enabled = false;
    //}
}
