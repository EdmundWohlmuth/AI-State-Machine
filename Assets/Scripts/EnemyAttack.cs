using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerStay(Collider other)
    {
        enemy.GetComponent<EnemyMovement>().attacking = true;
    }

    private void OnTriggerExit(Collider other)
    {
       //
    }
}
