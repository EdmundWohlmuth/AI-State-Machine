using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered the S P H E R E");
        enemy.GetComponent<EnemyMovement>().spottedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited the S P H E R E");
        enemy.GetComponent<EnemyMovement>().spottedPlayer = false;
    }
}
