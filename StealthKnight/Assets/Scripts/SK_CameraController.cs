using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The controller for the camera movement logic */
public class SK_CameraController : MonoBehaviour
{
    //Tweakable values
    private int SkipLerpCount = 5; //The number of frames to initially skip lerping for
    private float BaseRotationLerpTimeModifier = 3.0f; //The base time to take to lerp the rotation
    private float MaxRotationLerpTimeModifier = 0.6f; //The max time to take to lerp the rotation when camera target has changed
    private float BasePositionLerpTimeModifier = 0.5f; //The base time to take to lerp the position
    private float MaxPositionLerpTimeModifier = 1.0f; //The max time to take to lerp the position when camera target has changed
    private float TravelDistanceSpeedModifier = 6.0f; //The position lerp speed modifier

    /* Get a referece to the camera object on start */
    private Camera CameraObject;
    private void Start()
    {
        CameraObject = GetComponent<Camera>();
    }

    /* Keep moving the camera to track our action */
    private float PositionLerpTimeModifier = 3.0f;
    private float RotationLerpTimeModifier = 3.0f;
    private int FamesSinceMotivationChange = 0;
    private bool ShouldDoNewMotivationLerpSpeed = false;
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

        //Change lerp speed if we just changed motivation direction
        if (SK_CameraManager.Instance.GetDidJustChangeMotivationDirection())
        {
            ShouldDoNewMotivationLerpSpeed = true;
        }
        if (ShouldDoNewMotivationLerpSpeed && (
            Quaternion.Euler(
                (float)decimal.Round((decimal)transform.rotation.x, 1), 
                (float)decimal.Round((decimal)transform.rotation.y, 1), 
                (float)decimal.Round((decimal)transform.rotation.z, 1)
            ) !=
            Quaternion.Euler(
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraRotation().x, 1), 
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraRotation().y, 1), 
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraRotation().z, 1)
            ) || 
            new Vector3(
                (float)decimal.Round((decimal)transform.position.x, 1),
                (float)decimal.Round((decimal)transform.position.y, 1),
                (float)decimal.Round((decimal)transform.position.z, 1)
            ) !=
            new Vector3(
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraPosition().x, 1),
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraPosition().y, 1),
                (float)decimal.Round((decimal)SK_CameraManager.Instance.GetIntendedCameraPosition().z, 1)
            )
            ))
        {
            Debug.Log("going sicko mode");
            RotationLerpTimeModifier = MaxRotationLerpTimeModifier;
            PositionLerpTimeModifier = MaxPositionLerpTimeModifier;
        }
        else
        {
            ShouldDoNewMotivationLerpSpeed = false;
            RotationLerpTimeModifier = BaseRotationLerpTimeModifier;
            PositionLerpTimeModifier = BasePositionLerpTimeModifier - ((Vector3.Distance(SK_CameraManager.Instance.GetIntendedCameraPosition(), transform.position) / TravelDistanceSpeedModifier));
            if (PositionLerpTimeModifier < 0) PositionLerpTimeModifier = 0;
        }

        //Lerp camera position to follow the manager dummy position
        Debug.Log("Speed: " + RotationLerpTimeModifier);
        Debug.Log("Current: " + transform.rotation);
        Debug.Log("Target: " + SK_CameraManager.Instance.GetIntendedCameraRotation());
        transform.rotation = Quaternion.Lerp(transform.rotation, SK_CameraManager.Instance.GetIntendedCameraRotation(), Time.deltaTime / RotationLerpTimeModifier);
        transform.position = Vector3.Lerp(transform.position, SK_CameraManager.Instance.GetIntendedCameraPosition(), Time.deltaTime / PositionLerpTimeModifier);
        CameraObject.fieldOfView = Mathf.Lerp(CameraObject.fieldOfView, SK_CameraManager.Instance.GetIntendedCameraFOV(), Time.deltaTime / PositionLerpTimeModifier);
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
