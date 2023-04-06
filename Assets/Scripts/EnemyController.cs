using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform eyesPosition;
    public NavMeshAgent navMeshEnemy;
    public float maxDistance;
    public LayerMask layerMask;
    public float radiusCircle = 3f;

    private WayPointPatrol wayPointPatrol;

    private bool IsAlert;
    private bool detectPlayer;

    public GameEnding gameEnding;

    private void Awake()
    {
        wayPointPatrol = GetComponent<WayPointPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        VerificationDetectPlayer();
    }

    public void VerificationDetectPlayer()
    {
        if (!GameManager.instanceGameManager.playerMove.notCanMove)
        {
            Ray ray = new Ray(eyesPosition.position, transform.forward);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);

            IsAlert = Physics.Raycast(ray, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore);

            if (IsAlert)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    DetectoAlPlayer(hit);
                }
                else
                {
                    StatePatrulla();
                }
            }
        }
    }

    public void StatePatrulla()
    {
        if (navMeshEnemy.remainingDistance < navMeshEnemy.stoppingDistance && !detectPlayer)
        {
            wayPointPatrol.PatrullaEnemy();
        }
    }

    public void DetectoAlPlayer(RaycastHit hit)
    {
        detectPlayer = true;
        SetDestinationEnemy(hit.point);
        if (Physics.CheckSphere(transform.position, radiusCircle))
        {
            gameEnding.CaughtPlayer();
            hit.collider.gameObject.GetComponent<PlayerMove>().notCanMove = true;
        }
    }

    public void SetDestinationEnemy(Vector3 pointPlayer)
    {
        navMeshEnemy.SetDestination(pointPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, radiusCircle);
    }
}
