using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    CoinsManager coinsManager;
    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponentInChildren<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        uiManager.score += 200;
        coinsManager.AddCoin();
        Destroy(gameObject);
    }
}
