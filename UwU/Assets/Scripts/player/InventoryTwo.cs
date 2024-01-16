using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTwo : MonoBehaviour
{

    [SerializeField] Transform playerCameraTransform;
    [SerializeField] LayerMask pickUpLayerMask;
    [SerializeField] Transform objectGrabPointTransformInv;

    private objectPickUp ObjectPickUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpdistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpdistance, pickUpLayerMask))
            {
                if(raycastHit.transform.TryGetComponent(out ObjectPickUp))
                {
                    Debug.Log("jhfsd"); 
                    ObjectPickUp.Grab(objectGrabPointTransformInv);
                }

            }

        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        { 
            if(ObjectPickUp != null)
            {
                ObjectPickUp.Drop();
                ObjectPickUp = null;
            }
        }
    }
}
