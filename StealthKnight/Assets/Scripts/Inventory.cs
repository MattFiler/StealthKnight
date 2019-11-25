using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] inventoryItems;

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

    public int getScore()
    {
        int overall_score = 0;
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            overall_score += inventoryItems[i].value;
        }

        return overall_score;
    }

}
