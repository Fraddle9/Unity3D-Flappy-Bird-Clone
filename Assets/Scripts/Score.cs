using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public int highScore;
    public int totalScore;

    public Text panelScore;
    public Text panelHighScore;

    public GameObject New;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
        panelScore.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highScore");
        totalScore = PlayerPrefs.GetInt("totalScore");
        panelHighScore.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scored()
    {
        score++;
        totalScore++;
        scoreText.text = score.ToString();
        panelScore.text = score.ToString();
        
        if (score > highScore)
        {
            highScore = score;
            panelHighScore.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
            New.SetActive(true);
        }
        //panelHighScore.text = highScore.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
