using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value = 10;
    public float weight = 1.0f;
    public float scaleOfObject = 1.0f;
    public float velocityAfterDrop = 2.5f;

    public bool autoDestroy = false;
    public bool autoRecreatePrefab = false;
    public bool wasDropped = false;

    public GameObject prefabToDrop;

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
        bool isInvFull = playerInv.addInventoryItem(this, this.gameObject);
        //Debug.Log(playerInv);
        if(!isInvFull)
        {
            if (autoDestroy)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.GetComponent<MeshCollider>().enabled = false;
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            if (wasDropped && autoRecreatePrefab)
            {
                prefabToDrop.transform.parent = null;
                Destroy(this.gameObject);

                Rigidbody rigidbody = prefabToDrop.AddComponent<Rigidbody>();
                rigidbody.mass = 500000;

                rigidbody.velocity = new Vector3(rigidbody.velocity.x + Random.Range(-velocityAfterDrop, velocityAfterDrop), rigidbody.velocity.y, rigidbody.velocity.z + Random.Range(-velocityAfterDrop, velocityAfterDrop));

                foreach (MeshCollider meshCollider in prefabToDrop.GetComponents<MeshCollider>())
                {
                    meshCollider.enabled = true;
                }
                foreach (MeshRenderer meshRenderer in prefabToDrop.GetComponents<MeshRenderer>())
                {
                    meshRenderer.enabled = true;
                }
                prefabToDrop.tag = "Item";

                prefabToDrop.GetComponent<Item>().autoRecreatePrefab = false;
                //prefabToDrop.transform.position = prefabToDrop.transform.position + prefabToDrop.transform.up;
                prefabToDrop.SetActive(true);


                //Destroy(this.gameObject);
            }
        }

    }
}
