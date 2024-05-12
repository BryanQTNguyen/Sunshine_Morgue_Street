using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repick : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] Animator tableAnim;

    // Start is called before the first frame update
    void Start()
    {
        inventoryTwo = player.GetComponent<InventoryTwo>();
        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryTwo == null)
        {
            player = GameObject.Find("Player");
            inventoryTwo = player.GetComponent<InventoryTwo>();
        }
    }

    public void PickUpBodyAfterHygiene()
    {
        inventoryTwo.bodyEquipped = true;
        tableAnim.SetBool("BodyYes", false);
        GameManager.objectiveArrayDayOne[6] = 1;
    }
}
