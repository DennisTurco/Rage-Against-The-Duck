using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public List<PlayerObject> playerObjects;
    [SerializeField] public bool firstSpawn = false;

    void Start()
    {
        StartCoroutine(InitializeAfterGamaManager());   

        if (firstSpawn)
        {
            GameManager.Instance.ClearGameData();
        }

        GameManager.Instance.LoadGameData();

        SpawnPlayer();
        SpawnPlayerMinions();
    }

    private IEnumerator InitializeAfterGamaManager()
    {
        yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.isInitialized);
    }

    private void SpawnPlayer()
    {
        var playerStats = GameManager.Instance.gameData.playerStats;
        if (playerStats == null)
        {
            Debug.LogError("Impossible to spawn the object because player target is missing");
            return;
        }

        var playerToSpawn = playerStats.playerStats.playerType;

        // Check errors
        if (playerObjects == null) Debug.LogError("Impossible to spawn the player because the pool of player objects is empty");

        foreach (var player in playerObjects)
        {
            if (player.playerType == playerToSpawn)
            {
                Instantiate(player.playerObject, transform.position, Quaternion.identity);
                GameManager.Instance.playerInitialized = true;
                return;
            }
        }

        Debug.LogError($"Error on spawning player: {playerToSpawn}");
    }

    private void SpawnPlayerMinions()
    {
        GameManager.Instance.SpawnMinions();
    }
}

[Serializable]
public class PlayerObject
{
    public GameObject playerObject;
    public PlayerType playerType;
}
