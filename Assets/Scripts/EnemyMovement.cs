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

    // create states
    public enum State
    {
        Patrol,
        Chase,
        Attack,
        Search,
        Retreat
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    void SetState()
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
                break;

            case State.Search:
                break;

            case State.Retreat:
                break;
        }
    }

    void Patrol()
    {
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
        enemy.SetDestination(player.transform.position);
    }
}
