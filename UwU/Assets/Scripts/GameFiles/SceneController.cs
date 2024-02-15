using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
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
    public Animator transitionAnim;
    public GameObject canvas;
    public int sceneID;

    public void searchScenes(string searched)
    {
        if (searched == "Apartment")
            Apartment();
        if (searched == "Outside")
            Outside();
        if (searched == "MainMenu")
            MainMenu();
        if (searched == "Win")
            Win();
        if (searched == "Lose")
            Lose();

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
    public void MainMenu()
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

}