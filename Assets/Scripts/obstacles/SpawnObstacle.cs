using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public Transform playerTransform;
    public float groundSpawnDistance = 10f; // Distance in front of the player to spawn ground obstacles
    public float airSpawnDistance = 20f; // Distance in front of the player to spawn air obstacles
    public float minDistanceBetweenObstacles = 5f; // Minimum distance between spawned obstacles
    public float spawnInterval = 2f; // Interval between obstacle spawns
    public GameObject[] groundObstaclePrefabs; // Array of ground obstacle prefabs
    public GameObject[] airObstaclePrefabs; // Array of air obstacle prefabs

    private List<GameObject> activeObstacles = new List<GameObject>();
    private static List<Vector3> obstaclePositions = new List<Vector3>();
    private float timer = 0f;

    //Is Called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Calculate spawn positions for ground and air obstacles
        Vector3 groundSpawnPos = playerTransform.position + playerTransform.forward * groundSpawnDistance;
        Vector3 airSpawnPos = playerTransform.position + playerTransform.forward * airSpawnDistance;

        // Check if enough time has passed to spawn a new obstacle
        if (timer >= spawnInterval)
        {
            // Check if there is no obstacle currently spawned
            if (IsTooCloseToObstacle(groundSpawnPos, minDistanceBetweenObstacles))
            {
                SpawnObstacleAtPosition(groundObstaclePrefabs, groundSpawnPos);
            }

            if (IsTooCloseToObstacle(airSpawnPos, minDistanceBetweenObstacles) && PlayerMovement.isFlying)
            {
                SpawnObstacleAtPosition(airObstaclePrefabs, airSpawnPos);
            }

            timer = 0f; // Reset the timer
        }

        //DespawnObstacle();
    }

    //private void DespawnObstacle()
    //{
    //    foreach (GameObject obstacle in activeObstacles)
    //    {
    //        if((obstacle.transform.position.z < playerTransform.position.z) && obstacle != null)
    //        {
    //            activeObstacles.Remove(obstacle); //remove the object from the list
                
    //            Destroy(obstacle); //Destroy the Object.
    //        }
    //    }
    //}
    void SpawnObstacleAtPosition(GameObject[] obstaclePrefabs, Vector3 spawnPos)
    {
        // Randomly select an obstacle prefab
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Spawn the obstacle
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Add the obstacle position to the list
        activeObstacles.Add(obstacle);
        obstaclePositions.Add(spawnPos);  
    }

    static bool IsTooCloseToObstacle(Vector3 position, float minDistance)
    {
        foreach (Vector3 obstaclePos in obstaclePositions)
        {
            if (Vector3.Distance(position, obstaclePos) < minDistance)
            {
                return false;
            }
        }

        return true;
    }

}
