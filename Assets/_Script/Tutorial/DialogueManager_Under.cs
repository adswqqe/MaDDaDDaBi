using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_Under : MonoBehaviour
{
    private Queue<string> sentences;
    [SerializeField]
    Text dialogueText, DaabiStandingText;
    [SerializeField]
    Button Building, BagITEMBtn, ConfirmationBtn, DisplayBtn, PotionBtn;
    [SerializeField]
    GameObject FingerMove, BagITEM, Potion;
    int num;

    public void Start()
    {
        Building.onClick.AddListener(BuildingClick);
        ConfirmationBtn.onClick.AddListener(ConfirmationBtnClick);
        DisplayBtn.onClick.AddListener(DisplayBtnClick);
    }

    void BuildingClick()
    {
        BagITEMBtn = BagITEM.transform.GetChild(0).transform.GetComponent<Button>();
        BagITEMBtn.onClick.AddListener(BagITEMClick);
        PotionBtn = Potion.transform.GetChild(0).transform.GetComponent<Button>();
        PotionBtn.onClick.AddListener(PotionBtnClick);
        num++;
    }
    void BagITEMClick()
    {
        num++;
        Debug.Log(num);
    }

    void ConfirmationBtnClick()
    {
        num++;
        Debug.Log(num);
    }

    void DisplayBtnClick()
    {
        num++;
        Debug.Log(num);
    }
    void PotionBtnClick()
    {
        num++;
        Debug.Log(num);
    }

    private void Update()
    {


        if (num == 1)
        {
            FingerMove.transform.localPosition = new Vector2(-747, -54);
        }
        else if(num == 2)
        {
            FingerMove.transform.localPosition = new Vector2(11, -102); //가방에서 가구 눌렀을 때
        }
        else if(num == 3)
        {
            FingerMove.transform.localPosition = new Vector2(59, -507); //가구에서 확정 눌렀을 때
        }
    
        else if(num == 4)
        {
            FingerMove.transform.localPosition = new Vector2(-828, -36); //진열 눌렀을 때 
        }
        else if (num == 5)
        {
            FingerMove.SetActive(false);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        DaabiStandingText.text = sentence;
    }
    void EndDialogue()
    {

    }
}
