using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLoadManager : MonoBehaviour
{
    private float loadDelay = 5f;
    private float timeElapsed;


    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > loadDelay)
        {
            SceneManager.LoadScene("Level");
        }
    }
}

