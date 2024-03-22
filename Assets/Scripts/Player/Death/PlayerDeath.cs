using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            // Destroy the bomb
            Destroy(other.gameObject);

            // Increment score
            score++;

            // Show the credits scene
            SceneManager.LoadScene("Credit screen");

            // Pass the score to the credits scene
            PlayerPrefs.SetInt("Score", score);
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Increment score when colliding with an obstacle
            score++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Increment score when passing obstacles
        if (other.CompareTag("Obstacle"))
        {
            score++;
        }
    }
}
