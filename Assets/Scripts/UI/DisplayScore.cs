using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        int finalScore = PlayerPrefs.GetInt("Score", 0); // Get the final score from PlayerPrefs, defaulting to 0
        scoreText.text = "Score: " + finalScore;
    }
}
