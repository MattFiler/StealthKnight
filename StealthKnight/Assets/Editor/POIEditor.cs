using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointOfInterest))]
public class POIEditor : Editor
{
    public void OnSceneGUI()
    {
        PointOfInterest t = (target as PointOfInterest);

        EditorGUI.BeginChangeCheck();

        t.viewingArea = Handles.RadiusHandle(Quaternion.identity, t.transform.position, t.viewingArea, false);
    }
}