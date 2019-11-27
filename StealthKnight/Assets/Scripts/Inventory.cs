using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject backpackObj;
    public GameObject prefabToSpawn;
    public float yOffset = 5.0f;
    public Item[] inventoryItems;
    public GameObject[] invetoryItemObjs;

    public void addInventoryItem(Item item)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                GameObject backpackPrefab = Instantiate(prefabToSpawn);
                backpackPrefab.transform.parent = backpackObj.transform;
                backpackPrefab.transform.position = backpackObj.transform.position;
                backpackPrefab.transform.localPosition += new Vector3(0, yOffset*i, 0);
                backpackPrefab.transform.localRotation = new Quaternion(0, 0, 0, 0);


                backpackPrefab.GetComponent<Item>().value = item.value;
                backpackPrefab.GetComponent<Item>().weight = item.weight;
                backpackPrefab.GetComponent<Item>().scaleOfObject = item.scaleOfObject;
                backpackPrefab.GetComponent<Item>().autoDestroy = false;

                invetoryItemObjs[i] = backpackPrefab;

                inventoryItems[i] = invetoryItemObjs[i].GetComponent<Item>();
                i = inventoryItems.Length * 2;
            }
        }
    }

    public void freeInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i] = null;
            invetoryItemObjs[i] = null;
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
