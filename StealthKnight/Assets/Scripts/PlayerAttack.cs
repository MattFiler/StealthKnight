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
    private GameObject showcase;

    [SerializeField] LookAt lookAt;

    void Update()
    {
        if(lookAt.lookAt) lookingAtShowcase = nearShowcase;
        canInteract = showcase != null && !showcase.GetComponent<ShatterShowcase>().shattered && lookingAtShowcase;
        if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space)) && canInteract)
        {
            knightAnimator.SetTrigger("Punch");
            SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.PUNCHING);
            showcase.GetComponent<ShatterShowcase>().Shatter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Smashable"))
        {
            nearShowcase = true;
            showcase = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Smashable"))
        {
            nearShowcase = false;
            showcase = null;
        }
    }
}
