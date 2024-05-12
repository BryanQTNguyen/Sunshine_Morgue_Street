using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
    [SerializeField] ApplyHygiene applyHygiene;
    [SerializeField] Repick repick;
    [SerializeField] BurnBody burnBody;
    [SerializeField] bed Bed;

    public bool bodyEquipped; // This variable will control when the body equip will appear. This boolean will be used in BodyCabinet script
    public bool kitEquipped;
    [SerializeField] GameObject DeadBody;
    [SerializeField] GameObject Kit;





    string previousText;


    // Start is called before the first frame update
    void Start()
    {
        managerObj = GameObject.Find("gameManager");
        GameManager = managerObj.GetComponent<gameManager>();
        bodyEquipped = false;
        kitEquipped = false;

        SceneManagerObj = GameObject.Find("SceneController");
        sceneController = SceneManagerObj.GetComponent<SceneController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager == null)
        {
            managerObj = GameObject.Find("gameManager");
            GameManager = managerObj.GetComponent<gameManager>();
        }
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
                        if((DoorOpen.SceneTo == "Morgue 1" || DoorOpen.SceneTo == "Morgue 2"|| DoorOpen.SceneTo == "Morgue 3") && GameManager.DayOver == true)
                        {
                            previousText = GameManager.currentObjText.text;
                            GameManager.changeObjText("Day is done, I'm not going back");
                            StartCoroutine(FinishWorkText());
                        }
                        else if (DoorOpen.SceneTo == "Apartment")
                        {
                            if (GameManager.DayOver == true)
                            {
                                GameManager.defineText();
                                GameManager.PrimaryObjective[2] = 1;
                                GameManager.changeObjText("I better sleep");
                                sceneController.searchScenes(DoorOpen.SceneTo);
                            }
                            else
                            {
                                previousText = GameManager.currentObjText.text;
                                GameManager.changeObjText("Its getting late better get to work");
                                StartCoroutine(FinishWorkText());
                            }
                        }
                        else
                        {
                            sceneController.searchScenes(DoorOpen.SceneTo);
                        }
                    }
                    else if(DoorOpen.SceneTo == "Outside" && (SceneManager.GetActiveScene().name == "Morgue 1" || SceneManager.GetActiveScene().name == "Morgue 2" ||
                        SceneManager.GetActiveScene().name == "Morgue 3"))
                    {
                        if (GameManager.taskFinished == true || GameManager.objectiveArrayDayOne[7] == 1)
                        {
                            GameManager.DayOver = true;
                            sceneController.searchScenes("Outside");
                            GameManager.relocatePlayer();
                            GameManager.defineText();
                            GameManager.PrimaryObjective[1] = 1;
                            GameManager.changeObjText("I need to get home");
                            GameManager.taskFinished = false;
                            GameManager.taskStarted = false;

                        }
                        else
                        {
                            previousText = GameManager.currentObjText.text;
                            GameManager.pulse = true;
                            GameManager.changeObjText("Gotta Finish Work First");
                            StartCoroutine(FinishWorkText());
                        }

                    }
                    else if(DoorOpen.SceneTo == "Outside" && SceneManager.GetActiveScene().name == "Apartment"){
                        sceneController.searchScenes("Outside");
                        if (GameManager.PrimaryObjective[0] != 1)
                        {
                            GameManager.defineText();
                            GameManager.PrimaryObjective[0] = 1;
                            GameManager.DayOver = false;
                            GameManager.changeObjText("Get to work at the Morgue");
                        }

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
                        if(GameManager.objectiveArrayDayOne[0] != 1)
                        {
                            bodyCabinet.BodyOut();
                        }
                    }
                    //Placing the body down
                    if(raycastHit.transform.TryGetComponent(out bodyBed) && ObjectiveChecker(1, true) == true && GameManager.objectiveArrayDayOne[1] != 1)
                    {
                        bodyBed.bodyDown();
                    }
                    //Wash Hands
                    if (raycastHit.transform.TryGetComponent(out washHands) && ObjectiveChecker(2, true) == true && GameManager.objectiveArrayDayOne[2] != 1)
                    {
                        washHands.washHands();
                    }
                    //Wash body
                    if (raycastHit.transform.TryGetComponent(out washBody) && ObjectiveChecker(3, true) == true && GameManager.objectiveArrayDayOne[3] != 1)
                    {
                        washBody.washBody();
                    }
                    //Hygiene kit
                    if (raycastHit.transform.TryGetComponent(out hygiene) && ObjectiveChecker(4, true) == true && GameManager.objectiveArrayDayOne[4] != 1)
                    {
                        hygiene.HygieneKit();
                    }
                    if (raycastHit.transform.TryGetComponent(out applyHygiene) && ObjectiveChecker(5, true) == true && GameManager.objectiveArrayDayOne[5] != 1)
                    {
                        applyHygiene.ApplyHygieneKit();
                    }
                    //Picking up the body
                    if (raycastHit.transform.TryGetComponent(out repick) && ObjectiveChecker(6, true) == true && GameManager.objectiveArrayDayOne[6] != 1)
                    {
                        repick.PickUpBodyAfterHygiene();
                    }
                    if (raycastHit.transform.TryGetComponent(out burnBody) && ObjectiveChecker(7, true) == true && GameManager.objectiveArrayDayOne[7] != 1)
                    {
                        burnBody.BurningBody();
                    }
                }
                if(SceneManager.GetActiveScene().name == "Morgue 2")
                {

                    //Taking the Body Out
                    if (raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayTwo[0] != 1)
                    {
                        if (GameManager.objectiveArrayDayTwo[0] != 1)
                        {
                            bodyCabinet.BodyOut();
                        }
                    }
                    //Placing the body down
                    if (raycastHit.transform.TryGetComponent(out bodyBed) && ObjectiveChecker(1, true) == true && GameManager.objectiveArrayDayTwo[1] != 1)
                    {
                        bodyBed.bodyDown();
                    }
                    //Wash Hands
                    if (raycastHit.transform.TryGetComponent(out washHands) && ObjectiveChecker(2, true) == true && GameManager.objectiveArrayDayTwo[2] != 1)
                    {
                        washHands.washHands();
                    }
                    //Wash body
                    if (raycastHit.transform.TryGetComponent(out washBody) && ObjectiveChecker(3, true) == true && GameManager.objectiveArrayDayTwo[3] != 1)
                    {
                        washBody.washBody();
                    }
                    //Hygiene kit
                    if (raycastHit.transform.TryGetComponent(out hygiene) && ObjectiveChecker(4, true) == true && GameManager.objectiveArrayDayTwo[4] != 1)
                    {
                        hygiene.HygieneKit();
                    }
                    if (raycastHit.transform.TryGetComponent(out applyHygiene) && ObjectiveChecker(5, true) == true && GameManager.objectiveArrayDayTwo[5] != 1)
                    {
                        applyHygiene.ApplyHygieneKit();
                    }
                    //Picking up the body
                    if (raycastHit.transform.TryGetComponent(out repick) && ObjectiveChecker(6, true) == true && GameManager.objectiveArrayDayTwo[6] != 1)
                    {
                        repick.PickUpBodyAfterHygiene();
                    }
                    if (raycastHit.transform.TryGetComponent(out burnBody) && ObjectiveChecker(7, true) == true && GameManager.objectiveArrayDayTwo[7] != 1)
                    {
                        burnBody.BurningBody();
                    }
                }
                if (SceneManager.GetActiveScene().name == "Apartment")
                {
                    if (raycastHit.transform.TryGetComponent(out Bed) && GameManager.DayOver == true)
                    {
                        Bed.Sleep();
                    }
                    else if (raycastHit.transform.TryGetComponent(out Bed) && GameManager.DayOver == false)
                    {
                        previousText = GameManager.currentObjText.text;
                        GameManager.pulse = true;
                        GameManager.changeObjText("Can't sleep anymore, time for work");
                        StartCoroutine(FinishWorkText());
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

        if (bodyEquipped == true && DeadBody != null)
        {
            DeadBody.SetActive(true);
        }
        else if (bodyEquipped == false && DeadBody != null)
        {
            DeadBody.SetActive(false);
            bodyEquipped = false;
        }

        if (kitEquipped == true && Kit != null)
        {
            Kit.SetActive(true);
        }
        else if(kitEquipped == false && Kit != null)
        {
            Kit.SetActive(false);
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
                    print("Broke");
                    Debug.Log("BREAK");
                    Debug.Log(GameManager.objectiveArrayDayOne);
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
    public IEnumerator FinishWorkText()
    {
        yield return new WaitForSeconds(2f);
        GameManager.pulse = false;
        GameManager.changeObjText(previousText);
    }
}

