using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public bool isMovingLeft = true;
    
    private Rigidbody2D rb;
    private ContactPoint2D[] contactCache = new ContactPoint2D[10];
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            TryMoveOpposite(other);
        }
    }

    private void FixedUpdate()
    {
        float horizontalMovement = isMovingLeft ? -moveSpeed : moveSpeed;
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
    }

    private void TryMoveOpposite(Collision2D other)
    {
        bool shouldFlip = true;
        int contactCount = other.GetContacts(contactCache);

        for (int i = 0; i < contactCount; i++)
        {
            if (contactCache[i].normal.y > 0.5f)
            {
                shouldFlip = false;
                break;
            }
        }

        if (shouldFlip)
        {
            isMovingLeft = !isMovingLeft;
        }
    }
}