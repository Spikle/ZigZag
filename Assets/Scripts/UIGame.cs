using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    public static UIGame instance;

    public GameObject game;
    public Text scoreGame;
    public GameObject strart;
    public GameObject gameOver;
    public Text scoreText;
    public Text bestScoreText;

    public void Awake()
    {
        instance = this;
    }

    public void ShowGameOver(int score)
    {
        int bestScore = 0;

        gameOver.SetActive(true);
        game.SetActive(false);

        if (PlayerPrefs.HasKey(("BestScore")))
            bestScore = PlayerPrefs.GetInt("BestScore");

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        scoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();

        TilesGeneration.instance.Restart();
    }
    
    public void Restart()
    {
        gameOver.SetActive(false);
        game.SetActive(true);
        strart.SetActive(true);
    }

    public void Tap()
    {
        if(strart.activeSelf)
            strart.SetActive(false);

        TilesGeneration.instance.sphere.Tap();
    }
}
