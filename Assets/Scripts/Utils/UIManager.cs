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

    [SerializeField] private Canvas StageClearedCanvas;


    float timeLeft = 400.0f;

    LivesManager livesManager;
    CoinsManager coinsManager;
    private BackgroundMusicManager bgmManager;


    public int score = 0;

    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        livesManager = constantManagers.GetComponentInChildren<LivesManager>();
        coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();

        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        bgmManager = managers.GetComponentInChildren<BackgroundMusicManager>();


    }

    void Update()
    {
        UpdateTimer();
        UpdateScore();
        UpdateCoins();
        UpdateLives();

        //game over when time runs out
        if (timeLeft < 0 || livesManager.lives == 0)
        {
            SceneManager.LoadScene("Level");
        }

        ////reset level when R is pressed
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene("Level");
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
        scoreText.text = "SCORE " + score;
    }

    //update coins UI text
    public void UpdateCoins()
    {
        coinsText.text = "COINS " + coinsManager.coins;
    }

    //update lives UI text
    public void UpdateLives()
    {
        livesText.text = "LIVES " + livesManager.lives;
    }

    //SCENE MANAGEMENT

    //coroutine to load level cleared scene after a delay
    public IEnumerator EndLevel()

    {
        score += (int)Mathf.Round(timeLeft);
        yield return new WaitForSeconds(2f);
        bgmManager.PlaySound(3);
        StageClearedCanvas.GetComponent<Canvas>().enabled = true;
    }
}