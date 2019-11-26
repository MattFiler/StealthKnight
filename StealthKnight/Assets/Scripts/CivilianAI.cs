using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianAI : MonoBehaviour
{

    [SerializeField] private Vector3 exitLocation;
    [SerializeField] private float maxPathDist = 20;
    [SerializeField] private float minIdleTime = 2;
    [SerializeField] private float maxIdleTime = 5;

    private NavMeshAgent agent;
    private bool isIdle = false;
    private float timeIdle = 0;
    private float idleDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f)
        {
            isIdle = true;
            idleDuration = Random.Range(minIdleTime, maxIdleTime);
        }

        if (isIdle)
        {
            if (timeIdle < idleDuration)
            {
                timeIdle += Time.deltaTime;
            }
            else
            {
                FindNewPath();
                timeIdle = 0;
                isIdle = false;
            }
        } 
    }

    void FindNewPath()
    {
        Debug.Log("Find New Path");
        Vector3 randPoint = Random.insideUnitSphere * maxPathDist;
        randPoint += transform.position;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(randPoint, out hit, maxPathDist, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void ExitBuilding()
    {
        agent.SetDestination(exitLocation);
    }
}
