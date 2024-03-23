using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public BombSpawner bombSpawner;

    void Update()
    {
        // Move the player forward automatically
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check for player input for left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float movement = horizontalInput * speed * Time.deltaTime;
        transform.Translate(Vector3.right * movement, Space.Self);

        // Check for bomb spawn
        if (Input.GetKeyDown(KeyCode.Space) && bombSpawner != null)
        {
            bombSpawner.SpawnBomb();
        }
    }
}
