using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/* Main menu controller */
public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private Animator UIAnimator;
    [SerializeField] private Animator CameraAnimator;
    [SerializeField] private Transform PortalPosition;
    [SerializeField] private GameObject Camera;

    /* Menu animation - walk knight to portal */
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
            soundtrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            IsMovingCamera = false;
            SceneManager.LoadScene(1);
        }
    }

    /* Close the game */
    public void CloseGame()
    {
        Application.Quit();
    }

    /* Play the soundtrack */
    FMOD.Studio.EventInstance soundtrack;
    private void Start()
    {
        soundtrack = FMODUnity.RuntimeManager.CreateInstance("event:/menu/music");
        soundtrack.start();
    }
}
