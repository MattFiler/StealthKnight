using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Change civilian appearance on spawn */
public class SK_ChangeAppearance : MonoBehaviour
{
    [SerializeField] private Renderer hair;
    [SerializeField] private Renderer body;
    [SerializeField] private Renderer trousers;
    [SerializeField] private Renderer shirt;

    /* Randomise colours on spawn */
    void Start()
    {
        hair.material.SetColor("_Color", new Color(Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), Random.Range(0.0f, 0.2f), 1.0f));
        body.material.SetColor("_Color", new Color(Random.Range(0.7f, 1.0f), Random.Range(0.4f, 0.6f), Random.Range(0.4f, 0.6f), 1.0f));
        trousers.material.SetColor("_Color", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f));
        shirt.material.SetColor("_Color", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f));
    }
}
