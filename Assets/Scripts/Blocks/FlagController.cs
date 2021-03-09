using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public bool movingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingPlayer == true)
        {
            rigidBody.velocity = new Vector2(2.0f, rigidBody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponentInParent<PlayerMovement>();
            rigidBody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            StartCoroutine(MovingDownwards());
            playerMovement.canMove = false;
        }
    }

    private IEnumerator MovingDownwards()
    {
        rigidBody.velocity = new Vector2(0f, -2f);
        yield return new WaitForSeconds(1f);
        movingPlayer = true;

    }
}
