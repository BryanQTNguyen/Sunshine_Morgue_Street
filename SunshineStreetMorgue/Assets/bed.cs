using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bed : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    public string SceneToo;
    // Start is called before the first frame update
    void Start()
    {
        SceneManagerObj = GameObject.Find("SceneController");
        sceneController = SceneManagerObj.GetComponent<SceneController>();
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

        if (GameManager.DayNumber == 2)
            SceneToo = "Day Two";
        if (GameManager.DayNumber == 3)
            SceneToo = "Day Three";
        if(GameManager == null)
        {
            GameManagerObj = GameObject.Find("gameManager");
            GameManager = GameManagerObj.GetComponent<gameManager>();
        }
    }


    public void Sleep()
    {
        GameManager.DayOver = false;
        GameManager.PrimaryObjective[0] = 0;
        GameManager.PrimaryObjective[1] = 0;
        GameManager.PrimaryObjective[2] = 0;
        GameManager.PrimaryObjective[3] = 0;
        GameManager.taskStarted = false;
        GameManager.taskFinished = false;
        GameManager.DayNumber++;
        if (GameManager.DayNumber == 2)
            SceneToo = "Day Two";
        if (GameManager.DayNumber == 3)
            SceneToo = "Day Three";
        sceneController.searchScenes(SceneToo);
    }
}
