using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    bool gameOver = false;
    public GameObject liveHolder;
    public static GameManager instance;
    int score = 0;
     public Text scoreText;
    int lives = 3;
    public AudioSource scr;
    public AudioClip clip;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        NextLevelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString(); // Display remaining time
        }
        else if (timerRunning)
        {
            timerRunning = false;
            EvaluatePerformance();
        }
    }

    public void incrementScores()
    {
        if(!gameOver)
        {
            score++;
            scoreText.text = "" + score.ToString();
        }
       // print(score);
    }

    public void decreseLives()
    {
        if (lives > 0)
        {
            lives--;
            //print(lives);
            liveHolder.transform.GetChild(lives).gameObject.SetActive(false);
        }

        if (lives <= 0)
        {
            gameOver = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        //print("game over");
        CloneScript.instance.stopSpawn();
        GameObject.Find("catcher").GetComponent<MovePlayer>().isMove = false;
        GameOverPanel.SetActive(true);
    }

    public void RestartCandy()
    {
        SceneManager.LoadScene("MainGame 1");
    }

    public void MenuCandy()
    {
        SceneManager.LoadScene("Menu");
    }


    // Levels Section 


    public int currentLevel = 1;
    public int[] levelLives = { 5, 4, 3 };
    public float[] levelTimers = { 30f, 20f, 15f };
    public float[] candySpeeds = { 0.5f, 1f, 1.5f };
    public GameObject ratingPanel;
    public GameObject[] stars;
    public bool invalidCandyCaught = false;
    public Text timerText; // UI Text element for displaying the timer
    private float timeRemaining;
    private bool timerRunning = false;
    public GameObject NextLevelButton; 

    // Start Level
    public void StartLevel(int level)
    {
        currentLevel = level;
        lives = levelLives[level - 1];
        timeRemaining = levelTimers[level - 1];
        timerRunning = true;
       // StartCoroutine(LevelTimer());
    }

  

    // Adjust Candy Speed
    public float GetCandySpeed()
    {
        return candySpeeds[currentLevel - 1];
    }

    // Handle Invalid Candy Catch
    public void InvalidCandyCaught()
    {
        if (!gameOver)
        {
            decreseLives();
        }
    }

    // Evaluate Player Performance
    public void EvaluatePerformance()
    {
        gameOver = true;
        GameOverPanel.SetActive(false);
        ratingPanel.SetActive(true);

        foreach (GameObject star in stars) star.SetActive(false);

        if (score < 5)
        {
        }
        else if (score >= 5 && score < 15)
        {
            stars[0].SetActive(true);
            NextLevelButton.SetActive(true);
        }
        else if (score >= 15 && score < 20)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            NextLevelButton.SetActive(true);
        }
        else
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            NextLevelButton.SetActive(true);
        }
    }

    // Move to Next Level
    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > 3) currentLevel = 1;
        StartLevel(currentLevel);
    }




}
