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
    [SerializeField] Animator anim;
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
        StartCoroutine(ApplyingKit());

    }
    public IEnumerator ApplyingKit()
    {
        yield return new WaitForSeconds(20f);
        GameManager.objectiveArrayDayOne[5] = 1;
        Colliders.SetActive(false);
        GameManager.changeObjText("Pick up and Burn the Body");
    }
}
