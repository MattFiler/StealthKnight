using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;

    [HideInInspector]
    public bool isHandEmpty = true;

    public bool isLeftHand = true;
    public GameObject otherHand;

    public GameObject heldObject;

    public BoxCollider boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item" && heldObject == null)
        {
            Debug.Log(other.tag);

            heldObject = other.gameObject;

            isHandEmpty = false;
            otherHand.GetComponent<Hand>().isHandEmpty = false;
        }
        else if(other.CompareTag("Smashable") /*&& knightAnimator.GetBool("Attacking")*/)
        {
            other.GetComponent<ShatterShowcase>().Shatter();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (heldObject == other.gameObject)
        {
            heldObject = null;

            isHandEmpty = true;
            otherHand.GetComponent<Hand>().isHandEmpty = true;
        }
    }
}
