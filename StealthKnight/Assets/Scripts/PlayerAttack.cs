using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;
    public bool canInteract = false;
    [SerializeField] private Transform rayPoint;
    private bool lookingAtShowcase = false;
    private bool nearShowcase = false;
    private Collider showcaseCollider;

    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayPoint.position, transform.forward, out hit))
        {
            //Debug.DrawLine(rayPoint.position, hit.point);
            //Debug.Log(hit.collider.name);
            lookingAtShowcase = nearShowcase;
        }

        canInteract = showcaseCollider != null && !showcaseCollider.GetComponent<ShatterShowcase>().shattered && lookingAtShowcase;

        hand1.GetComponent<BoxCollider>().enabled = knightAnimator.GetBool("Attacking") && canInteract;
        hand2.GetComponent<BoxCollider>().enabled = knightAnimator.GetBool("Attacking") && canInteract;


        if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space)) && canInteract)
        {
            knightAnimator.SetTrigger("Punch");
            SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.PUNCHING);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Smashable"))
        {
            nearShowcase = true;
            showcaseCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Smashable"))
        {
            nearShowcase = false;
            showcaseCollider = null;
        }
    }
}
