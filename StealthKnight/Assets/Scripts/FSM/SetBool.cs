using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBool : StateMachineBehaviour
{
    [SerializeField] private string boolName = "";
    [SerializeField] private bool boolValue = true;
    [SerializeField] private bool onEnterOrExit = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onEnterOrExit) return;
        Debug.Log("Setting " + boolName + " to " + boolValue);
        animator.SetBool(boolName, boolValue);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onEnterOrExit) return;
        animator.SetBool(boolName, boolValue);
    }

}
