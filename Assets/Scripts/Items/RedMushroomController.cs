using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroomController : MonoBehaviour
{
    [SerializeField] float velocity = 2.0f;
    private PlayerController pController;
    bool goingRight = true;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

            if (goingRight)
            {
                rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(-velocity, rigidBody.velocity.y);
            }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Untagged")
        {
            goingRight = !goingRight;
        }
    }
}
