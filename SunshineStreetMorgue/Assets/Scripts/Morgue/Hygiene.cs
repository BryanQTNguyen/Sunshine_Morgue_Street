using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hygiene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject KitTable;
    // Start is called before the first frame update
    void Start()
    {
        KitTable.SetActive(true);
        player = GameObject.Find("Player");
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
    public void HygieneKit()
    {
        inventoryTwo.kitEquipped = true;
        GameManager.objectiveArrayDayOne[4] = 1;
        GameManager.changeObjText("Apply The Hygine kit on the body");
        KitTable.SetActive(false);
    }
}
