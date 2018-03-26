using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int startingEnemyHealth = 100;      // The amount of health the enemy starts the game with.
    [SerializeField]
    private float sinkSpeed = 2.5f;             // The speed at which the enemy sinks through the floor when dead.
    [SerializeField]
    private int scoreValue = 10;                // The amount added to the player's score when the enemy dies.

    [HideInInspector]
    public int currentEnemyHealth;              // The current health the enemy has.

    private BoxCollider boxCollider;            // Reference to the box collider.
    private bool isDead;                        // Whether the enemy is dead.
    private bool isSinking;                     // Whether the enemy has started sinking through the floor.



    void Start()
    {
        // Setting up the references.
        boxCollider = GetComponent<BoxCollider>();

        // Setting the current health when the enemy first spawns.
        currentEnemyHealth = startingEnemyHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
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

        // Turn the collider into a trigger so shots can pass through it.
        boxCollider.isTrigger = true;
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}
