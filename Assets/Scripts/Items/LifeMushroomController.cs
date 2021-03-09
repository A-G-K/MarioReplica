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
        StartCoroutine(MoveItem());
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

    private IEnumerator MoveItem()
    {
        MoveMushrooms(false);
        CollidersOn(false);
        for (int i = 0; i < 60; i++)
        {
            Debug.Log("moving up " + i);
            yield return new WaitForSeconds(0.01f);
            Debug.Log("after");
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.011f);
        }

        MoveMushrooms(true);
        CollidersOn(true);
    }

    private void MoveMushrooms(bool move)
    {
        Debug.Log("created life");
        LifeMushroomController controller = gameObject.GetComponent<LifeMushroomController>();
        controller.canMove = move;

        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (move)
        {
            rigidBody.gravityScale = 1;
        }
        else
        {
            rigidBody.gravityScale = 0;
        }
    }

    private void CollidersOn(bool col)
    {
        Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = col;
        }
    }
}
