using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVHS : MonoBehaviour
{
    public GameObject intText, TVOff, vhs;
    public GameObject TVOn, TVOnTwo, TVOnThree;
    public int WhatTape;
    public Animator vhsAnim;
    public float videoTime;
    bool tapeOutToggle;

    private void Start()
    {
    }

    void Update()
    {
        if (tapeOutToggle)
        {
            vhsAnim.SetTrigger("playOut");
            tapeOutToggle = false;

        }
    }

    public void PlayVHSTape(string VHSTapeName, float VideoTime)
    {
        if (VHSTapeName == "Tape1" || VHSTapeName == "Tape2" || VHSTapeName == "Tape3")
        {
            videoTime = VideoTime;
            vhsAnim.SetTrigger("playIn");
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
        if(WhatTape ==1)
            TVOn.SetActive(true);
        if (WhatTape == 2)
            TVOnTwo.SetActive(true);
        if (WhatTape == 3)
            TVOnThree.SetActive(true);
        yield return new WaitForSeconds(videoTime);
        tapeOutToggle = true;
        TVOff.SetActive(true);
        TVOn.SetActive(false);
        TVOnTwo.SetActive(false);
        TVOnThree.SetActive(false);
    }
}
