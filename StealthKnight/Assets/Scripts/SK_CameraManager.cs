using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The manager in control of all objects the camera should be interested in */
public class SK_CameraManager : MonoSingleton<SK_CameraManager>
{
    private List<SK_CameraInterest> CameraInterests = new List<SK_CameraInterest>();

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
}
