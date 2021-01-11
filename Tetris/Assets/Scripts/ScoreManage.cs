using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManage : MonoBehaviour
{
    [SerializeField] private int current_score=0;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
