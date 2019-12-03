using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    public void SetAlert()
    {
        transform.BroadcastMessage("SetAlert", SendMessageOptions.DontRequireReceiver);
    }
}
