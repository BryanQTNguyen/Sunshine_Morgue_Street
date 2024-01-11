using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Rigidbody rb;
    public float dropForwardForce, dropUpwardForce;
    public static bool slotFull;
    public bool equipped;
    public bool canPickUp;
    public BoxCollider coll;
    public Transform itemContainer;

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(itemContainer);



    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickUp") && !equipped && !slotFull)
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            canPickUp = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }


}
