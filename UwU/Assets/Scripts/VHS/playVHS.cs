using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playVHS : MonoBehaviour
{
    public GameObject intText, TVOff, vhs;
    public GameObject TVOn, TVOnTwo, TVOnThree;
    public bool interactable, toggle;
    public Animator vhsAnim;
    public float videoTime;

    private void Start()
    {
        toggle = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (toggle == false)
            {
                if (vhs.active == false)
                {
                    //intText.SetActive(false);
                    interactable = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            //intText.SetActive(false);
            interactable = false;
        }
    }
    public void playVHSstarter()
    {
        
    }

    void Update()
    {
        if(interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //intText.SetActive(false);
                vhsAnim.SetTrigger("playIn");
                StartCoroutine(playVHSTape());
                toggle = true;
                interactable = false;
            }
        }
    }

    IEnumerator playVHSTape()
    {
        yield return new WaitForSeconds(2f);
        TVOff.SetActive(false);
        TVOn.SetActive(true);
        yield return new WaitForSeconds(videoTime);
        TVOff.SetActive(true);
        vhsAnim.SetTrigger("playOut");
        TVOn.SetActive(false);
    }
}
