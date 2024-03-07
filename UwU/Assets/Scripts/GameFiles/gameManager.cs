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

    private bool needToRelocate = false;

    private void Awake()
    {
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
        if (sceneController != null)
        {
            SceneManagerObj = GameObject.Find("SceneController");
            sceneController = SceneManagerObj.GetComponent<SceneController>();
        }
        if(needToRelocate == true && SceneManager.GetActiveScene().name == "Outside")
        {
            Character = GameObject.FindWithTag("Player");
            Character.transform.position = new Vector3(103.55f, 1.04f, 110.27f);
            Debug.Log("I moved you");
            needToRelocate = false;
        }
    }

    public void relocatePlayer()
    {
        needToRelocate = true;
    }

}
