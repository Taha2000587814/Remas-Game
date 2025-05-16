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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        NextLevelButton.SetActive(false);
        foreach (GameObject star in stars)
    {
        star.SetActive(false);
    }

        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Load saved level or default to 1
        StartLevel(currentLevel); // Start the game from level 1
    }


    // Update is called once per frame
    void Update()
    {
       
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
            Debug.Log("Lives remaining: " + lives);

            if (lives < liveHolder.transform.childCount)
            {
                liveHolder.transform.GetChild(lives).gameObject.SetActive(false);
            }
        }

        if (lives <= 0)
        {
            gameOver = true;
            EvaluatePerformance(); 
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
    public float candySpeeds;
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

        if (currentLevel == 1)
        {
            lives = 5;
            timeRemaining = 30f;
            candySpeeds = 0.5f;
        }
        else if (currentLevel == 2)
        {
            lives = 4;
            timeRemaining = 20f;
            candySpeeds = 1f;
        }
        else if (currentLevel == 3)
        {
            lives = 3;
            timeRemaining = 15f;
            candySpeeds = 1.5f;
        }

        // **Activate correct number of lives based on the current level**
        for (int i = 0; i < liveHolder.transform.childCount; i++)
        {
            liveHolder.transform.GetChild(i).gameObject.SetActive(i < lives);
        }

        Debug.Log($"Starting Level {currentLevel}: Lives = {lives}, Timer = {timeRemaining}, Candy Speed = {candySpeeds}");

        timerRunning = true;
        StartCoroutine(LevelTimer());
    }



    // Adjust Candy Speed
    public float GetCandySpeed()
    {
        return candySpeeds; 
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
        // Increment level while keeping it within bounds (1 to 3)
        if (currentLevel + 1 <= 3)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1; // Reset to level 1 when exceeding level 3
        }

        // Save the updated level before reloading the scene
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        Debug.Log($"Switching to Level {currentLevel}");

        // Reload the scene to apply changes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





    IEnumerator LevelTimer()
    {
        timerRunning = true;
        while (timeRemaining > 0)
        {
            Debug.Log($"Timer: {Mathf.Ceil(timeRemaining)} seconds remaining");

            timeRemaining -= Time.deltaTime;
            timerText.text = "" + Mathf.Ceil(timeRemaining).ToString();
            yield return null;
        }

        timerRunning = false;
        EvaluatePerformance();
    }



}
