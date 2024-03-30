using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Fly,
        //Speed,
        // Add more pickup types here
    }

    [System.Serializable]
    public class PickupInfo
    {
        public PickupType type;
        public GameObject prefab;
        public float spawnDistance = 30f; // Distance in front of the player to spawn the pickup
        public float spawnInterval = 5f; // Interval between spawn attempts
        public float minDistanceBetweenPickups = 2f; // Minimum distance between spawned pickups
    }

    public List<PickupInfo> pickupInfos;
    public GameObject player;

    void Start()
    {
        foreach (var pickupInfo in pickupInfos)
        {
            StartCoroutine(SpawnPickup(pickupInfo));
        }
    }

    IEnumerator SpawnPickup(PickupInfo pickupInfo)
    {
        while (true)
        {
            yield return new WaitForSeconds(pickupInfo.spawnInterval);

            // Calculate spawn position
            Vector3 spawnPosition = player.transform.position + player.transform.forward * pickupInfo.spawnDistance;

            // Check if the spawn position is too close to any obstacles or bombs
            if (IsTooCloseToObstacle(spawnPosition, pickupInfo.minDistanceBetweenPickups) || IsTooCloseToBomb(spawnPosition, pickupInfo.minDistanceBetweenPickups))
            {
                continue; // Skip this spawn attempt
            }

            // Spawn the pickup prefab
            Instantiate(pickupInfo.prefab, spawnPosition, Quaternion.identity);
        }
    }

    static bool IsTooCloseToObstacle(Vector3 position, float minDistance)
    {
        // Check if the spawn position is too close to any obstacles
        Collider[] colliders = Physics.OverlapSphere(position, minDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                return true;
            }
        }

        return false;
    }

    static bool IsTooCloseToBomb(Vector3 position, float minDistance)
    {
        // Check if the spawn position is too close to any bombs
        Collider[] colliders = Physics.OverlapSphere(position, minDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Bomb"))
            {
                return true;
            }
        }

        return false;
    }
}
