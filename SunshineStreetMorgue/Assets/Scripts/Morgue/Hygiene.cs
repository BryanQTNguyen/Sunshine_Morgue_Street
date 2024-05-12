using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        updateList();
        GameManager.changeObjText("Gotta Apply The Hygine kit");
        KitTable.SetActive(false);
    }
    public void updateList()
    {
        if (SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[4] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[4] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[4] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[4] = 1;
    }
}
