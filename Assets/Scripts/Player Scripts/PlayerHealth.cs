using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 100;                           // The amount of health the player starts the game with.
    [SerializeField]
    private Slider healthSlider;                                // Reference to the UI's health bar.
    [SerializeField]
    private Image fillImage;                                    // The image component of the slider.
    [SerializeField]
    private Color fullHealthColor = Color.green;
    [SerializeField]
    private Color zeroHealthColor = Color.red;

    private PlayerMovement playerMovement;                              // Reference to the player's movement.
    private PlayerShooting playerShooting;                                      // Reference to the PlayerShooting script.
    private bool isDead;                                                // Whether the player is dead.
    private bool damaged;                                               // True when the player gets damaged.

    [HideInInspector]
    public float currentHealth;                                           // The current health the player has.


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;         // Set the initial health of the player.
    }

    private void OnEnable()
    {
        //currentHealth = startingHealth;
        //isDead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }


    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage done.
        currentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call Death.
        if (currentHealth <= 0f && !isDead)
        {
            Death();
        }
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        healthSlider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        playerShooting.DisableEffects();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
