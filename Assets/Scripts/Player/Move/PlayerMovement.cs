using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float horizontalSpeed = 3f;
    public float flySpeed = 5f;
    public float flyHeight = 5f;
    public float forwardSpeed = 10f;
    public BombSpawner bombSpawner;
    public static bool isFlying = false;
    private DisplayScore displayScore;
    public float flyDuration = 5f;

    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        displayScore = FindObjectOfType<DisplayScore>();
    }

    void Update()
    {

        if (!isFlying)
        {
            // Normal movement logic

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
            transform.Translate(movement, Space.Self);

            // Check for bomb spawn
            if (bombSpawner != null && transform.position.z > 0)
            {
                // Calculate the spawn position in front of the player
                Vector3 spawnPosition = transform.position + transform.forward * bombSpawner.spawnDistance;
                bombSpawner.SpawnBomb();
            }

            // Move forward automatically
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }
        else
        {
            //Flying movement code
            float verticalInput = Input.GetAxis("Vertical"); // Get vertical input for flying up/down
            float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input for flying left/right
            Vector3 flyMovement = new Vector3(horizontalInput, 0f, 1f) * flySpeed * Time.deltaTime; // Always move forward
            transform.Translate(flyMovement, Space.Self);


            // Limit flying height
            if (transform.position.y < flyHeight)
            {
                transform.position = new Vector3(transform.position.x, flyHeight, transform.position.z);
            }
        }
    }

    public void StartFlying()
    {
        isFlying = true;
    }

    public void StopFlying()
    {
        isFlying = false;
        // Fall to the ground
        StartCoroutine(FallToGroundCoroutine());
    }

    IEnumerator FallToGroundCoroutine()
    {
        while (transform.position.y > 1.6f)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime); // Adjust speed as needed
            yield return null;
        }

        // Set the player's Y position to 1.6
        transform.position = new Vector3(transform.position.x, 1.6f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlyPickup"))
        {
            // Start flying logic here
            StartFlying();
            StartCoroutine(FlyDurationCoroutine());

            // Destroy the fly pickup object
            Destroy(other.gameObject);
        }
    }

    IEnumerator FlyDurationCoroutine()
    {
        yield return new WaitForSeconds(flyDuration);
        StopFlying();
    }
}

