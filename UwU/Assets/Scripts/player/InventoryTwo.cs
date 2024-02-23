using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
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
            float pickUpdistance = 5f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpdistance, pickUpLayerMask))
            {
                if(raycastHit.transform.TryGetComponent(out ObjectPickUp))
                {
                    ObjectPickUp.Grab(objectGrabPointTransformInv);
                }

                if (raycastHit.transform.TryGetComponent(out PlayVHS) && currentlyEquippedBool == true)
                {
                    if(CurrentlyEquipped == "Tape1")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 20f);
                    else if (CurrentlyEquipped == "Tape2")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 1449f);
                    else if (CurrentlyEquipped == "Tape3")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 40f);
                }
                if (raycastHit.transform.TryGetComponent(out DoorOpen))
                {
                    sceneController.searchScenes(DoorOpen.SceneTo);

                }
                if(raycastHit.transform.TryGetComponent(out dialogueTrigger) && fadeScript.doneFadingOut == true)
                {
                    dialogueTrigger.StartDialogue();
                }
            }
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

