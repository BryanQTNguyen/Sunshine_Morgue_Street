using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPickUp : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    public string InventoryName;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransformInv)
    {
        this.objectGrabPointTransform = objectGrabPointTransformInv;
        rb.useGravity = false;
    }
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        InventoryName = null;
    }

    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(rb.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            InventoryName = gameObject.name;
            rb.MovePosition(newPosition);
        }
    }
}
