using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A gauge to monitor something on the player */
public class SK_Gauge : MonoBehaviour
{
    //Tweakable values (intended to edit in editor)
    [SerializeField] private SK_GaugeTypes GaugeType; //The type of gauge this is
    [SerializeField] private float MaxValue = 100; //The maximum value for this gauge that the player can have
    [SerializeField] private float RegenRate = 1.0f; //The amount of gauge value given back to the player per time defined below
    [SerializeField] private float RegenInterval = 1.0f; //The time (in seconds) between gauge regen hits

    /* Set gauge instance in manager */
    private void Start()
    {
        if (GaugeType == SK_GaugeTypes.HEALTH) SK_GaugeManager.Instance.SetHealthGaugeInstance(this);
        if (GaugeType == SK_GaugeTypes.STAMINA) SK_GaugeManager.Instance.SetStaminaGaugeInstance(this);
    }

    /* Reduce gauge value */
    private float CurrentValue = 100;
    public void Reduce(SK_GaugeReductionTypes reduction)
    {
        Debug.Log("Reducing " + GaugeType + " gauge! Now at " + GetGaugePercent() + "%");
        CurrentValue -= (int)reduction;
        if (CurrentValue < 0) CurrentValue = 0;
    }

    /* Increase gauge over time & update UI */
    private float TimeSinceRegen = 0.0f;
    private void Update()
    {
        TimeSinceRegen += Time.deltaTime;
        if (TimeSinceRegen >= RegenInterval)
        {
            CurrentValue += RegenRate;
            if (CurrentValue > MaxValue) CurrentValue = MaxValue;
            TimeSinceRegen = 0.0f;
        }

        if (GaugeType == SK_GaugeTypes.HEALTH) SK_GaugeManager.Instance.SetHealthPercent(GetGaugePercent());
        if (GaugeType == SK_GaugeTypes.STAMINA) SK_GaugeManager.Instance.SetStaminaPercent(GetGaugePercent());
    }

    /* Get the gauge type */
    public SK_GaugeTypes GetGaugeType()
    {
        return GaugeType;
    }

    /* Get the gauge value */
    public float GetGaugeValue()
    {
        return CurrentValue;
    }

    /* Get the gauge percent filled */
    public float GetGaugePercent()
    {
        return (CurrentValue / MaxValue) * 100;
    }
}
