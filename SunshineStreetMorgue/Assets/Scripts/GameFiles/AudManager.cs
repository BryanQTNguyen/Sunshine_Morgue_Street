using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class AudManager : MonoBehaviour
{
    public static AudManager Instance;
    private int musicType = 0;

    public Sound[] musicSounds, sfxSounds, dialogueSound, rainSounds;
    public AudioSource musicSource, sfxSource,dialogueSource, rainSource;

    //[SerializeField] DialgoueManager dialogueManager;
    [SerializeField] GameObject dialogueObject;

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
    private void Start()
    {

    }
    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" && musicType != 1)
        {
            PlayMusic("ApartAmbient");
            rainSource.volume = 0;
            PlayRain("RainOutside");
            musicType = 1;
        }
        if (SceneManager.GetActiveScene().name != "Outside" && rainSource.volume == 1)
        {
            rainSource.volume = 0;
        }
        if (SceneManager.GetActiveScene().name == "Outside" && rainSource.volume == 0)
        {
            rainSource.volume = 1;
        }





        /*
         This is for managing the dialogue audio
        if (SceneManager.GetActiveScene().name == "cutSceneFirst")
        {
            dialogueObject = GameObject.FindWithTag("dialogueManager");
            dialogueManager = dialogueObject.GetComponent<DialgoueManager>();
            if (dialogueManager != null)
            {
                if (dialogueManager.muteDialogueAudio == true)
                {
                    dialogueSource.volume = 0;
                }
                else if (dialogueManager.muteDialogueAudio == false)
                {
                    dialogueSource.volume = 1;
                }

            }
            else
            {
                Debug.Log("Cannot find the Dialogue Manager (line 73)");
            }
        }
        */
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found!!!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlayRain(string name)
    {
        Sound s = Array.Find(rainSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Rain not found!!!");
        }
        else
        {
            rainSource.clip = s.clip;
            rainSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Soundeffect not found!!!");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayDialogue(string name)
    {
        Sound s = Array.Find(dialogueSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Dialgoue sound not found!!!");
        }
        else
        {
            dialogueSource.clip = s.clip;
            dialogueSource.Play();
        }
    }
}
