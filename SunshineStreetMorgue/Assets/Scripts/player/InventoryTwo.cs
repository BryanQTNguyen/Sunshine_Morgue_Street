using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InventoryTwo : MonoBehaviour
{

    [SerializeField] Transform playerCameraTransform;
    [SerializeField] LayerMask pickUpLayerMask;
    [SerializeField] Transform objectGrabPointTransformInv;
    public string CurrentlyEquipped = null;
    public bool currentlyEquippedBool = false;
    public GameObject CurrentGameObject;
    public objectPickUp ObjectPickUp;
    [SerializeField] playVHS PlayVHS;
    [SerializeField] doorOpen DoorOpen;
    [SerializeField] GameObject managerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] FadeScript fadeScript;







    [SerializeField] BodyCabinet bodyCabinet;
    [SerializeField] BodyBed bodyBed;
    [SerializeField] WashHands washHands;
    [SerializeField] WashBody washBody;
    [SerializeField] Hygiene hygiene;
    [SerializeField] BurnBody burnBody;

    public bool bodyEquipped; // This variable will control when the body equip will appear. This boolean will be used in BodyCabinet script
    [SerializeField] GameObject DeadBody;

    // Start is called before the first frame update
    void Start()
    {
        bodyEquipped = false;
        managerObj = GameObject.Find("gameManager");
        GameManager = managerObj.GetComponent<gameManager>();
        SceneManagerObj = GameObject.Find("SceneController");
        sceneController = SceneManagerObj.GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.VHSVideoPlaying == false)
        {
            float pickUpdistance = 1.4f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpdistance, pickUpLayerMask))
            {
                // Picking up an object
                if (raycastHit.transform.TryGetComponent(out ObjectPickUp))
                {
                    ObjectPickUp.Grab(objectGrabPointTransformInv);
                }

                //Working with VHS tapes
                if (raycastHit.transform.TryGetComponent(out PlayVHS) && currentlyEquippedBool == true)
                {
                    if(CurrentlyEquipped == "Tape1")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 20f);
                    else if (CurrentlyEquipped == "Tape2")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 1449f);
                    else if (CurrentlyEquipped == "Tape3")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 40f);
                }

                //Working with the scene transition doors
                if (raycastHit.transform.TryGetComponent(out DoorOpen))
                {
                    if(DoorOpen.SceneTo != "Outside")
                    {
                        sceneController.searchScenes(DoorOpen.SceneTo);

                    }else if(DoorOpen.SceneTo == "Outside" && SceneManager.GetActiveScene().name == "Morgue")
                    {
                        sceneController.searchScenes("Outside");
                        GameManager.relocatePlayer();
                    }else if(DoorOpen.SceneTo == "Outside" && SceneManager.GetActiveScene().name == "Apartment"){
                        sceneController.searchScenes("Outside");
                    }

                }

                //Working with dialogue messages
                if(raycastHit.transform.TryGetComponent(out dialogueTrigger) && fadeScript.doneFadingOut == true)
                {
                    if(GameManager.isInTalkingRangeMain == true)
                    {
                        dialogueTrigger.StartDialogue();
                    }
                }

                //Day 1 Functionality
                if(SceneManager.GetActiveScene().name == "Morgue 1")
                {

                    //Taking the Body Out
                    if (raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayOne[0] != 1)
                    {
                        GameManager.objectiveArrayDayOne[0] = 1;
                        bodyCabinet.BodyOut();
                    }
                    //Placing the body down
                    if(raycastHit.transform.TryGetComponent(out bodyBed) && ObjectiveChecker(1, true) == true && GameManager.objectiveArrayDayOne[1] != 1)
                    {

                    }
                    //Wash Hands
                    if (raycastHit.transform.TryGetComponent(out washHands) && ObjectiveChecker(2, true) && GameManager.objectiveArrayDayOne[2] != 1)
                    {

                    }
                    //Wash body
                    if (raycastHit.transform.TryGetComponent(out washBody) && ObjectiveChecker(3, true) && GameManager.objectiveArrayDayOne[3] != 1)
                    {

                    }
                    //Hygiene kit
                    if (raycastHit.transform.TryGetComponent(out hygiene) && ObjectiveChecker(4, true) && GameManager.objectiveArrayDayOne[4] != 1)
                    {

                    }
                    //Burning the body
                    if (raycastHit.transform.TryGetComponent(out burnBody) && ObjectiveChecker(5, true) && GameManager.objectiveArrayDayOne[5] != 1)
                    {

                    }
                }


                //Taking the body out in Day 2
                if (raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayTwo[0] != 1 &&
                    SceneManager.GetActiveScene().name == "Morgue 2")
                {
                    GameManager.objectiveArrayDayTwo[0] = 1;
                    bodyCabinet.BodyOut();
                }
                //Taking the body out in Day 3
                if (raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayThree[0] != 1 &&
                    SceneManager.GetActiveScene().name == "Morgue 3")
                {
                    GameManager.objectiveArrayDayThree[0] = 1;
                    bodyCabinet.BodyOut();
                }
            }
        }

        if (bodyEquipped == true && SceneManager.GetActiveScene().name == "Morgue 1")
        {
            DeadBody.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "Morgue 1")
        {
            DeadBody.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && currentlyEquippedBool == true && GameManager.VHSVideoPlaying == false) 
        { 
            if(currentlyEquippedBool == true)
            {
                ObjectPickUp = CurrentGameObject.GetComponent<objectPickUp>();
                ObjectPickUp.Drop();
                ObjectPickUp = null;
            }
        }
    }

    public bool ObjectiveChecker(int length, bool conditionMet)
    {
        if(SceneManager.GetActiveScene().name == "Morgue 1")
        {
            for (int i = 0; i < length; i++)
            {
                if (GameManager.objectiveArrayDayOne[i] != 1)
                {
                    conditionMet = false;
                    break;
                }
            }
            if(conditionMet == true)
            {
                return conditionMet;
            }
            else
            {
                return conditionMet; 
            }
            
        }
        if (SceneManager.GetActiveScene().name == "Morgue Half")
        {
            for (int i = 0; i < length; i++)
            {
                if (GameManager.objectiveArrayDayOneHalf[i] != 1)
                {
                    conditionMet = false;
                    break;
                }
            }
            if (conditionMet == true)
            {
                return conditionMet;
            }
            else
            {
                return conditionMet;
            }

        }
        if (SceneManager.GetActiveScene().name == "Morgue 2")
        {
            for (int i = 0; i < length; i++)
            {
                if (GameManager.objectiveArrayDayTwo[i] != 1)
                {
                    conditionMet = false;
                    break;
                }
            }
            if (conditionMet == true)
            {
                return conditionMet;
            }
            else
            {
                return conditionMet;
            }

        }
        if (SceneManager.GetActiveScene().name == "Morgue 3")
        {
            for (int i = 0; i < length; i++)
            {
                if (GameManager.objectiveArrayDayThree[i] != 1)
                {
                    conditionMet = false;
                    break;
                }
            }
            if (conditionMet == true)
            {
                return conditionMet;
            }
            else
            {
                return conditionMet;
            }

        }
        else
        {
            return false;
        }


    }
}

