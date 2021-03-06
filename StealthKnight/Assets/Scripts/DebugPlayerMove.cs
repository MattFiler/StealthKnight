﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The debug player for camera tracking testing */
public class DebugPlayerMove : MonoBehaviour
{
    public float MoveSpeed = 0.3f;

    /* Debug "player" movement */
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(MoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0, 0, MoveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(MoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, MoveSpeed);
        }
    }
}
