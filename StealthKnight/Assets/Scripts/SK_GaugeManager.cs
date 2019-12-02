using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The manager to control UI gauges */
public class SK_GaugeManager : MonoSingleton<SK_GaugeManager>
{
    [SerializeField] private GameObject HealthGauge;
    [SerializeField] private GameObject StaminaGauge;

    private RectTransform HealthRect;
    private RectTransform StaminaRect;

    private float HealthMaxWidth;
    private float StaminaMaxWidth;

    private SK_Gauge HealthGaugeScript;
    private SK_Gauge StaminaGaugeScript;

    /* Get references on startup */
    private void Start()
    {
        HealthRect = HealthGauge.GetComponent<RectTransform>();
        StaminaRect = StaminaGauge.GetComponent<RectTransform>();

        HealthMaxWidth = HealthRect.sizeDelta.x;
        StaminaMaxWidth = HealthRect.sizeDelta.x;
    }

    /* Set the size of the health bar relative to the percent of health remaining */
    public void SetHealthPercent(float progress_percent)
    {
        if (progress_percent > 100) progress_percent = 100;
        if (progress_percent < 0) progress_percent = 0;
        HealthRect.sizeDelta = new Vector2(HealthMaxWidth * (progress_percent / 100), HealthRect.sizeDelta.y);
    }

    /* Set the size of the stamina bar relative to the percent of stamina remaining */
    public void SetStaminaPercent(float progress_percent)
    {
        if (progress_percent > 100) progress_percent = 100;
        if (progress_percent < 0) progress_percent = 0;
        StaminaRect.sizeDelta = new Vector2(StaminaMaxWidth * (progress_percent / 100), StaminaRect.sizeDelta.y);
    }

    /* Get/set health gauge instances */
    public void SetHealthGaugeInstance(SK_Gauge gauge)
    {
        HealthGaugeScript = gauge;
    }
    public SK_Gauge GetHealthGaugeInstance()
    {
        return HealthGaugeScript;
    }

    /* Get/set stamina gauge instances */
    public void SetStaminaGaugeInstance(SK_Gauge gauge)
    {
        StaminaGaugeScript = gauge;
    }
    public SK_Gauge GetStaminaGaugeInstance()
    {
        return StaminaGaugeScript;
    }
}
