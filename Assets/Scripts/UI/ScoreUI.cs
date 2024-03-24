using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        ScoreManager.ScoreUpdated += UpdateScoreText;
    }

    void OnDisable()
    {
        ScoreManager.ScoreUpdated -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
