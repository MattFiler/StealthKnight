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
            //SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.HIT_BY_GUARD);
            SK_GaugeManager.Instance.GetHealthGaugeInstance().Reduce(SK_GaugeReductionTypes.HIT_BY_GUARD);
            other.GetComponent<PlayerMovement>().SpankPlayer();
        }
    }
}
