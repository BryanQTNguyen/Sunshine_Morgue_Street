using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BodyCabinet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] Animator anim;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject Collider;
    // Start is called before the first frame update
    void Start()
    {
        Collider.SetActive(false);
        player = GameObject.Find("Player");
        inventoryTwo = player.GetComponent<InventoryTwo>();
        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventoryTwo == null)
        {
            player = GameObject.Find("Player");
            inventoryTwo = player.GetComponent<InventoryTwo>();
        }
    }
    public void BodyOut()
    {
        GameManager.taskStarted = true;        
        anim.SetTrigger("BodyOut");
        Collider.SetActive(true);
        StartCoroutine(BodyTakeOut());
    }
    public IEnumerator BodyTakeOut()
    {
        yield return new WaitForSeconds(5f);
        inventoryTwo.bodyEquipped = true;
        updateList();
        Collider.SetActive(false);
        GameManager.changeObjText("Now I place the body on the metal bed");
    }
    public void updateList()
    {
        if(SceneManager.GetActiveScene().name == "Morgue 1")
            GameManager.objectiveArrayDayOne[0] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 2")
            GameManager.objectiveArrayDayTwo[0] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 3")
            GameManager.objectiveArrayDayThree[0] = 1;
        if (SceneManager.GetActiveScene().name == "Morgue 4")
            GameManager.objectiveArrayDayFour[0] = 1;
    }
}
