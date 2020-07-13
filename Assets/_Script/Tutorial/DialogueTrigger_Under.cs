using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Under : MonoBehaviour
{
    public Dialogue dialogue;

    public void Start()
    {
        FindObjectOfType<DialogueManager_Under>().StartDialogue(dialogue);
    }
}
