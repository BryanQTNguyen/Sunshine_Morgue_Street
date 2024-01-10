using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] GameObject obj, intText;
    public bool interactable;
    public string VHSName;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            //intText.SetActive(true);
            interactable = true;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable == true)
        {
            //picking it up 
            if (Input.GetKeyDown(KeyCode.E))
            {
                //intText.SetActive(false);
                // VHSName = GameObject.nam I left off here
                obj.SetActive(false);
                interactable = false; 
            }
        }
    }
}
