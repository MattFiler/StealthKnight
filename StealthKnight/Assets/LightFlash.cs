using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoSingleton<LightFlash>
{
    private float TimeBetweenFlashes = 1.0f;
    private float CurrentTimer = 0.0f;
    private bool IsEnabled = false;
    private Light LightComponent;
    private void Start()
    {
        LightComponent = GetComponent<Light>();
    }
    void Update()
    {
        if (!IsEnabled) return;

        CurrentTimer += Time.deltaTime;
        if (CurrentTimer >= TimeBetweenFlashes)
        {
            LightComponent.enabled = !LightComponent.enabled;
            CurrentTimer = 0.0f;
        }
    }

    public void SetEnabled()
    {
        IsEnabled = true;
    }
}
