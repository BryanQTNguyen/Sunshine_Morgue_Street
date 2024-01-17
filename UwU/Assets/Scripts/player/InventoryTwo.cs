using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTwo : MonoBehaviour
{

    [SerializeField] Transform playerCameraTransform;
    [SerializeField] LayerMask pickUpLayerMask;
    [SerializeField] Transform objectGrabPointTransformInv;
    public string CurrentlyEquipped;

    private objectPickUp ObjectPickUp;
    private playVHS PlayVHS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpdistance = 5f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpdistance, pickUpLayerMask))
            {
                if(raycastHit.transform.TryGetComponent(out ObjectPickUp))
                {
                    ObjectPickUp.Grab(objectGrabPointTransformInv);
                }

                if (raycastHit.transform.TryGetComponent(out PlayVHS) && CurrentlyEquipped != null)
                {
                    if(CurrentlyEquipped == "Tape1")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 20f);
                    else if (CurrentlyEquipped == "Tape2")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 30f);
                    else if (CurrentlyEquipped == "Tape3")
                        PlayVHS.PlayVHSTape(CurrentlyEquipped, 40f);
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        { 
            if(CurrentlyEquipped != null)
            {
                ObjectPickUp.Drop();
                ObjectPickUp = null;
            }
        }
    }
}

