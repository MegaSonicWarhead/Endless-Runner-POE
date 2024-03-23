using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 10f;
    public float minDistanceBetweenBombs = 5f; // Define the minimum distance between bombs here
    public GameObject[] obstaclePrefabs;

    private GameObject currentObstacle;
    private Vector3 hiddenPosition;

    private List<Vector3> bombPositions = new List<Vector3>();

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
            Vector3 spawnPosition = CalculateObstacleSpawnPosition();

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

    Vector3 CalculateObstacleSpawnPosition()
    {
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

        // Check if the spawn position is too close to any bomb positions
        while (IsTooCloseToBomb(spawnPosition))
        {
            // Recalculate spawn position
            spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;
        }

        return spawnPosition;
    }

    bool IsTooCloseToBomb(Vector3 position)
    {
        foreach (Vector3 bombPosition in bombPositions)
        {
            if (Vector3.Distance(position, bombPosition) < minDistanceBetweenBombs)
            {
                return true;
            }
        }

        return false;
    }

    public void AddBombPosition(Vector3 position)
    {
        bombPositions.Add(position);
    }
}
