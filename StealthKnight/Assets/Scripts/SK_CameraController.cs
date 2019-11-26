using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The controller for the camera movement logic */
public class SK_CameraController : MonoBehaviour
{
    //Tweakable values
    private int SkipLerpCount = 5; //The number of frames to initially skip lerping for
    private float TravelDistanceSpeedModifier = 6.0f; //The lerp speed modifier
    private float RotationLerpTimeModifier = 3.0f; //The base time to take to lerp the rotation
    private float BasePositionLerpTimeModifier = 3.0f; //The base time to take to lerp the position

    /* Get a referece to the camera object on start */
    private Camera CameraObject;
    private void Start()
    {
        CameraObject = GetComponent<Camera>();
    }

    /* Keep moving the camera to track our action */
    private float PositionLerpTimeModifier = 3.0f;
    private void Update()
    {
        IsInPlayMode = true;

        //Skip the lerp on first few runs to get us into the right position
        if (SkipLerpCount != 0)
        {
            transform.rotation = SK_CameraManager.Instance.GetIntendedCameraRotation();
            transform.position = SK_CameraManager.Instance.GetIntendedCameraPosition();
            CameraObject.fieldOfView = SK_CameraManager.Instance.GetIntendedCameraFOV();
            SkipLerpCount--;
            return;
        }

        //Lerp camera position to follow the manager dummy position
        transform.rotation = Quaternion.Lerp(transform.rotation, SK_CameraManager.Instance.GetIntendedCameraRotation(), Time.deltaTime / RotationLerpTimeModifier);
        transform.position = Vector3.Lerp(transform.position, SK_CameraManager.Instance.GetIntendedCameraPosition(), Time.deltaTime / PositionLerpTimeModifier);
        CameraObject.fieldOfView = Mathf.Lerp(CameraObject.fieldOfView, SK_CameraManager.Instance.GetIntendedCameraFOV(), Time.deltaTime / PositionLerpTimeModifier);

        //Modify the camera lerp time based on the distance to travel
        PositionLerpTimeModifier = BasePositionLerpTimeModifier - ((Vector3.Distance(SK_CameraManager.Instance.GetIntendedCameraPosition(), transform.position) / TravelDistanceSpeedModifier));
        if (PositionLerpTimeModifier < 0) PositionLerpTimeModifier = 0;
    }

    /* Draw a debug pointer for the look location in editor */
    private bool IsInPlayMode = false;
    private void OnDrawGizmosSelected()
    {
        if (!IsInPlayMode) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(SK_CameraManager.Instance.GetIntendedCameraLookAtPosition(), 5);
    }
}
