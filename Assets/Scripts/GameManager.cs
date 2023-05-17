using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Vector2 bottomLeft;
    public static bool gameOver;
    public static bool gameStarted;
    public GameObject gameOverPanel;
    public GameObject getReady;
    public static int gameScore;
    public GameObject score;
    public Score scoreScript;
    public GameObject recoveryButton;

    public PlayFabController PFC;
    public CheckUserCredentials CUC;
    // Start is called before the first frame update
    private void Awake()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
    }

    void Start()
    {

        scoreScript = score.GetComponent<Score>();
        //PFC.GetStats();
        gameOver = false;
        gameStarted = false;
        score.SetActive(false);
        //recoveryButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("totalScore", scoreScript.totalScore);
        PFC.SetStats();
        gameOver = true;
        gameOverPanel.SetActive(true);
        score.SetActive(false);
        gameScore = scoreScript.GetScore();
        
    }

    public void GameHasStarted()
    {
        StartCoroutine(WaitForFunction());
        score.SetActive(true);
        gameStarted = true;
        getReady.SetActive(false);
        
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PFC.GetStats();
    }

    public void RecoveryButton()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(0.1f);
        recoveryButton.SetActive(false);
    }
}
