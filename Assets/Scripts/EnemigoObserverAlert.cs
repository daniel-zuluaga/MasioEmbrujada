using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemigoObserverAlert : MonoBehaviour
{
    public Transform player;

    public Transform positionPlayer;

    public EnemyController enemyController;

    bool m_IsPlayerInRange = false;

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.CompareTag("Player"))
                {
                    enemyController.SetDestinationEnemy(positionPlayer.position);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = false;
        }
    }
}
