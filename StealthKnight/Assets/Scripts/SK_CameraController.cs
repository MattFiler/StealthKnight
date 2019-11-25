using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The controller for the camera movement logic */
public class SK_CameraController : MonoBehaviour
{
    private float CameraDistCutoff = 30.0f;

    private void Update()
    {
        IsInPlayMode = true;

        transform.rotation = Quaternion.Lerp(transform.rotation, SK_CameraManager.Instance.GetIntendedCameraRotation(), Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, SK_CameraManager.Instance.GetIntendedCameraPosition(), Time.deltaTime);
    }

    /* Draw a debug pointer for the look location */
    private bool IsInPlayMode = false;
    private void OnDrawGizmosSelected()
    {
        if (!IsInPlayMode) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(SK_CameraManager.Instance.GetIntendedCameraLookAtPosition(), 5);
    }
}
