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


    void Start()
    {
        knightAnimator.SetFloat("Grab Speed", grabSpeed);

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
    }
}
