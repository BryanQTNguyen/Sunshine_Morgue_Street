using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCabinet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventoryTwo inventoryTwo;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventoryTwo = player.GetComponent<InventoryTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventoryTwo == null)
        {
            player = GameObject.Find("Player");
            inventoryTwo = player.GetComponent<InventoryTwo>();
        }
    }
    public void BodyOut()
    {
        anim.SetTrigger("BodyOut");
        StartCoroutine(BodyTakeOut());
    }
    public IEnumerator BodyTakeOut()
    {
        yield return new WaitForSeconds(5f);
        inventoryTwo.bodyEquipped = true;
    }
}
