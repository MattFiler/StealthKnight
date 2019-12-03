using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    private bool alerted = false;
    public void SetAlert()
    {
        if (!alerted)
        {
            transform.BroadcastMessage("SetAsAlert", SendMessageOptions.DontRequireReceiver);
            alerted = true;
        }
    }
}
