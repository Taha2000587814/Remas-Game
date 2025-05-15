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

}
