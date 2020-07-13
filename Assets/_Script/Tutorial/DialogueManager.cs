using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Text dialogueText, DaabiStandingText;
    [SerializeField]
    GameObject TutorialManager, Request, Daabi, DaabiStanding, DaabiContinueButton, DaabiStandingContinueButton, FingerMove, DaabiFinger, DaabiStandingFinger, PopRequestMenu, OrderMenu, OrderBox
        , ProductionMenu, RaycastBlocking, WorkstationMenu, GUIDE;
    [SerializeField]
    Button CreateBtn, CompletionBtn;
    private Queue<string> sentences;

    public static int RequestNum;

    int addSize = 0;

     void Awake()
    {
        sentences = new Queue<string>();
        CreateBtn.onClick.AddListener(CreateBtnClick);
        CompletionBtn.onClick.AddListener(CompletionBtnClick);
    }
    void CreateBtnClick()
    {
        DaabiStandingFinger.SetActive(false);
        DisplayNextSentence();
    }
    void CompletionBtnClick()
    {
        Debug.Log(RequestNum);
        if(RequestNum==1)
        {
            FingerMove.SetActive(true);
        }
    }

    private void Update()
    {
        if (ProductionMenu.activeSelf == false && sentences.Count == 0+ addSize && Request.activeSelf ==false && OrderMenu.activeSelf==false && WorkstationMenu.activeSelf == false)
        {
            GUIDE.SetActive(true);
            DaabiStanding.SetActive(false);
            DaabiFinger.SetActive(false);
            TutorialManager.GetComponent<TutorialManager>().AllOn();
        }

        else if(WorkstationMenu.activeSelf == true)
        {
            GUIDE.SetActive(false);
        }
        else if (Request.activeSelf == true)
        {
            RaycastBlocking.SetActive(false);
            GUIDE.SetActive(false);

            if (sentences.Count == 11+ addSize)
                DisplayNextSentence();

            else if (sentences.Count == 4+ addSize)
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(false);
                FingerMove.SetActive(false);
            }
            else if(sentences.Count == 0)
            {
                DaabiStanding.SetActive(false);
            }
            else
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(true);
            }
            if (PopRequestMenu.activeSelf == true)
            {
                if (sentences.Count == 8+ addSize)
                {
                    DaabiStandingFinger.SetActive(false);
                    FingerMove.SetActive(true);
                    FingerMove.transform.localPosition = new Vector2(200, 235);
                }
                else
                {
                    DaabiStandingContinueButton.SetActive(true);
                    FingerMove.SetActive(false);
                    DaabiStandingFinger.SetActive(true);
                }



            }
            else if (sentences.Count == 8+ addSize)
            {
                FingerMove.transform.localPosition = new Vector2(592, 221);
            }


        }
        else if (sentences.Count == 8+ addSize)
        {
            DisplayNextSentence();
            DaabiStanding.SetActive(false);
            Daabi.SetActive(true);
            TutorialManager.GetComponent<TutorialManager>().OrderOn();
            FingerMove.transform.localPosition = new Vector2(-608, -507);
        }

        else if (OrderMenu.activeSelf == true)
        {
            RaycastBlocking.SetActive(false);
            GUIDE.SetActive(false);
            if (sentences.Count == 7+ addSize)
                DisplayNextSentence();
            else if (sentences.Count == 6)
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(true);
            }
            else
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(false);
            }

        }
        else if (sentences.Count == 6+ addSize)
        {
            DisplayNextSentence();
            Daabi.SetActive(true);
            DaabiStanding.SetActive(false);
            FingerMove.transform.localPosition = new Vector2(106, -316);
        }
        else if (sentences.Count == 5+ addSize && OrderBox.activeSelf == false)
        {
            DisplayNextSentence();
            FingerMove.transform.localPosition = new Vector2(-829, 152);
        }
        else if (ProductionMenu.activeSelf == true)
        {
            GUIDE.SetActive(false);
            if (sentences.Count == 4+ addSize)
            {
                DisplayNextSentence();

                DaabiStanding.SetActive(true);
            }
            else if (sentences.Count == 2+ addSize)
            {
                DaabiStandingFinger.SetActive(false);
            }

        }
        else
        {
            GUIDE.SetActive(true);
        }
    }

    public void StartDialogue (Dialogue dialogue)
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
        else if(sentences.Count ==13+ addSize)
            TutorialManager.GetComponent<TutorialManager>().AllOff();

        else if(sentences.Count == 12+ addSize)
        {
            TutorialManager.GetComponent<TutorialManager>().RequestOn();
            DaabiContinueButton.SetActive(false);
            DaabiFinger.SetActive(false);
            FingerMove.SetActive(true);
            Debug.Log("의뢰 버튼 활성화");
        }
        else if(sentences.Count == 11+ addSize)
        {
            FingerMove.transform.localPosition = new Vector2(-401, 36);
            DaabiStandingContinueButton.SetActive(false);
        }
        else if(sentences.Count == 9)
        {
            DaabiStandingContinueButton.SetActive(false);
        }
        else if(sentences.Count == 3)
        {
            DaabiStandingContinueButton.SetActive(false);
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        DaabiStandingText.text = sentence;
    }
    void EndDialogue()
    {

    }
}
