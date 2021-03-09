using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroomController : MonoBehaviour
{

    private PlayerController pController;
    // Start is called before the first frame update
    void Start()
    {
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pController.IncreaseMarioState();
            Destroy(gameObject);
        }
    }
}
