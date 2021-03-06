using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public int coins = 0;
    LivesManager livesManager;

    private void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        livesManager = constantManagers.GetComponentInChildren<LivesManager>();
    }

    public void AddCoin()
    {
        coins += 1;
        if(coins >= 100)
        {
            coins -= 100;
            livesManager.lives += 1;
        }
    }
}
