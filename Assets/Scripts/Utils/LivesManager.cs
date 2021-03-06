using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public int lives = 3;

    public void LoseLife()
    {
        StartCoroutine(WaitForDeath());
    }

    // Waiting for animation for the player to die - to do in player
    public IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3f); // Time it takes for mario death animation
        lives -= 1;
        
        if (lives > 0)
        {
            SceneManager.LoadScene("Level");
        }

        else
        {
            lives = 3;
            SceneManager.LoadScene("GameOver");
        }
    }
}
