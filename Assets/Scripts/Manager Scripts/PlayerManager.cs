using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Material playerColor;            // This is the color this tank will be tinted.
    public Transform spawnPoint;            // The position and direction the tank will have when it spawns.

    [HideInInspector]
    public int playerNumber;                // This specifies which player this the manager for.
    [HideInInspector]
    public GameObject instance;             // A reference to the instance of the player when it is created.

    private PlayerMovement movement;            // Reference to player's movement script, used to disable and enable control.
    private PlayerShooting shooting;            // Reference to player's shooting script, used to disable and enable control.
    private GameObject canvasGameObject;        // Used to disable the world space UI during the Starting and Ending phases of each round.


    public void Setup()
    {
        // Get references to the components.
        movement = instance.GetComponent<PlayerMovement>();
        shooting = instance.GetComponentInChildren<PlayerShooting>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;
        instance.tag = "Player" + playerNumber;

        // Set the player numbers to be consistent across the scripts.
        movement.playerNumber = playerNumber;
        shooting.playerNumber = playerNumber;

        // Get all of the renderers of the player.
        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material = playerColor;
        }
    }
}
