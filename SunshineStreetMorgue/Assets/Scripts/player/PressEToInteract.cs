using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEToInteract : MonoBehaviour
{
    [SerializeField] GameObject PressEText;
    public bool show = false;
    public string[] messages = new string[2];
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            show = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            show = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        messages = new string[2] { "Press 'E' To Interact", " " };
    }

    // Update is called once per frame
    void Update()
    {
        if(PressEText == null)
        {
            PressEText = GameObject.Find("EToInteract");
        }


        if(show == true)
        {
            PressEText.SetActive(true);
        }else if (PressEText.active == true)
        {
            PressEText.SetActive(false);

        }
    }
}
