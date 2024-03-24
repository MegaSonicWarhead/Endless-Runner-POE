using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ScoreManager).Name;
                    instance = obj.AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }
    public void IncreaseScoreOnPassingObstacle(int amount)
    {
        IncreaseScore(amount);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        ScoreUpdated(score);
    }

    public delegate void ScoreUpdatedDelegate(int newScore);
    public static event ScoreUpdatedDelegate ScoreUpdated;
}
