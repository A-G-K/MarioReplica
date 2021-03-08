using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMushroomController : MonoBehaviour
{
    public bool canMove = true;
    private bool goingRight = true;
    private Rigidbody2D rigidBody;
    private LivesManager livesManager;
    [SerializeField] float velocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        livesManager = GameObject.FindGameObjectWithTag("ConstantManagers").GetComponentInChildren<LivesManager>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if(goingRight)
            {
                rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(-velocity, rigidBody.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            livesManager.lives += 1;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canMove)
        {
            goingRight = !goingRight;
        }
    }
}
