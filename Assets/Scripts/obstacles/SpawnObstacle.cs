using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 10f;
    public GameObject[] obstaclePrefabs;

    private GameObject currentObstacle;
    private Vector3 hiddenPosition;

    void Start()
    {
        // Calculate the hidden position behind the player
        hiddenPosition = playerTransform.position - playerTransform.forward * 100f;
    }

    void Update()
    {
        // Check if there is no obstacle currently spawned
        if (currentObstacle == null)
        {
            // Calculate spawn position
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

            // Randomly select an obstacle prefab
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Spawn the obstacle
            currentObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Check if the player has passed the obstacle
            if (playerTransform.position.z > currentObstacle.transform.position.z)
            {
                // Move the obstacle to the hidden position
                currentObstacle.transform.position = hiddenPosition;

                // Reset the obstacle's state for reuse
                // (e.g., reset any animations, timers, or other state)
                currentObstacle = null;
            }
        }
    }
}
