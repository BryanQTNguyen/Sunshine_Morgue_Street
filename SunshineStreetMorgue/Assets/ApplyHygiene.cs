using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyHygiene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] Animator anim;
    [SerializeField] Animator tableAnim;
    [SerializeField] GameObject Colliders;

    // Start is called before the first frame update
    void Start()
    {
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
    public void ApplyHygieneKit()
    {
        anim.SetBool("Apply", true);
        Colliders.SetActive(true);
        inventoryTwo.kitEquipped = false; 
        StartCoroutine(ApplyingKit());

    }
    public IEnumerator ApplyingKit()
    {
        yield return new WaitForSeconds(19f);
        Colliders.SetActive(false);
        GameManager.changeObjText("Pick up and Burn the body and call it a day");
        updateList();
    }
    public void updateList()
    {
        if (SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[5] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[5] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[5] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[5] = 1;
    }
}
