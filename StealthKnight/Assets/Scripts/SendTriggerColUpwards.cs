using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTriggerColUpwards : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SendMessageUpwards("ObjectInViewCone", other);
    }
}
