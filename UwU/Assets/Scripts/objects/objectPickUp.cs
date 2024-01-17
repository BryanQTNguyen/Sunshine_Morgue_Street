using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPickUp : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private string InventoryName;
    [SerializeField] InventoryTwo inventoryTwo;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransformInv)
    {
        this.objectGrabPointTransform = objectGrabPointTransformInv;
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        InventoryName = null;
        inventoryTwo.CurrentlyEquipped = null;
    }

    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(rb.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            InventoryName = gameObject.name;
            inventoryTwo.CurrentlyEquipped = InventoryName;
            rb.MovePosition(newPosition);
        }
    }
}
