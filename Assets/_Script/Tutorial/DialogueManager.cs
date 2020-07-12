using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Text dialogueText, DaabiStandingText;
    [SerializeField]
    GameObject TutorialManager, Request, Daabi, DaabiStanding, DaabiContinueButton, FingerMove, DaabiFinger, DaabiStandingFinger, PopRequestMenu, OrderMenu, OrderBox
        , ProductionMenu ;
    [SerializeField]
    Button CreateBtn;
    private Queue<string> sentences;

     void Awake()
    {
        sentences = new Queue<string>();
        CreateBtn.onClick.AddListener(Btn);
    }
    void Btn()
    {
        DaabiStandingFinger.SetActive(false);
        DisplayNextSentence();
    }

    private void Update()
    {
        if (ProductionMenu.activeSelf == false && sentences.Count == 0)
        {
            DaabiStanding.SetActive(false);
            DaabiFinger.SetActive(false);
            TutorialManager.GetComponent<TutorialManager>().AllOn();
        }

        else if (Request.activeSelf == true)
        {
            if (sentences.Count == 11)
                DisplayNextSentence();

            else if (sentences.Count == 4)
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(false);
                FingerMove.SetActive(false);
            }
            else
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(true);
            }
            if (PopRequestMenu.activeSelf == true)
            {
                if (sentences.Count == 8)
                {
                    DaabiStandingFinger.SetActive(false);
                    FingerMove.SetActive(true);
                    FingerMove.transform.localPosition = new Vector2(311, 285);
                }
                else if (sentences.Count == 8)
                {
                    FingerMove.SetActive(false);
                    DaabiStandingFinger.SetActive(true);
                }
                else
                {
                    FingerMove.SetActive(false);
                    DaabiStandingFinger.SetActive(true);
                }



            }
            else if (sentences.Count == 8)
            {
                FingerMove.transform.localPosition = new Vector2(592, 281);
            }


        }
        else if (sentences.Count == 8)
        {
            DisplayNextSentence();
            DaabiStanding.SetActive(false);
            Daabi.SetActive(true);
            TutorialManager.GetComponent<TutorialManager>().OrderOn();
            FingerMove.transform.localPosition = new Vector2(-608, -507);
        }

        else if (OrderMenu.activeSelf == true)
        {
            if (sentences.Count == 7)
                DisplayNextSentence();
            else if (sentences.Count == 4)
            {

            }
            else
            {
                Daabi.SetActive(false);
                DaabiStanding.SetActive(true);
            }

        }
        else if (sentences.Count == 6)
        {
            DisplayNextSentence();
            Daabi.SetActive(true);
            DaabiStanding.SetActive(false);
            FingerMove.transform.localPosition = new Vector2(106, -316);
        }
        else if (sentences.Count == 5 && OrderBox.activeSelf == false)
        {
            DisplayNextSentence();
            FingerMove.transform.localPosition = new Vector2(-829, 152);
        }
        else if (ProductionMenu.activeSelf == true)
        {
            if (sentences.Count == 4)
            {
                DisplayNextSentence();

                DaabiStanding.SetActive(true);
            }
            else if (sentences.Count == 2)
            {
                DaabiStandingFinger.SetActive(false);
            }

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
        else if(sentences.Count == 12)
        {
            TutorialManager.GetComponent<TutorialManager>().RequestOn();
            DaabiContinueButton.SetActive(false);
            DaabiFinger.SetActive(false);
            FingerMove.SetActive(true);
            Debug.Log("의뢰 버튼 활성화");
        }
        else if(sentences.Count == 11)
        {
            FingerMove.transform.localPosition = new Vector2(-401, 36);
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        DaabiStandingText.text = sentence;
    }
    void EndDialogue()
    {

    }
}
