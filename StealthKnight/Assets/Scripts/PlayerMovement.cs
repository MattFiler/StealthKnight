﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;

    [SerializeField] private float multiplierOffset = 0.05f;
    [SerializeField] private float maxMultReduction = 0.25f;
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float rotationSpeed = 0.0f;
    [SerializeField] private float maxWalkSpeed = 0.0f;
    [SerializeField] private float maxSprintSpeed = 0.0f;
    [SerializeField] private float maxSneakSpeed = 0.0f;
    private float currentMaxSpeed = 0.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraRelativeVelocity = Vector3.zero;

    private StudioEventEmitter knightAudio;

    private float speedMult = 1.0f;
    bool sprintLocked = false;

    private void Start()
    {
        knightAudio = GetComponent<StudioEventEmitter>();
    }

    void FixedUpdate()
    {
        Inventory inventory = this.gameObject.GetComponent<Inventory>();
        float reduceSpeed = 0.0f;

        for (int i = 0; i < inventory.inventoryItems.Length; i++)
        {
            if(inventory.inventoryItems[i] != null)
            {
                reduceSpeed += multiplierOffset;
            }
        }
        if(reduceSpeed > maxMultReduction)
        {
            reduceSpeed = maxMultReduction;
        }

        speedMult = 1.0f - reduceSpeed;

        if (knightAnimator.GetBool("On Floor"))
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 1.0f;
        }
        else if (!knightAnimator.GetBool("On Floor"))
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 0.0f;

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

            cameraRelativeVelocity = (velocity.x * SK_CameraManager.Instance.GetCamera().transform.right + velocity.z * SK_CameraManager.Instance.GetCamera().transform.forward);
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
        if(SK_GaugeManager.Instance.GetStaminaGaugeInstance().GetGaugePercent() == 0)
        {
            sprintLocked = true;
        }
        if(sprintLocked)
        {
            if (SK_GaugeManager.Instance.GetStaminaGaugeInstance().GetGaugePercent() > 50)
            {
                sprintLocked = false;
            }
        }

        if (!sprintLocked && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button1)) && SK_GaugeManager.Instance.GetStaminaGaugeInstance().GetGaugePercent() > 0)
        {
            knightAnimator.SetBool("Sprinting", true);
            //knightAnimator.SetBool("Sneak", false);
            currentMaxSpeed = maxSprintSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            //knightAnimator.SetBool("Sneak", true);
            knightAnimator.SetBool("Sprinting", false);
            currentMaxSpeed = maxSneakSpeed;
        }
        else
        {
            knightAnimator.SetBool("Sprinting", false);
            //knightAnimator.SetBool("Sneak", false);
            currentMaxSpeed = maxWalkSpeed;
        }
    }

    private void setVelocityComponent(ref float component, float inputAxis)
    {
        if (inputAxis != 0)
        {
            if (!knightAudio.IsPlaying())
            {
                knightAudio.Play();
            }
            component += moveSpeed * inputAxis * Time.fixedDeltaTime * speedMult;
        }
        else
        {
            knightAudio.Stop();
            if (component > 0)
            {
                component -= moveSpeed * Time.fixedDeltaTime * speedMult;
                if (component < 0)
                    component = 0;
            }
            else if (component < 0)
            {
                component += moveSpeed * Time.fixedDeltaTime * speedMult;
                if (component > 0)
                    component = 0;
            }
        }

    }

    public void SpankPlayer()
    {
        knightAnimator.SetTrigger("Stumble");
        this.GetComponent<Inventory>().dropTopItem();

        if (SK_GaugeManager.Instance.GetHealthGaugeInstance().GetGaugePercent() <= 0)
        {
            //player is dead - todo: game over and death anim
            SK_UIController.Instance.ShowGameOver(false);
            SK_CameraManager.Instance.SetPlayerIsDead(true);
        }
    }

    /* If we collide with the portal, we're leaving */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SK_PORTAL")
        {
            SK_UIController.Instance.ShowGameOver(true);
        }
    }
}
