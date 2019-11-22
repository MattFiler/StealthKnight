using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.0f;
    [SerializeField] float rotationSpeed = 0.0f;

    [SerializeField] Animator knightAnimator;

    void Start()
    {

    }

    void FixedUpdate()
    {
        float x = moveSpeed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        float z = moveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        Vector3 dir = new Vector3(x, 0, z);

        transform.position += dir;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
