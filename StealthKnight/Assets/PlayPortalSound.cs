using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayPortalSound : MonoBehaviour
{
    StudioEventEmitter portalAudio;

    // Start is called before the first frame update
    void Start()
    {
        portalAudio = GetComponent<StudioEventEmitter>();
        portalAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
