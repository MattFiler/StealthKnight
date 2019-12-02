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
    public GameObject[] inventoryItemPrefabs;

    public bool startTop = false;
    public bool startAll = false;

    private void Update()
    {
        if(startTop)
        {
            dropTopItem();
            startTop = false;
        }

        if (startAll)
        {
            dropAllItems();
            startAll = false;
        }
    }

    public void addInventoryItem(Item item, GameObject itemGameObject)
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

                item.gameObject.transform.parent = backpackPrefab.transform;
                item.gameObject.transform.position = backpackPrefab.transform.position;
                //item.gameObject.transform.rotation = backpackPrefab.transform.rotation;

                inventoryItemPrefabs[i] = itemGameObject;
                inventoryItemPrefabs[i].SetActive(false);
                if(inventoryItemPrefabs[i].GetComponent<Rigidbody>() != null)
                {
                    Destroy(inventoryItemPrefabs[i].GetComponent<Rigidbody>());
                }

                backpackPrefab.GetComponent<Item>().value = item.value;
                backpackPrefab.GetComponent<Item>().weight = item.weight;
                backpackPrefab.GetComponent<Item>().scaleOfObject = item.scaleOfObject;
                backpackPrefab.GetComponent<Item>().autoDestroy = true;

                invetoryItemObjs[i] = backpackPrefab;

                inventoryItems[i] = invetoryItemObjs[i].GetComponent<Item>();
                i = inventoryItems.Length * 2;
            }
        }
    }

    public void freeInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i--)
        {
            inventoryItems[i] = null;
            invetoryItemObjs[i] = null;
        }
    }

    public void dropTopItem()
    {
        for(int i = invetoryItemObjs.Length - 1; i >= 0; i--)
        {
            if (invetoryItemObjs[i] != null)
            {
                invetoryItemObjs[i].AddComponent<Rigidbody>();
                invetoryItemObjs[i].GetComponent<BoxCollider>().enabled = true;
                //invetoryItemObjs[i].tag = "Item";

                invetoryItemObjs[i].GetComponent<Item>().prefabToDrop = inventoryItemPrefabs[i];
                invetoryItemObjs[i].GetComponent<Item>().wasDropped = true;
                invetoryItemObjs[i].GetComponent<Item>().autoRecreatePrefab = true;

                invetoryItemObjs[i].transform.parent = null;

                inventoryItemPrefabs[i] = null;
                invetoryItemObjs[i] = null;
                inventoryItems[i] = null;
                i = -50;
            }
        }
    }

    public void dropAllItems()
    {
        for (int i = invetoryItemObjs.Length - 1; i >= 0; i--)
        {
            if(invetoryItemObjs[i] != null)
            {
                invetoryItemObjs[i].AddComponent<Rigidbody>();
                invetoryItemObjs[i].GetComponent<BoxCollider>().enabled = true;
                //invetoryItemObjs[i].tag = "Item";

                invetoryItemObjs[i].GetComponent<Item>().prefabToDrop = inventoryItemPrefabs[i];
                invetoryItemObjs[i].GetComponent<Item>().wasDropped = true;
                invetoryItemObjs[i].GetComponent<Item>().autoRecreatePrefab = true;

                invetoryItemObjs[i].transform.parent = null;

                inventoryItemPrefabs[i] = null;
                invetoryItemObjs[i] = null;
                inventoryItems[i] = null;
            }
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
