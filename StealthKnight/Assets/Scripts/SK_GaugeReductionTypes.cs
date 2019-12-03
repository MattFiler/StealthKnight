using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Types of cause to reduce a gauge (these values are divided by 100 when used) */
public enum SK_GaugeReductionTypes
{
    HIT_BY_GUARD = 1500,
    SPRINTING = 50,
    PUNCHING = 500,
}