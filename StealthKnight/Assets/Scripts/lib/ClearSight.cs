using UnityEngine;
using System.Collections;

/* Reworked from here: https://answers.unity.com/questions/44815/make-object-transparent-when-between-camera-and-pl.html */
public class ClearSight : MonoBehaviour
{
    private float DistanceToPlayer = 15.0f;

    /* Check objects infront of camera, and trigger invisibility */
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
        foreach (RaycastHit hit in hits)
        {
            Renderer R = hit.collider.GetComponent<Renderer>();
            if (R == null) continue;
            if (!hit.collider.gameObject.name.ToUpper().Contains("WALL") && !hit.collider.gameObject.name.ToUpper().Contains("PILLAR")) continue; //Only make walls invisible

            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT == null) AT = R.gameObject.AddComponent<AutoTransparent>();
            AT.BeTransparent();
        }
    }
}