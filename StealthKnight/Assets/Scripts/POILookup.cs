using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class POILookup : MonoBehaviour
{
    [SerializeField] private bool useChildrenAsPOI = false;
    public PointOfInterest[] pointsOfInterest;

    private void Awake()
    {
        if(useChildrenAsPOI)
        {
            List<PointOfInterest> allPOI = new List<PointOfInterest>(pointsOfInterest);
            foreach(PointOfInterest p in GetComponentsInChildren<PointOfInterest>())
            {
                allPOI.Add(p);
            }
            pointsOfInterest = allPOI.ToArray();
        }
    }
}
