using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float rotationSpeed = 0.0f;
                      
    [SerializeField] private Animator knightAnimator;

    [SerializeField] private float maxVelocityMagnitude = 0.0f;
    private float maxVelocityMagnitudeSqrd = 0.0f;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        //Make it magnitude squared
        maxVelocityMagnitudeSqrd = Mathf.Pow(maxVelocityMagnitude,2);
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            velocity.x += moveSpeed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        }
        else
        {
            if(velocity.x > 0)
            {
                velocity.x -= moveSpeed * Time.fixedDeltaTime;
                if (velocity.x < 0)
                    velocity.x = 0;
            }
            else if(velocity.x < 0)
            {
                velocity.x += moveSpeed * Time.fixedDeltaTime;
                if (velocity.x > 0)
                    velocity.x = 0;
            }
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            velocity.z += moveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        }
        else
        {
            if (velocity.z > 0)
            {
                velocity.z -= moveSpeed * Time.fixedDeltaTime;
                if (velocity.z < 0)
                    velocity.z = 0;
            }
            else if (velocity.z < 0)
            {
                velocity.z += moveSpeed * Time.fixedDeltaTime;
                if (velocity.z > 0)
                    velocity.z = 0;
            }
        }

        if (velocity.sqrMagnitude > maxVelocityMagnitudeSqrd)
        {
            velocity.Normalize();
            velocity *= maxVelocityMagnitude;
        }

        knightAnimator.SetFloat("Walk Speed", velocity.magnitude);
       
        velocity.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = velocity;

        if (!(velocity.x == 0 && velocity.z == 0))
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(velocity),
                Time.fixedDeltaTime * rotationSpeed
            );
        }

        //if (velocity.magnitude != 0)
        //{
        //    knightAnimator.SetFloat("Walk Speed", 1);
        //}
        //else
        //{
        //    knightAnimator.SetFloat("Walk Speed", 0);
        //}
    }
}
