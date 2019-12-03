using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private Animator UIAnimator;
    [SerializeField] private Animator CameraAnimator;
    [SerializeField] private Transform PortalPosition;
    [SerializeField] private GameObject Camera;
    private bool IsMovingCamera = false;
    private float TimeSinceStart = 0.0f;
    public void WalkKnightToPortal()
    {
        GetComponent<Animator>().SetBool("Sprinting", false);
        GetComponent<Animator>().SetFloat("Walk Speed", GetComponent<NavMeshAgent>().speed);
        GetComponent<NavMeshAgent>().SetDestination(PortalPosition.position);
        UIAnimator.SetBool("PopOut", true);
        CameraAnimator.SetBool("ShouldMove", true);
        IsMovingCamera = true;
    }
    private void Update()
    {
        if (!IsMovingCamera) return;
        TimeSinceStart += Time.deltaTime;
        if (TimeSinceStart >= 1.0f)
        {
            UIAnimator.SetBool("FlashOut", true);
        }
        if (TimeSinceStart >= 2.5f)
        {
            SceneManager.LoadScene(1);
            IsMovingCamera = false;
        }
    }
}
