using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBed : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] Animator anim;
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


    public void bodyDown()
    {
        inventoryTwo.bodyEquipped = false;
        anim.SetBool("BodyYes", true);
        GameManager.objectiveArrayDayOne[1] = 1;
        GameManager.changeObjText("Got to wash my hands first");
    }
}
