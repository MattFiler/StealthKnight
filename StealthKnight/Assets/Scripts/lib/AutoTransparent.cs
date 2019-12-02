using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Reworked from here: https://answers.unity.com/questions/44815/make-object-transparent-when-between-camera-and-pl.html */
public class AutoTransparent : MonoBehaviour
{
    //Tweakable
    private float TransparentTransparency = 0.2f; //The transparency value when transparent
    private float TimeBeforeBecomingVisible = 0.5f; //Time before this becomes visible again
    private float TimeToBecomeVisible = 5.0f; //Time to take to make this visible

    /* Turn invisible */
    private List<Shader> OldShader = new List<Shader>();
    private List<Color> OldColour = new List<Color>();
    private bool TurnedInvisible = false;
    public void BeTransparent()
    {
        if (TurnedInvisible) return;
        OldShader.Clear();
        OldColour.Clear();
        foreach (Material mat in GetComponent<Renderer>().materials)
        {
            OldShader.Add(mat.shader);
            OldColour.Add(mat.color);
            mat.shader = Shader.Find("Transparent/Diffuse");
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, TransparentTransparency);
        }
        TurnedInvisible = true;
    }

    /* Monitor transparency, and revert if time has passed */
    private float Timer = 0.0f;
    void Update()
    {
        if (!TurnedInvisible) return;

        if (Timer >= TimeBeforeBecomingVisible)
        {
            int index = 0;
            foreach (Color col in OldColour)
            {
                GetComponent<Renderer>().materials[index].color = new Color(col.r, col.g, col.b, Mathf.Lerp(GetComponent<Renderer>().material.color.a, col.a, TimeToBecomeVisible / Time.deltaTime));

                if (GetComponent<Renderer>().materials[index].color.a == col.a)
                {
                    GetComponent<Renderer>().materials[index].shader = OldShader[index];
                }

                index++;
            }
            Timer = 0.0f;
            TurnedInvisible = false;
        }
        else
        {
            Timer += Time.deltaTime;
        }
    }
}
