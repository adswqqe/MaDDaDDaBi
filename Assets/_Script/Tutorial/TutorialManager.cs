using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    Button[] CanvasUIs;
    [SerializeField]
    GameObject RaycastBlocking;
    [SerializeField]
    Button SettingBtn;

    private void Start()
    {
        SettingBtn.onClick.AddListener(SettingBtnClick);
        AllOff();
    }
    void SettingBtnClick()
    {
        RaycastBlocking.SetActive(false);
    }

    public void AllOff()
    {
        for (int i = 0; i < CanvasUIs.Length; i++)
        {
            CanvasUIs[i].interactable = false;
        }
        CanvasUIs[5].interactable = true;
    }
        

    public void RequestOn()
    {
        CanvasUIs[7].interactable = true;
    }

    public void OrderOn()
    {
        CanvasUIs[1].interactable = true;
    }

    public void AllOn()
    {
        for (int i = 0; i < CanvasUIs.Length; i++)
        {
            CanvasUIs[i].interactable = true;
        }
    }
}
