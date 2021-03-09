using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    CoinsManager coinsManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coinsManager.AddCoin();
        Destroy(gameObject);
    }
}
