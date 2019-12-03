using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;

    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float rotationSpeed = 0.0f;
    [SerializeField] private float maxWalkSpeed = 0.0f;
    [SerializeField] private float maxSprintSpeed = 0.0f;
    [SerializeField] private float maxSneakSpeed = 0.0f;
    private float currentMaxSpeed = 0.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraRelativeVelocity = Vector3.zero;

    void FixedUpdate()
    {
        if (!knightAnimator.GetBool("On Floor"))
        {

            setCurrentMaxSpeed();

            setVelocityComponent(ref velocity.x, Input.GetAxis("Horizontal"));
            setVelocityComponent(ref velocity.z, Input.GetAxis("Vertical"));

            if (velocity.magnitude > currentMaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxWalkSpeed;
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                velocity.x *= Mathf.Abs(Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                velocity.y *= Mathf.Abs(Input.GetAxis("Vertical"));
            }

            cameraRelativeVelocity = velocity.x * SK_CameraManager.Instance.GetCamera().transform.right + velocity.z * SK_CameraManager.Instance.GetCamera().transform.forward;
            cameraRelativeVelocity.y = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = cameraRelativeVelocity;
            knightAnimator.SetFloat("Walk Speed", cameraRelativeVelocity.magnitude);

            if (!(Mathf.Abs(velocity.x) <= 0.2 && Mathf.Abs(velocity.z) <= 0.2))
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(cameraRelativeVelocity),
                    Time.fixedDeltaTime * rotationSpeed
                );
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SpankPlayer();
            }

            if (knightAnimator.GetBool("Sprinting")) SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.SPRINTING);
        }
    }

    private void setCurrentMaxSpeed()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button1)) && SK_GaugeManager.Instance.GetStaminaGaugeInstance().GetGaugePercent() > 0)
        {
            knightAnimator.SetBool("Sprinting", true);
            knightAnimator.SetBool("Sneak", false);
            currentMaxSpeed = maxSprintSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            knightAnimator.SetBool("Sneak", true);
            knightAnimator.SetBool("Sprinting", false);
            currentMaxSpeed = maxSneakSpeed;
        }
        else
        {
            knightAnimator.SetBool("Sprinting", false);
            knightAnimator.SetBool("Sneak", false);
            currentMaxSpeed = maxWalkSpeed;
        }
    }

    private void setVelocityComponent(ref float component, float inputAxis)
    {
        if (inputAxis != 0)
        {
            component += moveSpeed * inputAxis * Time.fixedDeltaTime;
        }
        else
        {
            if (component > 0)
            {
                component -= moveSpeed * Time.fixedDeltaTime;
                if (component < 0)
                    component = 0;
            }
            else if (component < 0)
            {
                component += moveSpeed * Time.fixedDeltaTime;
                if (component > 0)
                    component = 0;
            }
        }
    }

    public void SpankPlayer()
    {
        knightAnimator.SetTrigger("Stumble");

        if (SK_GaugeManager.Instance.GetHealthGaugeInstance().GetGaugePercent() <= 0)
        {
            //player is dead - todo: game over and death anim
            SK_CameraManager.Instance.SetPlayerIsDead(true);
        }
    }
}
