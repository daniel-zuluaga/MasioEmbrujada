using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] patrullaEnemyPoint;
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void PatrullaEnemy()
    {
        int randomPointEnemy = Random.Range(0, patrullaEnemyPoint.Length);

        enemyController.SetDestinationEnemy(patrullaEnemyPoint[randomPointEnemy].position);
    }
}
