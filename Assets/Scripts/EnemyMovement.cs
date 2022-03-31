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
            // Debug.Log("LOOKIN BRO");
            // TEMP would first goto search
            state = State.Search;

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
        if (transform.position == points[nextWayPoint])
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
    }

    void Search()
    {
        currentMat.color = Color.yellow;

        if (isSearching)
        {
            lastKnownPosition.x = player.transform.position.x;
            lastKnownPosition.z = player.transform.position.z;

            enemy.SetDestination(lastKnownPosition);
            Debug.Log("S C A N N I N G");
            isSearching = false;
        }
        

        if (enemy.transform.position.x == lastKnownPosition.x && enemy.transform.position.z == lastKnownPosition.z)
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

        if (enemy.transform.position == points[nextWayPoint])
        {
            state = State.Patrol;
        }
    }
}
