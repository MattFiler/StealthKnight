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

    //Tweakable values
    private int InitialPlayerBias = 4; //The initial bias for the camera to have towards the player
    private int SecondaryPlayerBias = 2; //The secondary bias for the camera to have towards the player
    private Vector3 CameraOffset = new Vector3(10.0f, 10.0f, 0.0f); //The offset the camera should have from the interest mid-point
    private float InterestTrackTickTime = 0.01f; //The seconds between interest track updates (lower time = better accuracy, but higher performance impact)

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

    /* Update the raw look-at position and offset */
    private Vector3 CameraLookAt = new Vector3(0.0f, 0.0f, 0.0f);
    private void Update()
    {
        //Work out what points should be looked at
        List<Vector3> PointsToInclude = new List<Vector3>();
        foreach (SK_CameraInterest interest in SK_CameraManager.Instance.GetInterests())
        {
            if (!interest.GetIsEnabled() || !interest.GetIsInfrontCamera()) continue;
            if (interest.GetDistanceToCamera() > 20.0f) continue; //ToDo: scale this with camera zoom out, and have a max cap
            PointsToInclude.Add(interest.gameObject.transform.position);
        }

        //Get the mid-point of all interest points
        int IncludePointCount = InitialPlayerBias;
        CameraLookAt = (SK_CameraManager.Instance.GetPlayer().transform.position * InitialPlayerBias);
        foreach (Vector3 include in PointsToInclude)
        {
            CameraLookAt += include;
            IncludePointCount++;
        }
        CameraLookAt /= IncludePointCount;

        //Look at the mid-point
        transform.LookAt(CameraLookAt);

        //Bias the position towards the player
        for (int i = 0; i < SecondaryPlayerBias; i++)
        {
            CameraLookAt += SK_CameraManager.Instance.GetPlayer().transform.position;
            CameraLookAt /= 2;
        }

        //Move a set distance from that position
        transform.position = CameraLookAt + CameraOffset;
    }

    /* Get the guessed look-at position */
    public Vector3 GetIntendedCameraLookAtPosition()
    {
        return CameraLookAt;
    }

    /* Get the dummy's position and rotation (the intended destination for the camera) */
    public Vector3 GetIntendedCameraPosition()
    {
        return transform.position;
    }
    public Quaternion GetIntendedCameraRotation()
    {
        return transform.rotation;
    }
}
