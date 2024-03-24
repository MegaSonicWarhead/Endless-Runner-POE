using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Fly,
        // Add more pickup types here
    }

    public PickupType pickupType;
    public GameObject flyPickup; // Reference to the FlyPickup prefab
    public float spawnDistance = 10f; // Distance in front of the player to spawn the pickup
    public float spawnInterval = 5f; // Interval between spawn attempts
    public float minDistanceBetweenPickups = 2f; // Minimum distance between spawned pickups
    public GameObject player;

    void Start()
    {
        StartCoroutine(SpawnFlyPickup());
    }

    IEnumerator SpawnFlyPickup()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Calculate spawn position
            Vector3 spawnPosition = player.transform.position + player.transform.forward * spawnDistance;

            // Spawn the FlyPickup prefab
            Instantiate(flyPickup, spawnPosition, Quaternion.identity);
        }
    }
}
