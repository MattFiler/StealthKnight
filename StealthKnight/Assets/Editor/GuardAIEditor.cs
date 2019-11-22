// Name this script "EffectRadiusEditor"
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GuardAI))]
public class EnemyAIEditor : Editor
{
    public void OnSceneGUI()
    {
        GuardAI t = (target as GuardAI);

        EditorGUI.BeginChangeCheck();

        Vector3[] waypoints = new Vector3[t.navPoints.Length + 1];
        for (int i = 0; i < t.navPoints.Length; i++)
        {
            waypoints[i] = Handles.FreeMoveHandle(t.navPoints[i], Quaternion.identity, 0.1f, Vector3.one, Handles.CircleHandleCap);
            if (i > 0 && i < t.navPoints.Length && t.navPoints.Length > 1)
            {
                Handles.DrawLine(waypoints[i], waypoints[i - 1]);
            }
        }
        Handles.DrawLine(t.gameObject.transform.position, waypoints[0]);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed guard AI Settings");

            for (int i = 0; i < t.navPoints.Length; i++)
            {
                t.navPoints[i] = waypoints[i];
            }
        }
    }
}