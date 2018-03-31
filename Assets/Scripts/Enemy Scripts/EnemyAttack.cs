using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    [SerializeField]
    private int attackDamage = 10;               // The amount of health taken away per attack.

    private GameObject player;                  // Reference to the player GameObject.
    private PlayerHealth playerHealth;          // Reference to the player's health.
    private EnemyHealth enemyHealth;            // Reference to this enemy's health.
    private bool playerInRange;                 // Whether player is within the trigger collider and can be attacked.
    private float timer;                        // Timer for counting up to the next attack.

    private List<GameObject> playerList;

    private void Start()
    {
        // Setting up the references.
        playerList = new List<GameObject>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerList.Add(GameObject.FindGameObjectWithTag("Player1"));
        playerList.Add(GameObject.FindGameObjectWithTag("Player2")); 
    }


    void OnTriggerEnter(Collider other)
    {
        foreach (var player in playerList)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
                playerHealth = player.GetComponent<PlayerHealth>();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        foreach (var player in playerList)
        {
            // If the exiting collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentEnemyHealth > 0)
        {
            // ... attack.
            Attack();
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentPlayerHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
