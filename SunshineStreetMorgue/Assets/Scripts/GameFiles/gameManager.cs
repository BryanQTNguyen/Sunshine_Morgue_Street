using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class gameManager : MonoBehaviour
{
    public bool VHSVideoPlaying = false;
    public static gameManager Instance;
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    public bool isInTalkingRangeMain;

    public GameObject Character;

    public int[] objectiveArrayDayOne = new int[6];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Wash Hands
    3 = Wash body
    4 = Hydrate Skin/Hygiene
    5 = Burn Body
    */
    public string[] objectiveDayOneText = new string[6];

    public int[] objectiveArrayDayOneHalf = new int[6];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Wash Hands
    3 = Wash body
    4 = Hydrate Skin/Hygiene
    5 = Burn Body
    */
    public string[] objectiveDayOneHalfText = new string[6];


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

    public string[] objectiveDayTwoText = new string[9];


    public int[] objectiveArrayDayThree = new int[3];
    /*
    0 = Get body
    1 = Place Body Down
    2 = Saw Off Head
    */
    public string[] objectiveDayThreeText = new string[3];


    private bool needToRelocate = false;

    private void Awake()
    {
        objectiveArrayDayOne = new int[] { 0, 0, 0, 0, 0 };
        objectiveDayOneText = new string[] { "Get Body From Cabinet", "Place Body on Metal Bed", "Wash your hands", "Wash the Body", "Apply finishing touches with Hygine kit", "Burn the Body" };
        objectiveArrayDayTwo = new int[] { 0, 0, 0, 0, 0, 0 };
        objectiveDayOneText = new string[] { "Get Body From Cabinet", "Place Body on Metal Bed", "Strap it down", "Wash your hands", "Water Boa- Wash the body", "Saw Off Leg 1", "Saw off Leg 2",
            "Apply finishing touches with Hygine kit", "Burn the Body" };
        objectiveArrayDayThree = new int[] { 0, 0, 0 };
        objectiveDayThreeText = new string[] { "Get Body From Cabinet", "Place Body on Metal Bed", "Saw Off Head IMMEDIATELY" };

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

        //finds the SceneController
        if (sceneController != null)
        {
            SceneManagerObj = GameObject.Find("SceneController");
            sceneController = SceneManagerObj.GetComponent<SceneController>();
        }

        //relocates the player after moving from the morgue to outside 
        if(needToRelocate == true && SceneManager.GetActiveScene().name == "Outside")
        {
            Character = GameObject.FindWithTag("Player");
            Character.transform.position = new Vector3(103.55f, 1.04f, 110.27f);
            Debug.Log("I moved you");
            needToRelocate = false;
        }


        //Controls the objective messages
        if(SceneManager.GetActiveScene().name == "Morgue 1")
        {
            for (int i = 0; i < objectiveArrayDayOne.Length; i++)
            {
                if (objectiveArrayDayOne[i] == 1)
                {

                }
            }
        }




    }

    public void relocatePlayer()
    {
        needToRelocate = true;
    }

}
