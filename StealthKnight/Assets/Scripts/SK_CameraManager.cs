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
    private bool DidJustMotivateDirection = false;
    private float CameraTargetFOV = 0.0f;

    //Game state info
    private SK_CameraDirectionMotivation DirectionMotivation = SK_CameraDirectionMotivation.EAST;
    private SK_CameraPositionMotivation LocationMotivation = SK_CameraPositionMotivation.PASSIVE;
    private bool IsInAlertMode = false;
    private bool IsDead = false;

    //Tweakable values
    private int InitialPlayerBias = 4; //The initial bias for the camera to have towards the player
    private int SecondaryPlayerBias = 2; //The secondary bias for the camera to have towards the player
    private float CameraOffset = 10.0f; //The offset the camera should have from the interest mid-point both in height and width
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

    /* Disable/enable all interests */
    public void SetAllInterestsEnabled(bool enabled)
    {
        foreach (SK_CameraInterest interest in CameraInterests)
        {
            if (enabled) interest.Enable();
            else interest.Disable();
        }
    }

    /* Set game state info */
    public void SetLocationMotivation(SK_CameraPositionMotivation location)
    {
        LocationMotivation = location;
    }
    public void SetInAlertMode(bool inAlert)
    {
        IsInAlertMode = inAlert;
    }
    public void SetPlayerIsDead(bool isDead)
    {
        IsDead = isDead;
    }

    /* Get/set the motivation direction */
    public void SetMotivationDirection(SK_CameraDirectionMotivation motivation)
    {
        DirectionMotivation = motivation;
        DidJustMotivateDirection = true;
    }
    public SK_CameraDirectionMotivation GetMotivationDirection()
    {
        return DirectionMotivation;
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

    /* Update the raw look-at position, offset, and FOV */
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
        switch (DirectionMotivation)
        {
            case SK_CameraDirectionMotivation.NORTH:
                transform.position = CameraLookAt + new Vector3(CameraOffset, CameraOffset, 0.0f);
                break;
            case SK_CameraDirectionMotivation.EAST:
                transform.position = CameraLookAt + new Vector3(0.0f, CameraOffset, CameraOffset);
                break;
            case SK_CameraDirectionMotivation.SOUTH:
                transform.position = CameraLookAt + new Vector3(-CameraOffset, CameraOffset, 0.0f);
                break;
            case SK_CameraDirectionMotivation.WEST:
                transform.position = CameraLookAt + new Vector3(0.0f, CameraOffset, -CameraOffset);
                break;
        }

        //Change target FOV based on game states
        CameraTargetFOV = 60.0f;
        if (IsDead)
        {
            CameraTargetFOV = 40.0f; //Zoom into player corpse
        }
        else
        {
            if (LocationMotivation == SK_CameraPositionMotivation.CORIDOOR) CameraTargetFOV = 50.0f; //Narrower FOV for coridoor (maybe disable interests too)
            else if (LocationMotivation == SK_CameraPositionMotivation.ATRIUM) CameraTargetFOV = 80.0f; //Wider FOV for atrium 
            else if (IsInAlertMode) CameraTargetFOV = 70.0f; //Wider FOV for open space alert mode
        }

        //Debug: enable dead state or alert state
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetPlayerIsDead(!IsDead);
            Debug.Log("DEAD STATE TOGGLED TO: " + IsDead);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetInAlertMode(!IsInAlertMode);
            Debug.Log("ALERT STATE TOGGLED TO: " + IsDead);
        }
#endif
    }

    /* Did we just change motivation direction? */
    public bool GetDidJustChangeMotivationDirection()
    {
        bool motivDir = DidJustMotivateDirection;
        DidJustMotivateDirection = false;
        return motivDir;
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

    /* Get the intended FOV */
    public float GetIntendedCameraFOV()
    {
        return CameraTargetFOV;
    }
}
