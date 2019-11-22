using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public Vector3[] navPoints;
    [SerializeField] private navType navigationType;
    [SerializeField] private GameObject player;

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
        if (fixate)
        {
            agent.destination = player.transform.position;
        }
        else if (Vector3.Distance(transform.position, navPoints[currentNavIndex]) < 2)
        {
            GetNextNavPoint();
            agent.destination = navPoints[currentNavIndex];
        }
    }

    void FixatePlayer(bool shouldFixate)
    {
        fixate = shouldFixate;
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
