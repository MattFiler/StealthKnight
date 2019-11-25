using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The manager in control of all objects the camera should be interested in */
public class SK_CameraManager : MonoSingleton<SK_CameraManager>
{
    [SerializeField] private GameObject PlayerObject;
    private List<SK_CameraInterest> CameraInterests = new List<SK_CameraInterest>();

    private SK_CameraController CameraController = null;
    private GameObject CameraGameObject = null;

    private float InterestTrackTickTime = 0.5f; //The seconds between interest track updates (lower time = better accuracy, but higher performance impact)

    /* Get camera reference on start */
    private void Start()
    {
        SetCamera(Camera.main.gameObject);
    }

    /* Get the player GameObject */
    public GameObject GetPlayer()
    {
        return PlayerObject;
    }

    /* Get/set the camera GameObject */
    public void SetCamera(GameObject cam)
    {
        CameraGameObject = cam;
        CameraController = CameraGameObject.GetComponent<SK_CameraController>();
        if (CameraController == null)
        {
            CameraGameObject.AddComponent<SK_CameraController>();
            CameraController = CameraGameObject.GetComponent<SK_CameraController>();
        }
    }
    public GameObject GetCamera()
    {
        return CameraGameObject;
    }

    /* Add an interest */
    public void AddInterest(SK_CameraInterest interest)
    {
        CameraInterests.Add(interest);
    }

    /* Remove an interest */
    public void RemoveInterest(SK_CameraInterest interest)
    {
        CameraInterests.Remove(interest);
    }

    /* Return all camera interests */
    public List<SK_CameraInterest> GetInterests()
    {
        return CameraInterests;
    }

    /* Set/get the update tick time for trackable objects */
    public void SetTickTime(float tick)
    {
        InterestTrackTickTime = tick;
    }
    public float GetTickTime()
    {
        return InterestTrackTickTime;
    }
}
