using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroomController : MonoBehaviour
{
    [SerializeField] float velocity = 2.0f;
    private PlayerController pController;
    bool goingRight = true;
    private Rigidbody2D rigidBody;
    private bool canMove = true;
    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveItem());
        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        uiManager = managers.GetComponentInChildren<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Untagged")
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
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.011f);
        }

        MoveMushrooms(true);
        CollidersOn(true);
    }

    private void MoveMushrooms(bool move)
    {
        canMove = move;

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
