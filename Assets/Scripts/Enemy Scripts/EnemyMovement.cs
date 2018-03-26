using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;               // Reference to the player's position.
    private PlayerHealth playerHealth;      // Reference to the player's health.
    private EnemyHealth enemyHealth;        // Reference to this enemy's health.
    private NavMeshAgent nav;               // Reference to the nav mesh agent.

    //[HideInInspector]
    //public int playerNumber = 1;            // Used to identify the different players.

    void Awake()
    {

    }

    private void Start()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
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
