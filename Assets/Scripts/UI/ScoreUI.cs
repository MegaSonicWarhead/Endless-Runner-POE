using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        // Update the score text
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
