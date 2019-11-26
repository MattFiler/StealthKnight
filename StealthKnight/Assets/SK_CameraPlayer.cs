using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The player-specific scripts for the camera system */
public class SK_CameraPlayer : MonoBehaviour
{
    /* Handle entry of camera zones */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SK_CAM_CORIDOOR")
        {
            SK_CameraManager.Instance.SetInCoridoor(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SK_CAM_CORIDOOR")
        {
            SK_CameraManager.Instance.SetInCoridoor(false);
        }
    }
}
