using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBody : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject Smoke;
    [SerializeField] GameObject Colliders;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Colliders.SetActive(false);
        Smoke.SetActive(false);
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
    public void BurningBody()
    {
        Colliders.SetActive(true);
        anim.SetBool("Burn", true);
        inventoryTwo.bodyEquipped = false;
        StartCoroutine(Burning());

    }
    public IEnumerator Burning()
    {
        yield return new WaitForSeconds(2f);
        Smoke.SetActive(true);
        yield return new WaitForSeconds(8f);
        GameManager.changeObjText("Go home");
        Colliders.SetActive(false);
        GameManager.objectiveArrayDayOne[7] = 1;
        GameManager.taskFinished = true;
    }
}
