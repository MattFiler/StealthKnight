using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value = 10;
    public float weight = 1.0f;
    public float scaleOfObject = 1.0f;
    public bool autoDestroy = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickUpItem()
    {
        Inventory playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerInv.addInventoryItem(this);
        if(autoDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
