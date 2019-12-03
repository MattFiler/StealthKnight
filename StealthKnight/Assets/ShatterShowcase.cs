using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterShowcase : MonoBehaviour
{
    public bool shattered = false;
    [SerializeField] private GameObject showcaseTop;
    [SerializeField] private ParticleSystem glassShatter;
    [SerializeField] private GameObject showcaseItemObj;
    // Update is called once per frame
    public void Shatter()
    {
        if(!shattered)
        {
            AIManager.Instance.SetAlert();
            GameObject.Destroy(showcaseTop);
            glassShatter.Play();
            shattered = true;
        }
    }

    private void Update()
    {
        if(!shattered)
        {
            MeshCollider[] meshColliders = showcaseItemObj.GetComponents<MeshCollider>();
            meshColliders[0].isTrigger = false;
        }
        else
        {
            MeshCollider[] meshColliders = showcaseItemObj.GetComponents<MeshCollider>();
            meshColliders[0].isTrigger = true;
        }
    }
}
