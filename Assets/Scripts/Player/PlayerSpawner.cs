using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public List<PlayerObject> playerObjects;

    void Start()
    {
        StartCoroutine(InitializeAfterGamaManager());   
    }

    private IEnumerator InitializeAfterGamaManager()
    {
        yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.isInitialized);
        SpawnPlayer();
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

}


[Serializable]
public class PlayerObject
{
    public GameObject playerObject;
    public PlayerType playerType;
}
