using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField] private Animator guardAnimator;

    public Vector3[] navPoints;
    [SerializeField] private navType navigationType;
    [SerializeField] private GameObject player;

    [SerializeField] private float attackRange = 1;
    [SerializeField] private float attackRate = 1;
    [SerializeField] private float moveSpeed = 7;


    private float timeSinceAttack = 0;
    private bool attackCooldown = false;

    private int currentNavIndex = 0;
    private bool navLoopForward = true;

    private NavMeshAgent agent;
    private bool fixate = false;

    public enum navType
    {
        inOrderLoop = 0, // Ai will navigate to the first, second, ..., last position in navPoints then start at first again
        inOrderReverse = 1, // Ai will navigate to the first, second, ..., last position in navPoints then go in reverse
        random = 2, // Ai will select a rnadom nav point each time
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = navPoints[0];
        FixatePlayer(true);
    }

    // Update is called once per frame
    void Update()
    {
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
                    agent.destination = transform.position;
                    if(!attackCooldown)
                        Attack();
                }
            }
            else
            {
                agent.destination = player.transform.position;
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
        guardAnimator.SetTrigger("Attack");
        SK_GaugeManager.Instance.GetStaminaGaugeInstance().Reduce(SK_GaugeReductionTypes.HIT_BY_GUARD);
        SK_GaugeManager.Instance.GetHealthGaugeInstance().Reduce(SK_GaugeReductionTypes.HIT_BY_GUARD);
        Debug.Log("Thwack!");
    }

    public void FixatePlayer(bool shouldFixate)
    {
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
}
