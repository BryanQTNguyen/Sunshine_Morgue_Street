using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isCutScene; //the main difference between isCutScene and a regular dialogue is that it starts up automatically and utilizes the continue button
    public Message[] messages;


    public int enemyIdentify;
    public string enemyName;

    public bool isInTalkingRange = false;
    public DialgoueManager manager;
    public FadeScript fadeScript;
    public string[] audios;
    public bool agressiveStart; // this is for auto dialogue appear if you get close
    private int index = 0; // this index is used to make sure aggressive dialogue doesn't happen multiple times
    private int indexTwo = 0; //this index is used to make sure the cutscene start dialogue doesn't happen multiple times
    public bool fightingWords; //this is used for dialogue that ends with combat

    private void Start()
    {
    }
    public void StartDialogue()
    {
        if(isInTalkingRange == true && fadeScript.doneFadingOut == true)
        {
            FindObjectOfType<DialgoueManager>().OpenDialogue(messages, audios);
        }
    }

    void Update()
    {
        if (isCutScene == true && indexTwo == 0)
        {
            StartDialogue();
            indexTwo = 1;
        }
        if (isCutScene == false)
        {
            if (isInTalkingRange == true && agressiveStart == true && index == 0 && manager.isActive == false && fadeScript.doneFadingOut == true)
            {
                StartDialogue();
                index = 1;
            }
        }


    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInTalkingRange = true;
            FindObjectOfType<gameManager>().isInTalkingRangeMain = isInTalkingRange;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInTalkingRange = false;
            FindObjectOfType<gameManager>().isInTalkingRangeMain = isInTalkingRange;
            index = 0;
        }
    }
}

[System.Serializable]
public class Message
{
    public string message;
}
