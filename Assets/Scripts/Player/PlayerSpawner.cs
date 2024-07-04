using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public List<PlayerObject> playerObjects;

    void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var playerStats = GameManager.Instance.playerStats;
        if (playerStats == null)
        {
            Debug.Log($"PLAYER SPAWNER: {GameManager.Instance.coins}");
            Debug.LogError("Impossible to spawn the object because player target is missing");
            return;
        }

        var playerToSpawn = playerStats.playerStats.playerType;

        // Check errors
        if (playerObjects == null) Debug.LogError("Impossible to spawn the player because the pool of player objects is empty");
        foreach (var player in playerObjects)
        {
            if (player.playerType != playerToSpawn)
            {
                Debug.LogError($"Types doesn't match: {player.playerType} != {playerToSpawn}");
            }
        }

        foreach (var player in playerObjects)
        {
            if (player.playerType == playerToSpawn)
            {
                Instantiate(player.playerObject, transform.position, Quaternion.identity);
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
