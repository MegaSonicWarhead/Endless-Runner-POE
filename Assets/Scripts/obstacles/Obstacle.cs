using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncreaseScoreOnPassingObstacle(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }


}
