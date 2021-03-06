using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour

{
    //text fields
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI livesText;

    float timeLeft = 400.0f;

    //player character object
    //private PlayerController playerController;


    void Start()
    {
        //initialize player object
        //GameObject mario = GameObject.Find("Mario");
        //playerController = mario.GetComponent<PlayerController>();

    }

    void Update()
    {
        UpdateTimer();
        UpdateScore();
        UpdateCoins();
        UpdateLives();

        //game over when time runs out
        if (timeLeft < 0)
        {
            StartCoroutine(EndLevel());
        }
  

        //game over condition
        //if (playerController.life <= 0)
        //{
        //    StartCoroutine(GameOver());
        //    Debug.Log("GameOver");
        //}

        ////reset level when R is pressed
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(1);
        //}

    }

    //update time counter and UI text
    public void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "TIME " + Mathf.Round(timeLeft);
    }

    //update score UI text
    public void UpdateScore()
    {
        scoreText.text = "SCORE " + 0;
    }

    //update coins UI text
    public void UpdateCoins()
    {
        coinsText.text = "COINS " + 0;
    }

    //update lives UI text
    public void UpdateLives()
    {
        livesText.text = "LIVES " + 0;
    }



    //SCENE MANAGEMENT

    //coroutine to load level cleared scene after a delay
    public IEnumerator EndLevel()

    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelCleared");

    }

    //coroutine to load game over scene after a delay
    public IEnumerator GameOver()

    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");


    }

}


