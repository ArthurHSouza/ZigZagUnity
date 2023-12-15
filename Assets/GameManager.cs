using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private bool started = false;
    private int score = 0;
    private int bestScore = 0;
    private int speedIncressedTimes = 1;
    private PathCreation pathCreation;
    private CharControl charControl;

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        UpdateBestScoreText();
        pathCreation = FindObjectOfType<PathCreation>();
        charControl = FindObjectOfType<CharControl>();
    }
    void Update()
    {
        if(score % (3 * speedIncressedTimes) == 0 && score > 0 && speedIncressedTimes < 8)
        {
            speedIncressedTimes++;
            charControl.IncressSpeed(1.25f);
            pathCreation.CancelInvoke("CreatePath");
            pathCreation.StartPathCreation(1f/(5f + (speedIncressedTimes - 1)));
            return;
        }
        if (!started && Input.GetKey(KeyCode.Return))
        {
            started = true;
            pathCreation.StartPathCreation();
        }
    }
    private void UpdateBestScoreText()
    {
        bestScoreText.text = $"Best Score: {bestScore}";
    }

    public bool GetStarted()
    {
        return started;
    }

    public void IncressScore()
    {
        score++;
        scoreText.text = score.ToString();

        if(score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore = score;
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("BestScore")}";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
