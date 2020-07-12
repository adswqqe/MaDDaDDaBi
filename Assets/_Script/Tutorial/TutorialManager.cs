using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    Button[] CanvasUIs;

    private void Start()
    {
        for (int i = 0; i < CanvasUIs.Length; i++)
        {
            CanvasUIs[i].interactable = false;
        }
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
