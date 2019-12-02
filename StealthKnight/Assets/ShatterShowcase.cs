using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterShowcase : MonoBehaviour
{
    public bool shattered = false;
    [SerializeField] private GameObject showcaseTop;
    [SerializeField] private ParticleSystem glassShatter;
    // Update is called once per frame
    public void Shatter()
    {
        if(!shattered)
        {
            showcaseTop.SetActive(false);
            glassShatter.Play();
            shattered = true;
        }
    }
}
