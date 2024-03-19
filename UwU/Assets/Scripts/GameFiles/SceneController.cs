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
                StartCoroutine(SceneCool());
            }
            if (searched == "Outside")
            {
                Outside();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Morgue")
            {
                Morgue();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "MainMenu")
            {
                MainMenu();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Win")
            {
                Win();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }
            if (searched == "Lose")
            {
                Lose();
                SceneCoolDown = false;
                StartCoroutine(SceneCool());
            }

        }
        else if (SceneCoolDown == false)
            StartCoroutine(SceneCool());
    }

    public void Apartment()
    {
        sceneID = 0;
        StartCoroutine(LoadScene());

    }
    public void Outside()
    {
        sceneID = 1;
        StartCoroutine(LoadScene());

    }
    public void Morgue()
    {
        sceneID = 2;
        StartCoroutine(LoadScene());
    }
    public void Win()
    {
        sceneID = 3;
        StartCoroutine(LoadScene());

    }
    public void Lose()
    {
        sceneID = 4;
        StartCoroutine(LoadScene());

    }
    public void MainMenu()
    {
        sceneID = 5;
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