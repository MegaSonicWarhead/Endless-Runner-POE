using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab of the bomb object to spawn
    public Transform playerTransform; // Reference to the player's transform
    public float spawnDistance = 10f; // Distance in front of the player to spawn bombs
    public float minDistanceBetweenBombs = 2f; // Minimum distance between spawned bombs
    public float bombSpawnRadius = 5.0f; // Radius around a position where no other bombs can spawn
    public float bombTimerDuration = 5f;

    private List<Vector3> bombPositions = new List<Vector3>(); // List to store positions of spawned bombs

    void Start()
    {
        // Start the coroutine to spawn bombs in front of the player
        StartCoroutine(SpawnBombInFront());
    }

    IEnumerator SpawnBombInFront()
    {
        while (true)
        {
            // Calculate the spawn position in front of the player
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

            // Add the bomb position to the list
            AddBombPosition(spawnPosition);

            // Wait for the next frame
            yield return null;
        }
    }

    public void SpawnBomb()
    {
        // Calculate spawn position
        Vector3 spawnPosition = GetRandomPosition();

        // Debugging: Check the spawn position
        Debug.Log($"Spawn Position: {spawnPosition}");

        // Spawn the bomb at the calculated position
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

        // Debugging: Check if bomb is spawned
        Debug.Log("Bomb Spawned");

        // Start the timer coroutine for the bomb
        StartCoroutine(DestroyBombAfterDelay(bomb, bombTimerDuration));

        // Add the bomb position to the list
        AddBombPosition(spawnPosition);
    }

    IEnumerator DestroyBombAfterDelay(GameObject bomb, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (bomb != null)
        {
            Destroy(bomb);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * bombSpawnRadius;
        randomDirection += playerTransform.position + playerTransform.forward * spawnDistance;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, bombSpawnRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, bombSpawnRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Bomb") || col.CompareTag("Pickup"))
            {
                return false;
            }
        }

        foreach (Vector3 bombPos in bombPositions)
        {
            if (Vector3.Distance(position, bombPos) < minDistanceBetweenBombs)
            {
                return false;
            }
        }

        return true;
    }

    public void AddBombPosition(Vector3 position)
    {
        bombPositions.Add(position);
    }
}
