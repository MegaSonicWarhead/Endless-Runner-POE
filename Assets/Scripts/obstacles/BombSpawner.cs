using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform playerTransform;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float minDistanceBetweenBombs = 2f;
    public float bombTimerDuration = 5f;

    private float timer = 0f;
    private bool isBombSpawned = false;
    private GameObject currentBomb;

    void Update()
    {
        timer += Time.deltaTime;

        if (!isBombSpawned && timer >= spawnInterval && playerTransform.position.z > 0)
        {
            SpawnBomb();
            timer = 0f;
        }
    }

    public void SpawnBomb()
    {
        if (!isBombSpawned)
        {
            // Calculate spawn position
            Vector3 spawnPosition = CalculateSpawnPosition();

            // Spawn the bomb
            currentBomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

            // Start the timer coroutine for the bomb
            StartCoroutine(DestroyBombAfterDelay(currentBomb, bombTimerDuration));

            isBombSpawned = true;
        }
    }

    IEnumerator DestroyBombAfterDelay(GameObject bomb, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if the bomb still exists (it might have been destroyed by a collision)
        if (bomb != null)
        {
            // Destroy the bomb if the timer expires and the player hasn't collided with it
            Destroy(bomb);
            isBombSpawned = false;
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

        // Check if the spawn position is too close to the player or any existing obstacles
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, minDistanceBetweenBombs);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Obstacle") || collider.CompareTag("Pickup"))
            {
                // Recalculate spawn position if too close
                return CalculateSpawnPosition();
            }
        }

        return spawnPosition;
    }
}
