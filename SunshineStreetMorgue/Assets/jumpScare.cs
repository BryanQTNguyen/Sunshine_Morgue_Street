using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class jumpScare : MonoBehaviour
{
    [SerializeField] GameObject GameManagerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject JumpScare;
    [SerializeField] GameObject SceneManagerObj;
    [SerializeField] SceneController sceneController;
    [SerializeField] GameObject audManager;
    [SerializeField] GameObject audManagerObj;

    public AudioSource sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        JumpScare.SetActive(false);
        audManager = GameObject.Find("AudManager");
        audManagerObj = audManager.transform.GetChild(1).gameObject;
        sfxSource = audManagerObj.GetComponent<AudioSource>();


        GameManagerObj = GameObject.Find("gameManager");
        GameManager = GameManagerObj.GetComponent<gameManager>();
        SceneManagerObj = GameObject.Find("SceneController");
        sceneController = SceneManagerObj.GetComponent<SceneController>();
        GameManager.death = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider col)
    {
        if (GameManager.objectiveArrayDayFour[4] == 1)
        {
            AudManager.Instance.PlaySFX("JumpScare");
            sfxSource.volume = 1;
            JumpScare.SetActive(true);
            StartCoroutine(JUMP());
        }
    }
    public IEnumerator JUMP()
    {
        yield return new WaitForSeconds(2f);

        sceneController.searchScenes("EndGame");
        sfxSource.volume = 0;

        GameManager.death = true;
    }
}

