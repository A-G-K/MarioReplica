using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoinController : MonoBehaviour
{
    CoinsManager coinsManager;
    UIManager uiManager;
    Rigidbody2D rigidBody;
    float timer = 0;
    [SerializeField] float velocity = 5;

    // Start is called before the first frame update
    void Start()
    {
        GameObject constantManagers = GameObject.FindGameObjectWithTag("ConstantManagers");
        coinsManager = constantManagers.GetComponentInChildren<CoinsManager>();

        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponentInChildren<UIManager>();

        rigidBody = GetComponent<Rigidbody2D>();

        coinsManager.AddCoin();
        uiManager.score += 200;

        rigidBody.gravityScale = 2;
        rigidBody.velocity = new Vector2(0, velocity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5)
        {
            Destroy(gameObject);
        }
    }
}
