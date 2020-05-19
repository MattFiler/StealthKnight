using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public bool lookAt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Showcase")) lookAt = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Showcase")) lookAt = false;
    }
}
