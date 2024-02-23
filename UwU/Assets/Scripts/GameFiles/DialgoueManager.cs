using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.VersionControl;

public class DialgoueManager : MonoBehaviour
{
    public static DialgoueManager Instance;
    private void Awake()
    {


    }
    //All the variables for the displaying dialgoue 
    public TMP_Text actorName;
    public string AudioToPlay;
    public TMP_Text messageText;
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
                fadeScript.HideDialogueFade();
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
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true && fadeScript.doneFadingIn == true)
        {
            NextMessage();
        }
    }
}
