using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InvText : MonoBehaviour
{
    int maxinv = 9;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       TextMeshProUGUI test  = GetComponent<TextMeshProUGUI>();


        Inventory invonscreen = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
       
        
        //  test.SetText("HELP");
        Debug.Log("WAH");
        for (int i = 0; i < maxinv; ++i)
        {
             test.SetText(invonscreen.inventoryItems[i].itemName);
            
        }
    }
}
