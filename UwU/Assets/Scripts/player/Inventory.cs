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
    public Transform itemContainer, player, camera;
    public string inHandItem;

    private void Start()
    {
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        rb.AddForce(camera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(camera.up * dropUpwardForce, ForceMode.Impulse);
        float random = UnityEngine.Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);


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

    // Update is called once per frame
    void Update()
    {
        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        if(!equipped && Input.GetKeyUp(KeyCode.E) && !slotFull)
        {
            PickUp();
        }
    }


}
