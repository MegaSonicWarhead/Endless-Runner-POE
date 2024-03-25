using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            // Destroy the bomb
            Destroy(other.gameObject);

            // Get the score from the ScoreManager
            int finalScore = ScoreManager.Instance.score;

            // Show the credits scene
            SceneManager.LoadScene("Credit screen");

            // Pass the score to the credits scene
            PlayerPrefs.SetInt("Score", finalScore);
        }
    }
}
