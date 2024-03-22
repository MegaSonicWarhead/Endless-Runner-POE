using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + score.ToString();
    }
}
