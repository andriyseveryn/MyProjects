using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManage : MonoBehaviour
{
    [SerializeField] private int current_score=0;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        SetScoreText();
    }
    public void IncreaseScore(int amountToDec)
    {
        current_score += amountToDec;
        SetScoreText();
    }
    void SetScoreText()
    {
        if (scoreText)
        {
            scoreText.text = " " + current_score;
        }
    }
    void Update()
    {
        
    }
}
