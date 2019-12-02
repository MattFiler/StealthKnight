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
            SK_CameraManager.Instance.SetLocationMotivation(SK_CameraPositionMotivation.CORIDOOR);
        }
        else if (other.tag == "SK_CAM_ATRIUM")
        {
            SK_CameraManager.Instance.SetLocationMotivation(SK_CameraPositionMotivation.ATRIUM);
        }
        else if (other.tag == "SK_CAM_NORTH")
        {
            SK_CameraManager.Instance.SetMotivationDirection(SK_CameraDirectionMotivation.NORTH);
        }
        else if (other.tag == "SK_CAM_SOUTH")
        {
            SK_CameraManager.Instance.SetMotivationDirection(SK_CameraDirectionMotivation.SOUTH);
        }
        else if (other.tag == "SK_CAM_EAST")
        {
            SK_CameraManager.Instance.SetMotivationDirection(SK_CameraDirectionMotivation.EAST);
        }
        else if (other.tag == "SK_CAM_WEST")
        {
            SK_CameraManager.Instance.SetMotivationDirection(SK_CameraDirectionMotivation.WEST);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SK_CAM_CORIDOOR")
        {
            SK_CameraManager.Instance.SetLocationMotivation(SK_CameraPositionMotivation.PASSIVE);
        }
        else if (other.tag == "SK_CAM_ATRIUM")
        {
            SK_CameraManager.Instance.SetLocationMotivation(SK_CameraPositionMotivation.PASSIVE);
        }
    }
}
