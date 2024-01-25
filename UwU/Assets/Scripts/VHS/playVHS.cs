using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVHS : MonoBehaviour
{
    [SerializeField] GameObject managerObj;
    [SerializeField] gameManager GameManager;
    [SerializeField] GameObject VHSTapeObj;


    public GameObject intText, TVOff, vhs;
    public GameObject TVDefaultOn, TVOn, TVOnTwo, TVOnThree, TVDefaulfOff;
    public int WhatTape;
    public Animator vhsAnim;
    public Animator vhsTapeAnim;
    public float videoTime;
    bool tapeOutToggle;
    public bool videoPlayingToggle;

    private void Awake()
    {
        managerObj = GameObject.Find("gameManager");
        GameManager = managerObj.GetComponent<gameManager>();
        vhsAnim = gameObject.GetComponent<Animator>();
    }
       
    void Update()
    {
        if (tapeOutToggle)
        {
            vhsAnim.SetTrigger("playOut");
            vhsTapeAnim.SetBool("TapeOut", true);
            vhsTapeAnim.SetBool("TapeIn", false);

            tapeOutToggle = false;

        }
    }

    public void PlayVHSTape(string VHSTapeName, float VideoTime)
    {
        if ((VHSTapeName == "Tape1" || VHSTapeName == "Tape2" || VHSTapeName == "Tape3") && videoPlayingToggle == false)
        {
            videoPlayingToggle = true;
            GameManager.VHSVideoPlaying = videoPlayingToggle;
            videoTime = VideoTime;

            VHSTapeObj = GameObject.Find(VHSTapeName);
            vhsTapeAnim = VHSTapeObj.GetComponent<Animator>();


            vhsTapeAnim.SetBool("TapeIn", true);
            vhsTapeAnim.SetBool("TapeOut", false);

            vhsAnim.SetTrigger("playIn");
            AudManager.Instance.PlaySFX("VHSInsert");


            if (VHSTapeName == "Tape1")
                WhatTape = 1;
            if (VHSTapeName == "Tape2")
                WhatTape = 2;
            if (VHSTapeName == "Tape3")
                WhatTape = 3;

            StartCoroutine(playVHSTape());
        }
        else
        {
            Debug.Log("This is no VHS");
        }
    }

    IEnumerator playVHSTape()
    {
        yield return new WaitForSeconds(2f);
        TVOff.SetActive(false);
        TVDefaultOn.SetActive(true);
        
        yield return new WaitForSeconds(8f);
        TVDefaultOn.SetActive(false);
        if (WhatTape ==1)
            TVOn.SetActive(true);
        if (WhatTape == 2)
            TVOnTwo.SetActive(true);
        if (WhatTape == 3)
            TVOnThree.SetActive(true);
       
        yield return new WaitForSeconds(videoTime);
        if (WhatTape == 1)
            TVOn.SetActive(false);
        if (WhatTape == 2)
            TVOnTwo.SetActive(false);
        if (WhatTape == 3)
            TVOnThree.SetActive(false);
        TVDefaulfOff.SetActive(true);

        yield return new WaitForSeconds(1.2f);
        TVDefaulfOff.SetActive(false);

        tapeOutToggle = true;
        videoPlayingToggle = false;
        GameManager.VHSVideoPlaying = videoPlayingToggle;
        TVOff.SetActive(true);
        TVOn.SetActive(false);
        TVOnTwo.SetActive(false);
        TVOnThree.SetActive(false);
    }
}
