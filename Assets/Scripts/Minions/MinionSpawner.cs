using System;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] public GameObject minionOrbiter;
    [SerializeField] public GameObject minionFollower;

    public void SpawnMinion(ItemName minionName)
    {
        if (minionOrbiter == null)
            Debug.LogError("Minion Orbiter prefab is not assigned in the Inspector!");

        if (minionFollower == null)
            Debug.LogError("Minion Follower prefab is not assigned in the Inspector!");

        if (minionOrbiter == null || minionFollower == null)
            throw new Exception("One or more minion prefabs are null");

        switch (minionName)
        {
            case ItemName.MinionOrbiter:
                PlayerMinion.CreateMinion(minionOrbiter);
                break;
            case ItemName.MinionFollower:
                PlayerMinion.CreateMinion(minionFollower);
                break;
            default:
                Debug.LogError($"Minion name '{minionName}' doesn't exist");
                break;
        }
    }
}
