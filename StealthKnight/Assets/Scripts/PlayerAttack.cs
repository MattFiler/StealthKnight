using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space))
        {
            knightAnimator.SetTrigger("Punch");
            SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.PUNCHING);
        }
    }
}
