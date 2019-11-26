using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private Animator knightAnimator;

    [Range(0, 10)]
    [SerializeField] private float grabSpeed = 1.0f;

    private enum GrabHeight { LOW, MEDIUM, HIGH}
    [SerializeField] GrabHeight grabHeight;

    private Dictionary<GrabHeight, string> grabHeightDic = new Dictionary<GrabHeight, string>();

    [SerializeField] public bool autoGrabSpeedAdjust = true;

    [Range(0, 1)]
    [SerializeField] private float grabSpeedReadjustMult = 0.8f;

    private Hand leftHand;
    private Hand rightHand;
    private Inventory inventory;

    void Start()
    {
        knightAnimator.SetFloat("Grab Speed", grabSpeed);

        GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");

        if (handObjects[0].GetComponent<Hand>().isLeftHand)
        {
            leftHand = handObjects[0].GetComponent<Hand>();
            rightHand = handObjects[1].GetComponent<Hand>();
        }
        else
        {
            leftHand = handObjects[1].GetComponent<Hand>();
            rightHand = handObjects[0].GetComponent<Hand>();
        }

        grabHeightDic.Add(GrabHeight.LOW, "Low");
        grabHeightDic.Add(GrabHeight.MEDIUM, "Medium");
        grabHeightDic.Add(GrabHeight.HIGH, "High");
    }

    void Update()
    {
        float grabSpeedToUse = grabSpeed;

        if(grabHeight != GrabHeight.MEDIUM && autoGrabSpeedAdjust)
        {
            grabSpeedToUse = (float)grabSpeed * (float)grabSpeedReadjustMult;
        }

        if(grabSpeedToUse != knightAnimator.GetFloat("Grab Speed"))
        {
            knightAnimator.SetFloat("Grab Speed", grabSpeedToUse);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Return))
        {
            knightAnimator.SetTrigger("Grab " + grabHeightDic[grabHeight]);
        }

        if (knightAnimator.GetBool("Grabbed"))
        {
            if(!leftHand.isHandEmpty)
            {
                leftHand.heldObject.GetComponent<Item>();
            }

            if (!rightHand.isHandEmpty)
            {
                rightHand.heldObject.GetComponent<Item>();
            }
        }
    }
}
