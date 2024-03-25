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
    [SerializeField] BodyCabinet bodyCabinet;
    [SerializeField] FadeScript fadeScript;

    public bool bodyEquipped;
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
            float pickUpdistance = 2.5f;
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

                //Taking the body out in Day 1
                if(raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayOne[0] != 1 &&
                    SceneManager.GetActiveScene().name == "Morgue 1")
                {
                    GameManager.objectiveArrayDayOne[0] = 1;
                    bodyCabinet.BodyOut();
                }
                //Taking the body out in Day 1
                if (raycastHit.transform.TryGetComponent(out bodyCabinet) && bodyEquipped != true && GameManager.objectiveArrayDayOneHalf[0] != 1 &&
                    SceneManager.GetActiveScene().name == "Morgue Half")
                {
                    GameManager.objectiveArrayDayOneHalf[0] = 1;
                    bodyCabinet.BodyOut();
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

        if (bodyEquipped == true)
        {
            DeadBody.SetActive(true);
        }
        else
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
}

