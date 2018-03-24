using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 100;                            // The amount of health the player starts the game with.
    [SerializeField]
    private int currentHealth;                                   // The current health the player has.
    [SerializeField]
    private Slider healthSlider;                                 // Reference to the UI's health bar.
    [SerializeField]
    private Color fullHealthColor = Color.green;
    [SerializeField]
    private Color zeroHealthColor = Color.red;
    [SerializeField]
    private Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    [SerializeField]
    private float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    [SerializeField]
    private Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    private PlayerMovement playerMovement;                              // Reference to the player's movement.
    //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    private bool isDead;                                                // Whether the player is dead.
    private bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;         // Set the initial health of the player.
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }
}
