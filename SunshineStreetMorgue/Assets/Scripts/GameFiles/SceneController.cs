using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private bool SceneCoolDown = true;
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

        canvas = GameObject.Find("Canvas");
        transitionAnim = canvas.GetComponent<Animator>();

    }
    public void Update()
    {
        if(transitionAnim == null)
        {
            canvas = GameObject.Find("Canvas");
            transitionAnim = canvas.GetComponent<Animator>();
        }
    }
    public Animator transitionAnim;
    public GameObject canvas;
    public int sceneID;

    public void searchScenes(string searched)
    {
        if(SceneCoolDown == true)
        {
            if (searched == "Apartment")
            {
                Apartment();
                SceneCoolDown = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Outside")
            {
                Outside();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Morgue 1")
            {
                Morgue();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Morgue 2")
            {
                Morgue2();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Morgue 3")
            {
                Morgue3();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Morgue 4")
            {
                Morgue4();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "MainMenu")
            {
                MainMenu();
                SceneCoolDown = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                StartCoroutine(SceneCool());
            }
            if (searched == "Day One")
            {
                Day1();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Day Two")
            {
                Day2();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Day Three")
            {
                Day3();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Day Four")
            {
                Day4();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "EndGame")
            {
                EndGame();
                SceneCoolDown = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                StartCoroutine(SceneCool());
            }



        }
        else if (SceneCoolDown == false)
            StartCoroutine(SceneCool());
    }
    public void MainMenu()
    {
        sceneID = 0;
        StartCoroutine(LoadScene());

    }
    public void Apartment()
    {
        sceneID = 1;
        StartCoroutine(LoadScene());

    }
    public void Outside()
    {
        sceneID = 2;
        StartCoroutine(LoadScene());

    }
    public void Morgue()
    {
        sceneID = 3;
        StartCoroutine(LoadScene());
    }
    public void Morgue2()
    {
        sceneID = 4;
        StartCoroutine(LoadScene());
    }
    public void Morgue3()
    {
        sceneID = 5;
        StartCoroutine(LoadScene());
    }
    public void Morgue4()
    {
        sceneID = 6;
        StartCoroutine(LoadScene());
    }
    public void Day1()
    {
        sceneID = 7;
        StartCoroutine(LoadScene());
    }
    public void Day2()
    {
        sceneID = 8;
        StartCoroutine(LoadScene());
    }
    public void Day3()
    {
        sceneID = 9;
        StartCoroutine(LoadScene());
    }
    public void Day4()
    {
        sceneID = 10;
        StartCoroutine(LoadScene());
    }
    public void EndGame()
    {
        sceneID = 11;
        StartCoroutine(LoadScene());
    }



    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneID);
    }
    IEnumerator SceneCool()
    {
        yield return new WaitForSeconds(3f);
        SceneCoolDown = true;
    }

}