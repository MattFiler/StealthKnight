using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class GuardAI : MonoBehaviour
{
    [SerializeField] private Animator guardAnimator;

    public Vector3[] navPoints;
    [SerializeField] private navType navigationType;
    [SerializeField] private GameObject player;

    [SerializeField] private float attackRange = 1;
    [SerializeField] private float attackRate = 1;

    [SerializeField] private POILookup poiLookup;
    [SerializeField] private float walkSpeed = 4;
    [SerializeField] private float jogSpeed = 5;
    [SerializeField] private float runSpeed = 7;
    [SerializeField] private Transform rayPoint;

    private float moveSpeed = 4;
    private PointOfInterest targetPOI;

    private float timeSinceAttack = 0;
    private bool attackCooldown = false;

    private int currentNavIndex = 0;
    private bool navLoopForward = true;

    private NavMeshAgent agent;
    private bool fixate = false;
    private bool alert = false;

    private MeshCollider col;


    private StudioEventEmitter guardAudio;

    public enum navType
    {
        inOrderLoop = 0, // Ai will navigate to the first, second, ..., last position in navPoints then start at first again
        inOrderReverse = 1, // Ai will navigate to the first, second, ..., last position in navPoints then go in reverse
        random = 2, // Ai will select a rnadom nav point each time
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = walkSpeed;
        col = GetComponent<MeshCollider>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = navPoints[0];
        guardAudio = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!guardAudio.IsPlaying() && !SK_UIController.Instance.IsGameOver)
        {
            guardAudio.Play();
        }

        if(SK_UIController.Instance.IsGameOver)
        {
            guardAudio.Stop();
        }


        agent.speed = guardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ? 0 : moveSpeed;

        if(attackCooldown)
        {
            timeSinceAttack += Time.deltaTime;
            if(timeSinceAttack >= attackRate)
            {
                timeSinceAttack = 0;
                attackCooldown = false;
            }
        }

        if (fixate)
        {
            if (agent.remainingDistance <= attackRange)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
                {
                    agent.destination = player.transform.position;
                }
                else
                {
                    Quaternion temp = transform.rotation;
                    transform.LookAt(player.transform);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    Quaternion targetRot = transform.rotation;
                    transform.rotation = temp;
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10 * Time.deltaTime);
                    agent.destination = transform.position;
                    if (!attackCooldown)
                        Attack();
                }
            }
            else
            {
                agent.destination = player.transform.position;
            }
        }
        else if (alert && !fixate)
        {
            if (agent.remainingDistance < 0.1f)
            {
                FindNewPath();
            }
        }
        else if (Vector3.Distance(transform.position, navPoints[currentNavIndex]) < 2)
        {
            GetNextNavPoint();
            agent.destination = navPoints[currentNavIndex];
        }
    }

    void Attack()
    {
        if(!SK_UIController.Instance.IsGameOver)
        {
            guardAnimator.SetTrigger("Attack");
            Debug.Log("Thwack!");
            attackCooldown = true;
        }
    }

    public void SetAsAlert()
    {
        alert = true;
        moveSpeed = jogSpeed;
        FindNewPath();
        guardAnimator.SetBool("Sprint", true);
        guardAnimator.SetBool("Walk", false);
    }

    private void FixatePlayer(bool shouldFixate)
    {
        moveSpeed = shouldFixate ? runSpeed : walkSpeed;
        fixate = shouldFixate;
        guardAnimator.SetBool("Sprint", shouldFixate);
        guardAnimator.SetBool("Walk", !shouldFixate);
    }

    private void GetNextNavPoint()
    {
        if (navPoints.Length < 2)
            return;

        switch (navigationType)
        {
            case navType.inOrderLoop:
                {
                    currentNavIndex++;
                    if (currentNavIndex == navPoints.Length)
                        currentNavIndex = 0;
                    break;
                }
            case navType.inOrderReverse:
                {
                    if (navLoopForward)
                    {
                        currentNavIndex++;
                        if (currentNavIndex == navPoints.Length)
                        {
                            currentNavIndex -= 2;
                            navLoopForward = false;
                        }
                    }
                    else
                    {
                        currentNavIndex--;
                        if (currentNavIndex == -1)
                        {
                            currentNavIndex = 1;
                            navLoopForward = true;
                        }
                    }
                    break;
                }
            case navType.random:
                {
                    currentNavIndex = Random.Range(0, navPoints.Length);
                    break;
                }
        }
    }

    void FindNewPath()
    {
        Vector3 randPoint;


        PointOfInterest poi = poiLookup.pointsOfInterest[Random.Range(0, poiLookup.pointsOfInterest.Length)];
        randPoint = Random.insideUnitSphere * poi.viewingArea;
        randPoint += poi.transform.position;
        targetPOI = poi;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randPoint, out hit, 1000, 1))
        {
            Debug.Log("Guard got paff");
            agent.SetDestination(hit.position);
        }
    }

    private void ObjectInViewCone(Collider other)
    {
        if (!alert)
            return;
        if(other.CompareTag("Player") || other.CompareTag("Hand"))
        {

            RaycastHit hit;
            if(Physics.Raycast(rayPoint.position, (player.transform.position + transform.up) - (transform.position +transform.up), out hit))
            {
                if(hit.transform.CompareTag("Player") || other.CompareTag("Hand"))
                {
                    FixatePlayer(true);
                }
            }
        }
    }

}
