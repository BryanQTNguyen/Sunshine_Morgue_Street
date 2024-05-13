using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using TMPro;
using System.Globalization;


public class gameManager : MonoBehaviour
{
    public bool VHSVideoPlaying = false;
    public static gameManager Instance;
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    public bool isInTalkingRangeMain;
    public bool taskStarted; // for morgue gameplay
    public bool taskFinished;
    public GameObject Character;

    public GameObject canvas;
    public GameObject ObjectiveTextObj;
    public TMP_Text currentObjText;


    public bool death;


    //Day one helping
    [SerializeField] GameObject ApplyHygiene;
    [SerializeField] GameObject Repick;

    public int[] objectiveArrayDayOne = new int[8];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Wash Hands
    3 = Wash body
    4 = Hydrate Skin/Hygiene
    5 = Burn Body
    */
    public int[] objectiveArrayDayTwo = new int[8];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Strap it down
    3 = Wash Hands
    4 = Water Boa- Wash the body
    5 = Saw Off Leg 1
    6 = Saw off Leg 2
    7 = Hydrate Skin/Hygiene
    8 = Burn the body
    */
    public int[] objectiveArrayDayThree = new int[9];
    /*
    0 = Get body
    1 = Place Body Down
    3 = Wash Hands
    4 = Water Body
    5 = Saw Off Leg 1
    6 = Saw Off Leg 2
    7 = Pick up body
    8 = burn body
    */
    public int[] objectiveArrayDayFour = new int[5];

    public int[] PrimaryObjective = new int[4];
    /* 
     0 = Get out the apartment
     1 = Get to work at the Morgue
     2 = Get home
     3 = Sleep
     Then this should reset to the next day
     */
    public int DayNumber = 1;
    public bool DayOver;
    public bool pulse = false;


    private bool needToRelocate = false;

    private void Awake()
    {
        taskFinished = false;
        objectiveArrayDayOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        objectiveArrayDayTwo = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        objectiveArrayDayThree = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        objectiveArrayDayFour = new int[] { 0, 0, 0, 0, 0 };

        PrimaryObjective = new int[] { 0, 0, 0, 0 };

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(death == true)
        {
            death = false;
            Destroy(gameObject);
        }
        if ((SceneManager.GetActiveScene().name != "Morgue 1" && SceneManager.GetActiveScene().name != "Morgue 2" && SceneManager.GetActiveScene().name != "Morgue 3" && SceneManager.GetActiveScene().name != "Morgue 4" && SceneManager.GetActiveScene().name != "MainMenu" &&
            SceneManager.GetActiveScene().name != "EndGame") && pulse == false && death == false)
        {
            if (PrimaryObjective[0] == 0)
                changeObjText("Get out the apartment");
            if (PrimaryObjective[0] == 1)
                changeObjText("Get to work at the Morgue");
            if (PrimaryObjective[1] == 1)
                changeObjText("I need to get home");
            if (PrimaryObjective[2] == 1)
                changeObjText("I better sleep");
        }

        if (ObjectiveTextObj == null)
        {
            defineText();
        }

        //finds the SceneController
        if (sceneController == null)
        {
            SceneManagerObj = GameObject.Find("SceneController");
            sceneController = SceneManagerObj.GetComponent<SceneController>();
        }
        if((SceneManager.GetActiveScene().name == "Morgue 1" || SceneManager.GetActiveScene().name == "Morgue 2" || SceneManager.GetActiveScene().name == "Morgue 3" || SceneManager.GetActiveScene().name == "Morgue 4")
            && taskFinished == false)
        {
            taskStarted = true;
        }

        //relocates the player after moving from the morgue to outside 
        if(needToRelocate == true && SceneManager.GetActiveScene().name == "Outside")
        {
            Character = GameObject.FindWithTag("Player");
            Character.transform.position = new Vector3(116.68f, 0.31f, 101.34f);
            needToRelocate = false;
        }
        if(needToRelocate == true && SceneManager.GetActiveScene().name == "Apartment")
        {
            Character = GameObject.FindWithTag("Player");
            Character.transform.position = new Vector3(-0.848f, 0.82361f, -1.69f);
            needToRelocate = false;
        }

        //Day One Morgue Helps For applying hygiene
        if (SceneManager.GetActiveScene().name == "Morgue 1")
        {
            if(ApplyHygiene == null || Repick == null)
            {
                ApplyHygiene = GameObject.Find("ApplyHygiene");
                Repick = GameObject.Find("Repick");

            }
            if (Repick == null) 
                ApplyHygiene = GameObject.Find("Repick");

            if (objectiveArrayDayOne[4] == 1 && objectiveArrayDayOne[5] != 1)
            {
                ApplyHygiene.SetActive(true);
                Repick.SetActive(false);
            }
            else if (objectiveArrayDayOne[5] == 1)
            {
                Repick.SetActive(true);
                ApplyHygiene.SetActive(false);
            }
            else
            {
                Repick.SetActive(false);
                ApplyHygiene.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().name == "Morgue 2")
        {
            if (ApplyHygiene == null || Repick == null)
            {
                ApplyHygiene = GameObject.Find("ApplyHygiene");
                Repick = GameObject.Find("Repick");

            }
            if (Repick == null)
                ApplyHygiene = GameObject.Find("Repick");

            if (objectiveArrayDayTwo[4] == 1 && objectiveArrayDayTwo[5] != 1)
            {
                ApplyHygiene.SetActive(true);
                Repick.SetActive(false);
            }
            else if (objectiveArrayDayTwo[5] == 1)
            {
                Repick.SetActive(true);
                ApplyHygiene.SetActive(false);
            }
            else
            {
                Repick.SetActive(false);
                ApplyHygiene.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().name == "Morgue 3")
        {
            if (ApplyHygiene == null || Repick == null)
            {
                ApplyHygiene = GameObject.Find("ApplyHygiene");
                Repick = GameObject.Find("Repick");
            }
            if (Repick == null)
                ApplyHygiene = GameObject.Find("Repick");

            if (objectiveArrayDayThree[4] == 0)
            {
                Repick.SetActive(false);
                ApplyHygiene.SetActive(false);
            }

            if (objectiveArrayDayThree[4] == 1 && objectiveArrayDayThree[6] != 1)
            {
                ApplyHygiene.SetActive(true);
                Repick.SetActive(false);
            } 
            if (objectiveArrayDayThree[6] == 1)
            {
                Repick.SetActive(true);
            }
        }
    }


    public void changeObjText(string text)
    {
        defineText();
        currentObjText.text = text;
    }

    public void relocatePlayer()
    {
        needToRelocate = true;
    }

    public void defineText()
    {
        canvas = GameObject.Find("Canvas");
        ObjectiveTextObj = canvas.transform.GetChild(0).gameObject;
        currentObjText = ObjectiveTextObj.GetComponent<TMP_Text>();
    }

}
