using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;

    // Patrol
    public int nextWayPoint;
    public Vector3[] points;
    public Vector3 lastKnownPosition;

    public bool spottedPlayer;
    public bool isSearching;
    public bool attacking;
    public Material currentMat;

    // create states
    public enum State
    {
        Patrol,
        Chase,
        Attack,
        Search,
        Retreat
    }
    public State state;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Patrol;
        isSearching = true;
        attacking = false;
        currentMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        ActivateState();
    }

    void ChangeState()
    {
        if (spottedPlayer)
        {
            state = State.Chase;
        }
        else if (!spottedPlayer && state == State.Chase)
        {
            Debug.Log("LOOKIN BRO");
            state = State.Search;
        }

        if (attacking)
        {
            state = State.Attack;
        }
    }

    void ActivateState()
    {
        switch (state)
        {
            case State.Patrol:                
                Patrol();
                break;

            case State.Chase:               
                Chase();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Search:
                Search();
                break;

            case State.Retreat:                
                Retreat();
                break;
        }
    }


    void Patrol()
    {
        currentMat.color = Color.green;
        if (Vector3.Distance(transform.position, points[nextWayPoint]) < 1)
        {
            if (nextWayPoint >= 3)
            {
                nextWayPoint = 0;
            }
            else
            {
                nextWayPoint++;
            }

            enemy.SetDestination(points[nextWayPoint]);
        }
    }

    void Chase()
    {
        currentMat.color = Color.red;

        enemy.SetDestination(player.transform.position);
    }

    void Attack()
    {
        currentMat.color = Color.black;

        enemy.isStopped = true;
        enemy.SetDestination(transform.position);
        StartCoroutine(WaitCorutine(1));
    }
    IEnumerator WaitCorutine(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        state = State.Chase;
        enemy.isStopped = false;
        attacking = false;
    }

    void Search()
    {
        currentMat.color = Color.yellow;
        

        if (isSearching)
        {
            lastKnownPosition = player.transform.position;
            enemy.SetDestination(lastKnownPosition);
            Debug.Log("S C A N N I N G");
            isSearching = false;
        }
        if (Vector3.Distance(transform.position, lastKnownPosition) < 2)
        {   
            Debug.Log("At Last Known Pos");
            state = State.Retreat;
            isSearching = true;
        }
    }

    void Retreat()
    {
        currentMat.color = Color.blue;
        enemy.SetDestination(points[nextWayPoint]);

        if (Vector3.Distance(enemy.transform.position, points[nextWayPoint]) < 1)
        {
            state = State.Patrol;
        }
    }

}
