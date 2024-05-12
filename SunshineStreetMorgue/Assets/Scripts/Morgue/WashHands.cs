using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WashHands : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] Animator anim;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject WaterFlow;
    // Start is called before the first frame update
    void Start()
    {
        WaterFlow.SetActive(false);
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
    public void washHands()
    {
        WaterFlow.SetActive(true);
        StartCoroutine(WashHand());

    }
    public IEnumerator WashHand()
    {
        yield return new WaitForSeconds(5f);
        WaterFlow.SetActive(false);
        updateList();
        GameManager.changeObjText("Now I can shower the body");
    }
    public void updateList()
    {
        if (SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[2] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[2] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[2] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[2] = 1;
    }
}
