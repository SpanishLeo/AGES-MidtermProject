using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float aggroRange = 10;
    private Transform targetPlayer = null; 
    private EnemyHealth enemyHealth;        // Reference to this enemy's health.
    private PlayerHealth playerHealth;
    private NavMeshAgent nav;               // Reference to the nav mesh agent.

    private List<Transform> playerList;

    private void Start()
    {
        // Set up the references.
        playerList = new List<Transform>();
        playerList.Add(GameObject.FindGameObjectWithTag("Player1").transform);
        playerList.Add(GameObject.FindGameObjectWithTag("Player2").transform);
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void CheckForTarget()
    {
        foreach (var player in playerList)
        {
            Vector3 positionDifference = (player.transform.position - transform.position);

            float distanceToPlayer = Mathf.Abs(positionDifference.magnitude);

            if (distanceToPlayer <= aggroRange)
            {
                targetPlayer = player;

                playerHealth = targetPlayer.GetComponent<PlayerHealth>();
            }
        }
    }

    void Update()
    {
        if (targetPlayer == null)
        {
            CheckForTarget();
        }
        else
        {
            MoveTowardsTarget(targetPlayer);
        }
    }

    private void MoveTowardsTarget(Transform player)
    {
        // If the enemy and the player have health left...
        if (enemyHealth.currentEnemyHealth > 0 && playerHealth.currentPlayerHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}
