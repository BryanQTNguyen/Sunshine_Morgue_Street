using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialgoueManager : MonoBehaviour
{
    public static DialgoueManager Instance;
    //All the variables for the displaying dialgoue 
    public Text actorName;
    public string AudioToPlay;
    public Text messageText;
    public RectTransform backgroundBox;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private GameObject continueButton;

    [SerializeField] GameObject managerObj;
    [SerializeField] gameManager manager;

    [SerializeField] SceneController controller;

    public bool muteDialogueAudio;

    Message[] currentMessages;
    string[] currentAudio;

    int currentEnemy;
    string currentEnemySpecifics;
    int activeMessage = 0;
    public bool isActive = false;
    public float typingSpeed;


    public FadeScript fadeScript;


    public void OpenDialogue(Message[] messages, string[] audios)
    {
        fadeScript.ShowDialogue();
        currentMessages = messages;
        currentAudio = audios;
        activeMessage = 0;
        isActive = true;
        DisplayMessage();
    }


    void DisplayMessage()
    {
        muteDialogueAudio = false;
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        /*
        AudioToPlay = currentAudio[activeMessage];
        AudManager.Instance.PlayDialogue(AudioToPlay);
        */
    }
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        
        else
        {
            
            if (dialogueTrigger.isCutScene == false)
            {
                isActive = false;
                fadeScript.HideDialogue();
                muteDialogueAudio = true;

            }
            
            else if (dialogueTrigger.isCutScene == true)
            {
                muteDialogueAudio = true;
                AudManager.Instance.PlayDialogue("Silence");
                continueButton.gameObject.SetActive(true);
            }
       }
    }
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        fadeScript.HideDialogue();
        if (dialogueTrigger.isCutScene == true)
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true && fadeScript.doneFadingIn == true && manager.isInTalkingRangeMain == true)
        {
            NextMessage();
        }
        if(manager == null)
        {
            managerObj = GameObject.Find("gameManager");
            manager = managerObj.GetComponent<gameManager>();
        }

        if(manager.isInTalkingRangeMain == false && isActive == true)
        {
            fadeScript.HideDialogueFade();
            muteDialogueAudio = true;
            isActive = false;
            Debug.Log("Kai Cenat rizzler");
        }
    }
}
