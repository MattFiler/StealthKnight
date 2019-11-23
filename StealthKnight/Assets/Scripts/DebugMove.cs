using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A test object that moves at a given speed for a given distance on a given axis for testing camera tracking */
public class DebugMove : MonoBehaviour
{
    public float MoveDist = 50.0f;
    public bool MoveX = true;
    public float MoveSpeed = 5.0f;
    private bool DirectionFlipper = false;
    private Vector3 StartPos;

    /* Track the starting position to move from */
    private void Start()
    {
        StartPos = transform.position;
    }

    /* Move the debug object on the given axis over time */
    void Update()
    {
        Vector3 TargetPos = StartPos;
        if (DirectionFlipper)
        {
            if (MoveX) TargetPos += new Vector3(MoveDist, 0, 0);
            else TargetPos += new Vector3(0, 0, MoveDist);
        }
        else
        {
            if (MoveX) TargetPos -= new Vector3(MoveDist, 0, 0);
            else TargetPos -= new Vector3(0, 0, MoveDist);
        }
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);

        if (transform.position == TargetPos)
        {
            DirectionFlipper = !DirectionFlipper;
        }
    }
}
