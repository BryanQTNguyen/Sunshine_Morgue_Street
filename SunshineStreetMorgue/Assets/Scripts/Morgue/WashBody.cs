using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WashBody : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject WaterFlow;
    [SerializeField] GameObject Colliders;
    // Start is called before the first frame update
    void Start()
    {
        WaterFlow.SetActive(false);
        Colliders.SetActive(false);
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
    public void washBody()
    {
        WaterFlow.SetActive(true);
        Colliders.SetActive(true);
        StartCoroutine(washBodyy());

    }
    public IEnumerator washBodyy()
    {
        yield return new WaitForSeconds(8f);
        WaterFlow.SetActive(false);
        Colliders.SetActive(false);
        updateList();
        GameManager.changeObjText("Get The Hygine kit");
    }
    public void updateList()
    {
        if (SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[3] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[3] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[3] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[3] = 1;
    }
}
