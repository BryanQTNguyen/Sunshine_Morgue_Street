using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class doorOpen : MonoBehaviour
{
    public string SceneTo;
    public bool MorgueToOut;
    public bool isMorgueDoor;
    [SerializeField] GameObject managerObj;
    [SerializeField] gameManager GameManager;

    void Update()
    {
        if(isMorgueDoor == true)
        {
            if(GameManager == null)
            {
                managerObj = GameObject.Find("gameManager");
                GameManager = managerObj.GetComponent<gameManager>();
            }
            if(GameManager.DayNumber == 1)
                SceneTo = "Morgue 1";
            if (GameManager.DayNumber == 2)
                SceneTo = "Morgue 2";
            if (GameManager.DayNumber == 3)
                SceneTo = "Morgue 3";
            if (GameManager.DayNumber == 4)
                SceneTo = "Morgue 4";
        }
    }
}


