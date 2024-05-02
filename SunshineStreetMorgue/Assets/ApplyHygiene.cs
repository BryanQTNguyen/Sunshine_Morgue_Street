using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHygiene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
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
        StartCoroutine(washBodyy());

    }
    public IEnumerator washBodyy()
    {
        yield return new WaitForSeconds(8f);
        GameManager.objectiveArrayDayOne[5] = 1;
        GameManager.changeObjText("Get The Hygine kit");
    }
}
