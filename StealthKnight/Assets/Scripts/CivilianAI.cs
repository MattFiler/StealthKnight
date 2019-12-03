using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianAI : MonoBehaviour
{
    [SerializeField] private Animator civAnimator;
    [SerializeField] private Transform exitLocation;
    [SerializeField] private POILookup poiLookup;
    [SerializeField] private float maxPathDist = 20;
    [SerializeField] private float minIdleTime = 2;
    [SerializeField] private float maxIdleTime = 5;
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private float minHeadTurn = 0.1f;
    [SerializeField] private float maxHeadTurn = 0.9f;


    private NavMeshAgent agent;
    private bool hasIdleRotation = false;
    private PointOfInterest targetPOI;
    private Quaternion targetIdleRot = Quaternion.identity;
    private float rotStep = 0;
    private bool isIdle = false;
    private bool setLook = false;
    private float timeIdle = 0;
    private float idleDuration = 0;
    private bool fleeing = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewPath();
        civAnimator.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(fleeing)
        {
            if(Vector3.Distance(transform.position, exitLocation.position) < 0.5f)
            {

            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            isIdle = true;
            idleDuration = Random.Range(minIdleTime, maxIdleTime);
            agent.updateRotation = false;

            Quaternion temp = transform.rotation;
            transform.LookAt(targetPOI.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            rotStep = 300.0f / Vector3.Distance(transform.eulerAngles, temp.eulerAngles);
            targetIdleRot = transform.rotation;
            transform.rotation = temp;

        }

        if (isIdle)
        {
            if (!setLook)
            {
                civAnimator.SetBool("Walk", false);
                civAnimator.SetFloat("Head Turn", Random.Range(minHeadTurn, maxHeadTurn));
                civAnimator.SetTrigger("Look");
                setLook = true;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, targetIdleRot, rotStep * Time.deltaTime);
            if (timeIdle < idleDuration)
            {
                timeIdle += Time.deltaTime;
            }
            else
            {
                civAnimator.SetBool("Walk", true);
                setLook = false;
                FindNewPath();
                timeIdle = 0;
                isIdle = false;
                agent.updateRotation = true;
            }
        }
        else
        {
            civAnimator.SetBool("Walk", true);
        }
    }

    void FindNewPath()
    {
        Vector3 randPoint;

        // If there are POI's, select one at random and pick a random location within its viewing area to visit
        if (poiLookup.pointsOfInterest.Length > 0)
        {
            hasIdleRotation = true;
            PointOfInterest poi = poiLookup.pointsOfInterest[Random.Range(0, poiLookup.pointsOfInterest.Length)];
            randPoint = Random.insideUnitSphere * poi.viewingArea;
            randPoint += poi.transform.position;
            targetPOI = poi;
        }
        // Otherwise pick a random area on the navmesh
        else
        {
            randPoint = Random.insideUnitSphere * maxPathDist;
            randPoint += transform.position;
        }
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randPoint, out hit, maxPathDist, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void SetAlert()
    {
        fleeing = true;
        civAnimator.SetBool("Run Away", true);
        agent.SetDestination(exitLocation.position);
        agent.speed *= runMultiplier;
    }

    private IEnumerator FadeOutAndDelete()
    {
        for(int i = 0; i < 20; i++)
        {

            yield return new WaitForSeconds(0.05f);
        }
    }
}
