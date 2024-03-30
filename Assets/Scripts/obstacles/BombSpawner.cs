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
        //yield return new WaitUntil(() => playerTransform.position.z > bomb.transform.position.z + 2f);
        
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
        // Set maximum number of attempts to find a suitable spawn position
        int maxAttempts = 10;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

            // Check if the spawn position is too close to the player or any existing obstacles
            Collider[] colliders = Physics.OverlapSphere(spawnPosition, minDistanceBetweenBombs);
            bool tooClose = false;
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player") || collider.CompareTag("Obstacle") || collider.CompareTag("Pickup"))
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                return spawnPosition;
            }

            // Increment attempts and try again
            attempts++;
        }

        // Return a default spawn position if maximum attempts reached
        return playerTransform.position + playerTransform.forward * spawnDistance;
    }
}
