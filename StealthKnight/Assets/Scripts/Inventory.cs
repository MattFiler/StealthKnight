using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addInventoryItem(Item item)
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if(inventoryItems[i] != null)
            {
                inventoryItems[i] = item;
            }
        }
    }

    public void freeInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
