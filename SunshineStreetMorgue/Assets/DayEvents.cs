using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEvents : MonoBehaviour
{
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        SceneManagerObj = GameObject.Find("SceneController");
        sceneController = SceneManagerObj.GetComponent<SceneController>();
        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();
        DayFlash();
    }
    public void DayFlash()
    {
        StartCoroutine(Flash());
    }
    public IEnumerator Flash()
    {
        yield return new WaitForSeconds(1.5f);
        sceneController.searchScenes("Apartment");
        GameManager.relocatePlayer();
    }
}
