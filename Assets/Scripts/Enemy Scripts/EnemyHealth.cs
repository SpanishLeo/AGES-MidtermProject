using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int startingEnemyHealth = 100;      // The amount of health the enemy starts the game with.

    [HideInInspector]
    public int currentEnemyHealth;              // The current health the enemy has.
    [HideInInspector]
    public bool isDead;                        // Whether the enemy is dead.

    private BoxCollider boxCollider;            // Reference to the box collider.



    void Start()
    {
        // Setting up the references.
        boxCollider = GetComponent<BoxCollider>();

        // Setting the current health when the enemy first spawns.
        currentEnemyHealth = startingEnemyHealth;
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentEnemyHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentEnemyHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead.
        isDead = true;
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;
    }
}
