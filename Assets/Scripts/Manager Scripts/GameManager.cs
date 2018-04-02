using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timerDelay = 5f;            // Time to wait before restarting or ending the game.
    [SerializeField]
    public PlayerHealth playerHealth;           // Reference to the player's health.
    [SerializeField]
    private CameraControl cameraControl;        // Reference to the CameraControl script for control during different phases.
    [SerializeField]
    private GameObject enemyBoss;         // Reference to the prefab the enemy boss.
    [SerializeField]
    private GameObject playerPrefab;            // Reference to the prefab the players will control.

    public PlayerManager[] playerSetup;         // A collection of managers for enabling and disabling different aspects of the players.

    private int deadPlayerCount = 0;
    private int totalPlayers = 2;
    private EnemyHealth enemyBossHealth;



    private void Start()
    {
        // Get references to the components.
        enemyBossHealth = enemyBoss.GetComponent<EnemyHealth>();

        SpawnAllPLayers();
        SetCameraTargets();
    }

    private void FixedUpdate()
    {
        GameWon();
    }


    private void SpawnAllPLayers()
    {
        // For all the players...
        for (int i = 0; i < playerSetup.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            playerSetup[i].instance =
                Instantiate(playerPrefab, playerSetup[i].spawnPoint.position, playerSetup[i].spawnPoint.rotation) as GameObject;
            playerSetup[i].playerNumber = i + 1;
            playerSetup[i].Setup();
            playerSetup[i].instance.GetComponent<PlayerHealth>().PlayerDied += OnPlayerDied;
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

    private void GameWon()
    {
        if (enemyBossHealth.isDead == true)
        {
            StartCoroutine(PlayersWin());
        }
    }

    IEnumerator PlayersWin()
    {
        yield return new WaitForSeconds(timerDelay);
        SceneManager.LoadScene("End Scene");
    }

    private void OnPlayerDied()
    {
        deadPlayerCount++;

        if (deadPlayerCount == totalPlayers)
        {
            StartCoroutine(LevelRestartAfterDelay());
        }
    }

    IEnumerator LevelRestartAfterDelay()
    {
        yield return new WaitForSeconds(timerDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
