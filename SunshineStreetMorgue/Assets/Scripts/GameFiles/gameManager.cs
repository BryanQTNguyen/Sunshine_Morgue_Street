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
    public GameObject ObjectiveTextObj;
    public TMP_Text currentObjText;





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
    public int[] objectiveArrayDayTwo = new int[9];
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
    public int[] objectiveArrayDayThree = new int[3];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Saw Off Head
    */

    public int[] PrimaryObjective = new int[4];
    /* 
     0 = Get out the apartment
     1 = Get to work at the Morgue
     2 = Get home
     3 = Sleep
     Then this should reset to the next day
     */
    public int DayNumber = 1;


    private bool needToRelocate = false;

    private void Awake()
    {
        taskFinished = false;
        objectiveArrayDayOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        objectiveArrayDayTwo = new int[] { 0, 0, 0, 0, 0, 0 };
        objectiveArrayDayThree = new int[] { 0, 0, 0 };
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
        if(ObjectiveTextObj != null)
        {
            defineText();
        }

        //finds the SceneController
        if (sceneController != null)
        {
            SceneManagerObj = GameObject.Find("SceneController");
            sceneController = SceneManagerObj.GetComponent<SceneController>();
        }
        if((SceneManager.GetActiveScene().name == "Morgue 1" || SceneManager.GetActiveScene().name == "Morgue 2" || SceneManager.GetActiveScene().name == "Morgue 3")
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
    }


    public void changeObjText(string text)
    {
        currentObjText.text = text;
    }

    public void relocatePlayer()
    {
        needToRelocate = true;
    }

    public void defineText()
    {
        ObjectiveTextObj = GameObject.Find("CurrentObj");
        currentObjText = ObjectiveTextObj.GetComponent<TMP_Text>();
    }

}
