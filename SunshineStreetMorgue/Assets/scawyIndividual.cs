using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scawyIndividual : MonoBehaviour
{
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject Myself;
    // Start is called before the first frame update
    void Start()
    {
        Myself.SetActive(true);
        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.objectiveArrayDayFour[4] == 1)
        {
            Myself.SetActive(false);
        }
    }
}
