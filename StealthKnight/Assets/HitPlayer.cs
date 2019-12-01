using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField] private Animator guardAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(guardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SpankPlayer();
        }
    }
}
