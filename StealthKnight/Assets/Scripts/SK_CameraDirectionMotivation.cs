using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The current direction to motivate the camera towards */
public enum SK_CameraDirectionMotivation
{
    NORTH, //Forward from default
    EAST,  //Left from default
    SOUTH, //Back from default
    WEST,  //Right from default
}

/* Set the current camera location (changes FOV) */
public enum SK_CameraPositionMotivation
{
    ATRIUM,   //In the atrium
    CORIDOOR, //In a coridoor
    PASSIVE   //Any other area
}