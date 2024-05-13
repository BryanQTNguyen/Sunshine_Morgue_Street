using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sawLegs : MonoBehaviour
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
    public void sawOffLegRight()
    {
        tableAnim.SetBool("BodyYes3", false);
        anim.SetBool("SawRight", true);
        Colliders.SetActive(true);
        inventoryTwo.kitEquipped = false;
        StartCoroutine(SawingRight());
    }
    public IEnumerator SawingRight()
    {
        yield return new WaitForSeconds(5f);
        Colliders.SetActive(false);
        anim.SetBool("SawRight", false);
        GameManager.changeObjText("Saw off the left leg! Hurry!");
        updateList();
    }

    public void sawOffLegLeft()
    {
        anim.SetBool("SawLeft", true);
        Colliders.SetActive(true);
        StartCoroutine(SawingLeft());
    }
    public IEnumerator SawingLeft()
    {
        yield return new WaitForSeconds(5.5f);
        Colliders.SetActive(false);
        anim.SetBool("SawRight", false);
        anim.SetBool("SawLeft", false);
        GameManager.changeObjText("Time to burn and call it a day");
        updateListTwo();
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
    public void updateListTwo()
    {
        if (SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[6] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[6] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[6] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[6] = 1;
    }
}
