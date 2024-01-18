using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class objectPickUp : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform = null;
    private string InventoryName;
    private Collider collider;
    [SerializeField] InventoryTwo inventoryTwo;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
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
        inventoryTwo.currentlyEquippedBool = false;
        inventoryTwo.CurrentGameObject = null;
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && rb.useGravity == false)
        {
            Physics.IgnoreCollision(col.collider, collider);
        }
    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(rb.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            InventoryName = gameObject.name;
            inventoryTwo.CurrentlyEquipped = InventoryName;
            inventoryTwo.currentlyEquippedBool = true;
            inventoryTwo.CurrentGameObject = gameObject;
            rb.MovePosition(newPosition);
        }
    }
}
