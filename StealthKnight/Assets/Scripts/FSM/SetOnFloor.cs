using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFloor : StateMachineBehaviour
{
    [SerializeField] private bool onFloor = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onFloor) return;
        animator.SetBool("On Floor", onFloor);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onFloor) return;
        animator.SetBool("On Floor", onFloor);
    }

}
