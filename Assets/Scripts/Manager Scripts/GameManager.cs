using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float restartDelay = 5f;            // Time to wait before restarting the level
    [SerializeField]
    public PlayerHealth playerHealth;          // Reference to the player's health.
    [SerializeField]
    private CameraControl cameraControl;        // Reference to the CameraControl script for control during different phases.
    [SerializeField]
    private GameObject playerPrefab;            // Reference to the prefab the players will control.

    public PlayerManager[] playerSetup;         // A collection of managers for enabling and disabling different aspects of the players.

    private float restartTimer;                 // Timer to count up to restarting the level
    private int roundNumber;                    // Which round the game is currently on.
    private WaitForSeconds startWait;           // Used to have a delay whilst the round starts.
    private WaitForSeconds endWait;             // Used to have a delay whilst the round or game ends.
    private PlayerManager roundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
    private PlayerManager gameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.


    private void Start()
    {
        SpawnAllPLayers();
        SetCameraTargets();
    }


    private void SpawnAllPLayers()
    {
        // For all the tanks...
        for (int i = 0; i < playerSetup.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            playerSetup[i].instance =
                Instantiate(playerPrefab, playerSetup[i].spawnPoint.position, playerSetup[i].spawnPoint.rotation) as GameObject;
            playerSetup[i].playerNumber = i + 1;
            playerSetup[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        // Create a collection of transforms the same size as the number of tanks.
        Transform[] targets = new Transform[playerSetup.Length];

        // For each of these transforms...
        for (int i = 0; i < targets.Length; i++)
        {
            // ... set it to the appropriate tank transform.
            targets[i] = playerSetup[i].instance.transform;
        }

        // These are the targets the camera should follow.
        cameraControl.targets = targets;
    }


    void Update()
    {
        // If the player has run out of health...
        if (playerHealth.currentHealth <= 0)
        {
            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
