using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winstate : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    public GameObject start;
    public Transform movetopoint;
    public float speed;

 

    // Start is called before the first frame update
    void OnAwake()
    {
        UI.SetActive(false);
        player.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
      
        float step = speed * Time.deltaTime;
        if (transform.position != movetopoint.position)
         {
            transform.position = Vector3.MoveTowards(transform.position, movetopoint.position, step);
            Debug.Log("move");
        }
        if (transform.position == movetopoint.position)
        {
            transform.Rotate(10 *Time.deltaTime, 10 * Time.deltaTime, 0);
            Debug.Log("spin");
        }
    }
}
