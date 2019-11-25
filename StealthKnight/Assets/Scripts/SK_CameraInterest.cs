using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An object that the camera can be interested in */
public class SK_CameraInterest : MonoBehaviour
{
    private float DistToCamera = 0.0f;
    private float PrevDistToCamera = 0.0f;
    private float DistToPlayer = 0.0f;
    private float PrevDistToPlayer = 0.0f;
    private bool IsInfrontCamera = false;
    private bool IsEnabled = true;

    private float TimeSinceLastTrack = 0.0f;

    /* When initialised, add to our object tracker */
    private void Start()
    {
        SK_CameraManager.Instance.AddInterest(this);
    }

    /* Update trackable object values */
    private void Update()
    {
        if (!IsEnabled) return;

        TimeSinceLastTrack += Time.deltaTime;
        if (TimeSinceLastTrack < SK_CameraManager.Instance.GetTickTime()) return;
        TimeSinceLastTrack = 0.0f;

        PrevDistToCamera = DistToCamera;
        DistToCamera = Vector3.Distance(transform.position, SK_CameraManager.Instance.GetCamera().transform.position);
        PrevDistToPlayer = DistToPlayer;
        DistToPlayer = Vector3.Distance(transform.position, SK_CameraManager.Instance.GetPlayer().transform.position);

        IsInfrontCamera = new Plane(SK_CameraManager.Instance.GetCamera().transform.forward, SK_CameraManager.Instance.GetCamera().transform.position).GetSide(transform.position);
    }

    /* Get tracked values */
    public float GetDistanceToCamera()
    {
        return DistToCamera;
    }
    public float GetDistanceToCameraChange()
    {
        float DistChange = DistToCamera - PrevDistToCamera;
        if (DistChange < 0) DistChange *= -1;
        return DistChange;
    }
    public float GetDistanceToPlayer()
    {
        return DistToPlayer;
    }
    public float GetDistanceToPlayerChange()
    {
        float DistChange = DistToPlayer - PrevDistToPlayer;
        if (DistChange < 0) DistChange *= -1;
        return DistChange;
    }
    public bool GetIsInfrontCamera()
    {
        return IsInfrontCamera;
    }

    /* Enable/disable tracking for this object */
    public void Enable()
    {
        IsEnabled = true;
    }
    public void Disable()
    {
        IsEnabled = false;

        DistToCamera = 0.0f;
        PrevDistToCamera = 0.0f;
        DistToPlayer = 0.0f;
        PrevDistToPlayer = 0.0f;
        IsInfrontCamera = false;

        TimeSinceLastTrack = 0.0f;
    }

    /* Enabled or disabled? */
    public bool GetIsEnabled()
    {
        return IsEnabled;
    }
}
